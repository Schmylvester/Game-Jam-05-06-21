using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TutorialManager m_tutorialManager = null;
    [SerializeField] AudioSource m_scoreSound = null;
    [SerializeField] Text m_scoreUI = null;
    int m_score = 0;
    int m_tutorialScore = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ScoreCheck")
        {
            m_scoreSound.Play();
            if (m_tutorialManager.tutorialActive()) {
                ++m_tutorialScore;
                if (m_tutorialScore % 2 == 0)
                {
                    m_tutorialManager.advanceTutorial();
                }
            } else {
                m_score++;
                m_scoreUI.text = m_score.ToString();
            }
        }
    }

    public int getScore()
    {
        return m_score;
    }
}
