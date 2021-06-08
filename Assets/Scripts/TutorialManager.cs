using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Text m_tutorialText = null;

    public bool m_tutorialActive;
    int m_currentTutorialPart = 0;
    string[] m_tutorialMessages = new string[]
    {
        "Tap to change size",
        "Tap and hold to jump",
        "Jump higher when you're big",
        "Jump longer when you're small"
    };

    void Start()
    {
        GameData.instance.m_tutorialManager = this;
        if (GameData.instance.m_playTutorialOnReload)
            m_tutorialActive = true;
        m_tutorialText.text = m_tutorialMessages[0];
    }

    public int getTutorialObstacle()
    {
        int[] map = new int[] { 3, 0, 2, 1 };
        return map[m_currentTutorialPart];
    }

    public void advanceTutorial()
    {
        if (m_currentTutorialPart == m_tutorialMessages.Length - 1)
        {
            m_tutorialText.enabled = false;
            m_tutorialActive = false;
            GameData.instance.m_playTutorialOnReload = false;
        }
        else
        {
            m_tutorialText.text = m_tutorialMessages[++m_currentTutorialPart];
        }
    }
}
