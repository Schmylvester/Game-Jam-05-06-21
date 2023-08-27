using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Text m_tutorialText = null;

    public ushort m_currentTutorialPart = 4;
    string[] m_tutorialMessages = new string[]
    {
        "Tap to change size",
        "Tap and hold to jump",
        "Jump higher when you're big",
        "Jump longer when you're small"
    };
    public UnityEvent onTutorialAdvanced;

    void Start()
    {
        GameData.instance.m_tutorialManager = this;
        m_currentTutorialPart = GameData.instance.m_tutorialReached;
        if (tutorialActive()) {
            updateText();
        }
    }

    public void updateText()
    {
        m_tutorialText.text = m_tutorialMessages[m_currentTutorialPart];
    }

    public bool tutorialActive()
    {
        return m_currentTutorialPart < m_tutorialMessages.Length;
    }

    public int getTutorialObstacle()
    {
        int[] map = new int[] { 3, 0, 2, 1 };
        return map[m_currentTutorialPart];
    }

    public void resetTutorial() {
        m_currentTutorialPart = 0;
        updateText();
    }

    public void advanceTutorial()
    {
        onTutorialAdvanced.Invoke();
        if (m_currentTutorialPart++ == m_tutorialMessages.Length - 1)
        {
            m_tutorialText.enabled = false;
        }
        else
        {
            m_tutorialText.text = m_tutorialMessages[m_currentTutorialPart];
            GameData.instance.m_tutorialReached = m_currentTutorialPart;
        }
    }
}
