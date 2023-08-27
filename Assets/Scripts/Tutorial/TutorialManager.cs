using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private float m_textSwitchTime = 0.5f;
    [SerializeField] GameObject[] m_tutorialSections = null;

    ushort m_currentTutorialPart = 4;

    public void Start()
    {
        if (tutorialActive()) {
           StartCoroutine(switchTutorialView(m_currentTutorialPart));
        }
    }

    public bool tutorialActive()
    {
        return m_currentTutorialPart < m_tutorialSections.Length;
    }

    public int getTutorialObstacle()
    {
        int[] map = new int[] { 3, 0, 2, 1 };
        return map[m_currentTutorialPart];
    }

    public void resetTutorial() {
        m_currentTutorialPart = 0;
        StartCoroutine(switchTutorialView(0));
    }

    public void advanceTutorial()
    {
        if (m_currentTutorialPart + 1 == m_tutorialSections.Length)
        {
            m_tutorialSections[m_currentTutorialPart].SetActive(false);
            m_currentTutorialPart++;
        }
        else
        {
            StartCoroutine(switchTutorialView((ushort)(m_currentTutorialPart + 1)));
        }
    }

    IEnumerator switchTutorialView(ushort _newSection) {
        float t = m_textSwitchTime / 2;
        while (t > 0) {
            t -= Time.deltaTime;
            m_tutorialSections[m_currentTutorialPart].transform.localScale = Vector3.one * t * (2 / m_textSwitchTime);
            yield return null;
        }
        m_tutorialSections[m_currentTutorialPart].SetActive(false);
        m_tutorialSections[_newSection].SetActive(true);
        m_currentTutorialPart = _newSection;
        t = 0;
        while (t < m_textSwitchTime / 2) {
            t += Time.deltaTime;
            m_tutorialSections[_newSection].transform.localScale = Vector3.one *  (2 / m_textSwitchTime) * t;
            yield return null;
        }
        yield return null;
    }
}
