using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObstacle : Scroll
{
    [SerializeField] ScoreManager m_scoreManager = null;
    [SerializeField] GameObject[] m_allElements = null;
    [SerializeField] float[] m_endPosPerObstacle;
    [SerializeField] TutorialManager m_tutorialManager = null;
    int m_currentObstacleIndex = 0;

    private void Start()
    {
        selectObstacle();
        if (m_tutorialManager.m_tutorialActive)
        {
            m_elements[0].transform.position += Vector3.right * 30;
        }
    }

    protected override bool checkEndPosReached(GameObject element)
    {
        return element.transform.position.x <= m_endPosPerObstacle[m_currentObstacleIndex];
    }

    protected override void resetPosition(GameObject element)
    {
        if (m_tutorialManager.m_tutorialActive)
        {
            StartCoroutine(delayObstacle(element));
        }
        else
        {
            sendObstacle(element);
        }
    }

    IEnumerator delayObstacle(GameObject obstacle)
    {
        yield return new WaitForSeconds(1);
        sendObstacle(obstacle);
        yield return null;
    }

    void sendObstacle(GameObject obstacle)
    {
        base.resetPosition(obstacle);
        selectObstacle();
    }

    void selectObstacle()
    {
        if (m_tutorialManager.m_tutorialActive)
        {
            m_currentObstacleIndex = m_tutorialManager.getTutorialObstacle();
        }
        else if (m_scoreManager.getScore() > 10)
        {
            m_currentObstacleIndex = Random.Range(0, m_allElements.Length);
        }
        else if (m_scoreManager.getScore() > 1)
        {
            m_currentObstacleIndex = Random.Range(0, m_allElements.Length - 1);
        }
        else
        {
            m_currentObstacleIndex = 0;
        }

        GameObject selectedObstacle = m_allElements[m_currentObstacleIndex];
        m_elements = new GameObject[] { selectedObstacle };
    }
}
