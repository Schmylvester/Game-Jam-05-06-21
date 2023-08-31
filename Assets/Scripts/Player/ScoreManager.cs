using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent<int> m_onScoreChange;
    [SerializeField] TutorialManager m_tutorialManager = null;
    [SerializeField] PlayerAudioManager m_audioManager = null;
    [SerializeField] Text m_scoreUI = null;
    int m_score = 0;
    int m_tutorialScore = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ScoreCheck")
        {
            m_audioManager.score();
            if (m_tutorialManager.tutorialActive()) {
                ++m_tutorialScore;
                if (m_tutorialScore % 2 == 0)
                {
                    m_tutorialManager.advanceTutorial();
                }
            } else {
                m_score++;
                m_scoreUI.text = m_score.ToString();
                m_onScoreChange.Invoke(m_score);
            }
        }
    }

    public int getScore()
    {
        return m_score;
    }
}
