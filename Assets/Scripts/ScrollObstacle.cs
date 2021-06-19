using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObstacle : Scroll
{
    [SerializeField] ScrollObstacle m_counterPart = null;
    [SerializeField] ScoreManager m_scoreManager = null;
    [SerializeField] GameObject[] m_allElements = null;
    [SerializeField] float[] m_endPosPerObstacle;
    [SerializeField] float[] m_spawnNextPosPerObstacle;
    [SerializeField] TutorialManager m_tutorialManager = null;
    int m_currentObstacleIndex = 0;
    [SerializeField] bool m_activeObstacle = false;
    [SerializeField] int m_overrideObstacle = -1;

    void Start()
    {
        StartCoroutine(lateStart());
    }

    //ensure the tutorial is set up before the obstacles start
    IEnumerator lateStart()
    {
        yield return null;
        if (m_activeObstacle)
        {
            selectObstacle();
            if (m_tutorialManager.m_tutorialActive )
            {
                m_elements[0].transform.position += Vector3.right * 30;
            }
        }
    }

    protected override void resetPosition(GameObject element)
    {
        m_elements = new GameObject[0];
        base.resetPosition(element);
    }

    protected override bool checkEndPosReached(GameObject element)
    {
        if (element.transform.position.x <= m_spawnNextPosPerObstacle[m_currentObstacleIndex] && m_activeObstacle)
        {
            readyForNext();
        }
        return element.transform.position.x <= m_endPosPerObstacle[m_currentObstacleIndex];
    }

    protected void readyForNext()
    {
        m_activeObstacle = false;
        if (m_tutorialManager.m_tutorialActive)
        {
            StartCoroutine(delayObstacle());
        }
        else
        {
            m_counterPart.selectObstacle();
        }
    }

    IEnumerator delayObstacle()
    {
        yield return new WaitForSeconds(1);
        m_counterPart.selectObstacle();
        yield return null;
    }

    public void selectObstacle()
    {
        m_activeObstacle = true;
        if (m_overrideObstacle != -1)
        {
            m_currentObstacleIndex = m_overrideObstacle;
        }
        else if (m_tutorialManager.m_tutorialActive)
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
