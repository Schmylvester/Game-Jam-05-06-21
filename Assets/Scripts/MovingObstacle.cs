using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] float m_middle = 0.0f;
    [SerializeField] float m_acceleration = 0.0f;
    float m_speed = 0.0f;
    [SerializeField] float m_startDelay = 0.0f;

    void Update()
    {
        if (GameManager.instance.m_gameStatus == GameManager.GameStatus.Active)
        {
            if (m_startDelay <= 0)
            {
                int accelerationDirection = transform.position.y > m_middle ? -1 : 1;
                m_speed += (m_acceleration * Time.deltaTime * accelerationDirection);
                transform.position += Vector3.up * m_speed * Time.deltaTime;
            }
            else
            {
                m_startDelay -= Time.deltaTime;
            }
        }
    }
}
