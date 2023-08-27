using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct DeathTweenSettings {
    public float timeToTake;
    public float endValue;
}

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator m_animator = null;
    private bool m_isBig = false;
    private JumpState m_animState = JumpState.OnGround;
    bool m_animEnabled = true;

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

    public void die() {
        setAnim("fall");
        m_animEnabled = false;
        deathTween();
    }

    void deathTween() {
        iTween.MoveTo(gameObject, iTween.Hash("x", -0.2f, "time", 1, "islocal", true));
        iTween.MoveTo(transform.parent.gameObject, iTween.Hash("y", -4.5f, "time", 1, "islocal", true));
    }

    void setAnim(string _state) {
        if (m_animEnabled){
            m_animator.Play("player_" + _state + (m_isBig ? "_big" : "_small"));
        }
    }
}
