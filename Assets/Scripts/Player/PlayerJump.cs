using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum JumpState
    {
        Null = -1,
        OnGround,
        Jumping,
        Hovering,
        Falling
    }

public class PlayerJump : MonoBehaviour
{
    [SerializeField] PlayerAnimator m_playerAnimator;
    [SerializeField] private PlayerAudioManager m_playerAudioManager = null;
    float m_groundLevel = 0.0f;
    float m_gravityForce = 0.0f;
    float m_verticalSpeed = 0.0f;
    float m_hoverTime = 0.0f;
    JumpState m_state = JumpState.OnGround;

    private void Start()
    {
        m_groundLevel = transform.position.y;
    }

    public void jump(SizeData _data)
    {
        if (m_state == JumpState.OnGround)
        {
            setState(JumpState.Jumping);
            m_verticalSpeed = _data.jumpForce;
            m_gravityForce = _data.gravForce;
            m_hoverTime = _data.hoverTime;
            m_playerAudioManager.jump();
        }
        m_playerAudioManager.resize();
    }

    public void cancelJump()
    {
        if (m_state == JumpState.Jumping || m_state == JumpState.Hovering)
        {
            setState(JumpState.Falling);
            m_verticalSpeed *= -1;
        }
    }

    private void Update()
    {
        switch (m_state)
        {
            case JumpState.Null:
            case JumpState.OnGround:
                break;
            case JumpState.Jumping:
                jumpingState();
                break;
            case JumpState.Hovering:
                hoverState();
                break;
            case JumpState.Falling:
                fallingState();
                break;
        }
        transform.position += Vector3.up * m_verticalSpeed * Time.deltaTime;
    }

    void setState(JumpState _state)
    {
        if (_state == JumpState.Hovering) {
            m_playerAudioManager.startHover();
        } else if (m_state == JumpState.Hovering) {
            m_playerAudioManager.endHover();
        }
        m_state = _state;
        m_playerAnimator.setState(_state);
    }

    void jumpingState()
    {
        m_verticalSpeed -= m_gravityForce * Time.deltaTime;
        if (m_verticalSpeed <= 0)
        {
            setState(JumpState.Hovering);
        }
    }

    void hoverState()
    {
        m_hoverTime -= Time.deltaTime;
        if (m_hoverTime <= 0)
        {
            setState(JumpState.Falling);
        }
    }

    void fallingState()
    {
        m_verticalSpeed -= m_gravityForce * Time.deltaTime;
        if (transform.position.y <= m_groundLevel)
        {
            transform.position = new Vector3(transform.position.x, m_groundLevel);
            m_verticalSpeed = 0;
            setState(JumpState.OnGround);
        }
    }
}
