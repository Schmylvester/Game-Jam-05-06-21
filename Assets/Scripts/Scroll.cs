using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] protected GameObject[] m_elements = null;
    [SerializeField] float m_scrollStartPos = 0.0f;
    [SerializeField] float m_scrollEndPos = 0.0f;
    [SerializeField] float m_speed = 0.0f;

    void Update()
    {
        if (GameManager.instance.m_gameStatus == GameManager.GameStatus.Active)
        {
            foreach (GameObject element in m_elements)
            {
                element.transform.position += Vector3.left * m_speed * Time.deltaTime;
                if (checkEndPosReached(element))
                {
                    resetPosition(element);
                }
            }
        }
    }

    protected virtual bool checkEndPosReached(GameObject element)
    {
        return element.transform.position.x <= m_scrollEndPos;
    }

    protected virtual void resetPosition(GameObject element)
    {
        element.transform.position = new Vector3(m_scrollStartPos, element.transform.position.y);
    }
}
