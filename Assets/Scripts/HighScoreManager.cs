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

    public void setScoreRenderer(ShowHighScores _scoreRenderer)
    {
        m_scoreRenderer = _scoreRenderer;
    }

    public int checkHighScore(int newScore)
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
        HighScoreData highScoreData = new HighScoreData()
        {
            name = m_scoreRenderer.getName(),
            score = newScore
        };
        m_highScore.Insert(index, highScoreData);
        m_scoreRenderer.renderScores(m_highScore, index);
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
