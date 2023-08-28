using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TweenSettings {
    public string transformType;
    public string dimension;
    public float target;
    public float time;
    public iTween.LoopType loopType;
    public iTween.EaseType easeType;
    public bool isLocal;
}