using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator m_animator = null;
    private bool m_isBig = false;
    private JumpState m_animState = JumpState.OnGround;

    public void gameStart() {
        onUpdateState();
    }

    public void setState(JumpState _state) {
        m_animState = _state;
        onUpdateState();
    }

    public void changeSize(bool _big) {
        m_isBig = _big;
        if (m_animState != JumpState.Jumping) {
            onUpdateState();
        }
    }

    void onUpdateState() {
        switch (m_animState) {
            case JumpState.OnGround: setAnim("run"); return;
            case JumpState.Jumping: setAnim("jump"); return;
            case JumpState.Hovering: setAnim("hover"); return;
            case JumpState.Falling: setAnim("fall"); return;
        }
    }

    void setAnim(string _state) {
        m_animator.Play("player_" + _state + (m_isBig ? "_big" : "_small"));
    }
}
