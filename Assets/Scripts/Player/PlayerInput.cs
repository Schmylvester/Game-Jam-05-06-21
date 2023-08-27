using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] InputManager m_inputManager = null;
    [SerializeField] PlayerJump m_playerJump = null;
    [SerializeField] PlayerSize m_playerSize = null;

    void Update()
    {
        if (!GameManager.instance) return;
        if (GameManager.instance.m_gameStatus == GameManager.GameStatus.Active)
        {
            if (m_inputManager.tapDownThisFrame)
            {
                m_playerJump.jump(m_playerSize.getData());
                m_playerSize.switchSize();
            }
            else if (m_inputManager.tapUpThisFrame)
            {
                m_playerJump.cancelJump();
            }
        }
    }
}
