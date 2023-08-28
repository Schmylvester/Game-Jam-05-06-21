using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenAnimator : MonoBehaviour
{
    [SerializeField] private TweenSettings[] m_parentTweens = null;
    [SerializeField] private TweenSettings[] m_selfTweens = null;
    void Start()
    {
        addTweens();
    }

    void addTweens() {
        foreach (var tween in m_selfTweens) {
            addTween(gameObject, tween);
        }
        foreach (var tween in m_parentTweens) {
            addTween(transform.parent.gameObject, tween);
        }
    }

    void addTween(GameObject _target, TweenSettings _settings) {
        var hash = iTween.Hash(
            _settings.dimension, _settings.target,
            "time", _settings.time,
            "LoopType", _settings.loopType,
            "EaseType", _settings.easeType,
            "isLocal", _settings.isLocal,
            "name", _settings.dimension + _settings.transformType
        );
        if (_settings.transformType == "scale") {
            iTween.ScaleTo(_target, hash);
        } else if (_settings.transformType == "move") {
            iTween.MoveTo(_target, hash);
        }
    }
}
