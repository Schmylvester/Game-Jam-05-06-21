using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HighScoreData
{
    public string name;
    public int score;
}

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    [SerializeField] int m_highScoreCount = 0;
    List<HighScoreData> m_highScore;
    ShowHighScores m_scoreRenderer = null;

    int m_scoreToAdd = 0;
    int m_indexToAdd = 0;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;

        m_highScore = new List<HighScoreData>();
    }

    public bool playerOnBoard(int score)
    {
        return score > m_highScore[m_highScoreCount - 1].score;
    }

    public void setScoreRenderer(ShowHighScores _scoreRenderer)
    {
        m_scoreRenderer = _scoreRenderer;
    }

    public bool checkHighScore(int score)
    {
        if (m_highScore.Count < m_highScoreCount)
        {
            return true;
        }
        return score > m_highScore[m_highScoreCount - 1].score;
    }

    public int assignHighScore(int newScore)
    {
        for (int i = 0; i < m_highScoreCount; ++i)
        {
            if (i >= m_highScore.Count)
            {
                addHighScore(newScore, i);
                return i;
            }
            else if (newScore > m_highScore[i].score)
            {
                addHighScore(newScore, i);
                return i;
            }
        }
        m_scoreRenderer.renderScores(m_highScore);
        return -1;
    }

    void addHighScore(int newScore, int index)
    {
        m_scoreToAdd = newScore;
        m_indexToAdd = index;
        GameManager.instance.m_nameInput.SetActive(true);
    }

    public void submitHighScore()
    {
        GameManager.instance.m_nameInput.SetActive(false);
        HighScoreData highScoreData = new HighScoreData()
        {
            name = m_scoreRenderer.getName(),
            score = m_scoreToAdd
        };
        m_highScore.Insert(m_indexToAdd, highScoreData);
        m_scoreRenderer.renderScores(m_highScore, m_indexToAdd);

        GameManager.instance.m_gameOverObject.SetActive(true);
        GameManager.instance.m_gameStatus = GameManager.GameStatus.GameOver;
    }

    public HighScoreData[] getHighScores()
    {
        return m_highScore.ToArray();
    }

    public void loadHighScores(HighScoreData[] _scores)
    {
        foreach (HighScoreData data in _scores)
        {
            m_highScore.Add(data);
        }
    }
}
