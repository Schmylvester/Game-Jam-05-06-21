using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighScores : MonoBehaviour
{
    [SerializeField] Text[] m_highScoreNames = null;
    [SerializeField] Text[] m_highScoreScores = null;
    [SerializeField] InputField m_nameInput = null;

    void Start()
    {
        HighScoreManager.instance.setScoreRenderer(this);
    }

    public string getName()
    {
        return m_nameInput.text == "" ? GameData.instance.getRandomNames()[0] : m_nameInput.text;
    }

    public void renderScores(List<HighScoreData> _scores, int highlightPlayer = -1)
    {
        for (int i = 0; i < m_highScoreNames.Length; ++i)
        {
            if (i >= _scores.Count)
            {
                m_highScoreNames[i].text = "";
                m_highScoreScores[i].text = "";
            }
            else
            {
                m_highScoreNames[i].text = _scores[i].name;
                if (i == highlightPlayer)
                {
                    m_highScoreNames[i].color = Color.green;
                }
                m_highScoreScores[i].text = _scores[i].score.ToString();
            }
        }
    }
}
