using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SizeData
{
    public float scale;
    public float scaleSpeed;
    public float jumpForce;
    public float gravForce;
    public float hoverTime;
    public AudioSource audio;
}

public class PlayerParams : MonoBehaviour
{
    public SizeData m_smallData;
    public SizeData m_bigData;
}
