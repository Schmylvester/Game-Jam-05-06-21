using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowShrinkTween : MonoBehaviour
{
    [SerializeField] private float m_targetScale;
    [SerializeField] private float m_scaleTime;
    [SerializeField] private float m_delay;
    void Start()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", m_targetScale, "y", m_targetScale, "time", m_scaleTime, "LoopType", "pingPong", "EaseType", "linear", "delay", m_delay));
    }
}
