using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObstacle : Scroll
{
    [SerializeField] ScrollObstacle m_counterPart = null;
    [SerializeField] ScoreManager m_scoreManager = null;
    [SerializeField] MovingObstacle m_movingObstacle = null;
    [SerializeField] GameObject[] m_scrollableObstacles = null;
    [SerializeField] ObstacleDataManager m_obstacleDataManager = null;
    [SerializeField] TutorialManager m_tutorialManager = null;
    int m_currentObstacleIndex = 0;
    [SerializeField] bool m_activeObstacle = false;
    [SerializeField] int m_overrideObstacle = -1;

    //ensure the tutorial is set up before the obstacles start
    public void lateStart()
    {
        if (m_activeObstacle)
        {
            selectObstacle();
            if (m_tutorialManager.tutorialActive())
            {
                m_elements[0].transform.position += Vector3.right * 30;
            }
        }
    }

    protected override Vector3 getMovementFrameDistance() {
        var data = m_obstacleDataManager.getData(m_currentObstacleIndex);
        return base.getMovementFrameDistance() * data._speedMod;
    }

    protected override void resetPosition(GameObject element)
    {
        m_elements = new GameObject[0];
        base.resetPosition(element);
    }

    protected override bool checkEndPosReached(GameObject element)
    {
        var data = m_obstacleDataManager.getData(m_currentObstacleIndex);
        if (element.transform.position.x <= data._nextSpawnPos && m_activeObstacle)
        {
            readyForNext();
        }
        return element.transform.position.x <= data._endPos;
    }

    protected void readyForNext()
    {
        m_activeObstacle = false;
        if (m_tutorialManager.tutorialActive())
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
        else if (m_tutorialManager.tutorialActive())
        {
            m_currentObstacleIndex = m_tutorialManager.getTutorialObstacle();
        }
        else if (m_scoreManager.getScore() > 10)
        {
            m_currentObstacleIndex = Random.Range(0, m_scrollableObstacles.Length);
        }
        else if (m_scoreManager.getScore() > 1)
        {
            m_currentObstacleIndex = Random.Range(0, m_scrollableObstacles.Length - 1);
        }
        else
        {
            m_currentObstacleIndex = 0;
        }

        GameObject selectedObstacle = m_scrollableObstacles[m_currentObstacleIndex];
        m_elements = new GameObject[] { selectedObstacle };
    }
}
