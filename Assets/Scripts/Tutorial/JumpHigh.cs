using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHigh : MonoBehaviour
{
    [SerializeField] private GameObject m_smallPlayer = null;
    [SerializeField] private GameObject m_largePlayer = null;

    void Start()
    {
        iTween.MoveTo(m_smallPlayer, iTween.Hash("y", -1, "time", 0.5f, "LoopType", "pingPong", "EaseType", "easeOutQuart", "isLocal", true, "delay", 0.1f));
        iTween.MoveTo(m_largePlayer, iTween.Hash("y", 2, "time", 0.5f, "LoopType", "pingPong", "EaseType", "easeOutQuart", "isLocal", true, "delay", 0.1f));
    }
}
