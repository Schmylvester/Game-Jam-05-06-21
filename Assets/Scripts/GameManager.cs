using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameStatus
    {
        NotStarted = 0,
        Active,
        NameInput,
        GameOver
    }

    [SerializeField] InputManager m_inputManager = null;
    [SerializeField] PlayerAnimator m_playerAnimator = null;
    [SerializeField] GameObject m_tapToStartText = null;
    public GameObject m_gameOverObject = null;
    public GameObject m_nameInput = null;
    [SerializeField] ScoreManager m_scoreManager = null;
    [SerializeField] GameObject m_tutorialText = null;
    [SerializeField] TutorialManager m_tutorialManager = null;

    public static GameManager instance = null;
    public GameStatus m_gameStatus = GameStatus.NotStarted;

    [SerializeField] float m_difficultyIncrementTime = 15.0f;
    float m_timeSinceLastIncrement = 0.0f;
    [SerializeField] float m_increments = 0.1f;
    [SerializeField] float m_maxDifficulty = 5.0f;
    [SerializeField] ScrollObstacle[] m_obstacles = null;

    public void startGame(bool _clearData)
    {
        if (_clearData) {
            m_tutorialManager.resetTutorial();
        }
        m_playerAnimator.gameStart();
        m_tapToStartText.gameObject.SetActive(false);
        Time.timeScale = 1;
        m_gameStatus = GameStatus.Active;
        if (m_tutorialManager.tutorialActive())
        {
            m_tutorialText.SetActive(true);
        }
        foreach (ScrollObstacle obstacle in m_obstacles) {
            obstacle.lateStart();
        }
    }

    public void gameOver()
    {
        m_tutorialText.SetActive(false);

        if (HighScoreManager.instance.checkHighScore(m_scoreManager.getScore()))
        {
            m_gameStatus = GameStatus.NameInput;
        }
        else
        {
            m_gameOverObject.SetActive(true);
            m_gameStatus = GameStatus.GameOver;
        }
        HighScoreManager.instance.assignHighScore(m_scoreManager.getScore());
    }

    public void submitHighScore()
    {
        HighScoreManager.instance.submitHighScore();
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
            if (m_gameStatus == GameStatus.GameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (m_gameStatus == GameStatus.Active && Time.timeScale < m_maxDifficulty
            && !m_tutorialManager.tutorialActive())
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
        if (Time.timeScale > m_maxDifficulty)
        {
            Time.timeScale = m_maxDifficulty;
        }
    }
}
