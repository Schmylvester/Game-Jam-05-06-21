using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTutorial : MonoBehaviour
{
    [SerializeField] private  Animator m_animator = null;
    [SerializeField] private float m_delayTime = 0.1f;
    [SerializeField] private float m_jumpTarget = -1;
    [SerializeField] private float m_jumpTime = 0.5f;
    void Start()
    {
        iTween.MoveTo(gameObject, iTween.Hash("y", m_jumpTarget, "time", m_jumpTime, "LoopType", "pingPong", "EaseType", "linear", "isLocal", true, "delay", m_delayTime));
        StartCoroutine(animator());
    }
        IEnumerator animator() {
        while(true) {
            m_animator.Play("RunSmall");
            yield return new WaitForSeconds(m_delayTime);
            m_animator.Play("JumpStartSmall");
            yield return new WaitForSeconds((m_jumpTime * 2) + m_delayTime);
        }
    }
}
