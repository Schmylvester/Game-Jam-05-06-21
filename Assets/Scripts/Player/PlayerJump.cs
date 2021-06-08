using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    enum JumpState
    {
        Null = -1,
        OnGround,
        Jumping,
        Hovering,
        Falling
    }

    [SerializeField] Animator m_playerAnimator;
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
            _data.audio.Play();
        }
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
        m_state = _state;
        updateAnimation(_state);
    }

    void updateAnimation(JumpState _state)
    {
        switch (_state)
        {
            case JumpState.OnGround:
                m_playerAnimator.SetBool("jumping", false);
                break;
            case JumpState.Jumping:
                m_playerAnimator.SetBool("jumping", true);
                break;
        }
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
