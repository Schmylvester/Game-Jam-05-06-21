using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameStatus
    {
        NotStarted = 0,
        Active,
        GameOver
    }

    [SerializeField] InputManager m_inputManager = null;
    [SerializeField] Animator m_playerAnimator = null;
    [SerializeField] GameObject m_tapToStartText = null;
    [SerializeField] GameObject m_gameOverText = null;
    [SerializeField] ScoreManager m_scoreManager = null;
    [SerializeField] GameObject m_tutorialText = null;
    [SerializeField] TutorialManager m_tutorialManager = null;

    public static GameManager instance = null;
    public GameStatus m_gameStatus = GameStatus.NotStarted;

    float m_difficultyIncrementTime = 15.0f;
    float m_timeSinceLastIncrement = 0.0f;
    float m_increments = 0.1f;
    float m_maxDifficulty = 5.0f;

    public void startGame()
    {
        m_tapToStartText.SetActive(false);
        m_playerAnimator.SetBool("running", true);
        Time.timeScale = 1;
        m_gameStatus = GameStatus.Active;
        if (m_tutorialManager.m_tutorialActive)
        {
            m_tutorialText.SetActive(true);
        }
    }

    public void gameOver()
    {
        m_tutorialText.SetActive(false);
        m_gameOverText.SetActive(true);
        m_gameStatus = GameStatus.GameOver;
        m_playerAnimator.SetBool("running", false);
        HighScoreManager.instance.checkHighScore(m_scoreManager.getScore());
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("What that all about?");
            return;
        }
        instance = this;
    }

    void Update()
    {
        if (m_inputManager.tapDownThisFrame)
        {
            if (m_gameStatus == GameStatus.NotStarted)
            {
                m_inputManager.clearInputThisFrame();
                startGame();
            }
            else if (m_gameStatus == GameStatus.GameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (m_gameStatus == GameStatus.Active && Time.timeScale < m_maxDifficulty
            && !m_tutorialManager.m_tutorialActive)
        {
            checkUpdateDifficulty();
        }
    }

    void checkUpdateDifficulty()
    {
        m_timeSinceLastIncrement += Time.unscaledDeltaTime;
        if (m_timeSinceLastIncrement > m_difficultyIncrementTime)
        {
            m_timeSinceLastIncrement = 0;
            Time.timeScale += m_increments;
        }
    }
}
