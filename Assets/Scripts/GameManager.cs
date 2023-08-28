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
    [SerializeField] TutorialManager m_tutorialManager = null;

    public static GameManager instance = null;
    public GameStatus m_gameStatus = GameStatus.NotStarted;

    [SerializeField] float m_difficultyIncrementScore = 7;
    [SerializeField] float m_increments = 0.1f;
    [SerializeField] float m_maxDifficulty = 5.0f;
    [SerializeField] ScrollObstacle[] m_obstacles = null;

    public void startGame(bool _restartTutorial)
    {
        if (_restartTutorial) {
            m_tutorialManager.resetTutorial();
        }
        m_playerAnimator.gameStart();
        m_tapToStartText.gameObject.SetActive(false);
        Time.timeScale = 1;
        m_gameStatus = GameStatus.Active;
        foreach (ScrollObstacle obstacle in m_obstacles) {
            obstacle.lateStart();
        }
        m_scoreManager.m_onScoreChange.AddListener(checkUpdateDifficulty);
    }

    public void gameOver()
    {
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
        if (m_inputManager.unsafeTapDownThisFrame)
        {
            if (m_gameStatus == GameStatus.GameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void checkUpdateDifficulty(int _currentScore)
    {
        if (Time.timeScale < m_maxDifficulty && _currentScore % m_difficultyIncrementScore == 0) {
            Time.timeScale = Mathf.Min(Time.timeScale + m_increments, m_maxDifficulty);   
        }
    }
}
