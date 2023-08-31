using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct AudioData {
    public AudioSource source;
    public AudioClip big;
    public AudioClip small;
}

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_score;
    [SerializeField] private AudioSource m_hover;
    [SerializeField] private AudioData m_death;
    [SerializeField] private AudioData m_jump;
    [SerializeField] private AudioData m_resize;
    [SerializeField] private PlayerSize m_playerSize;

    public void score() {
        m_score.Play();
    }

    void playSound(AudioData _data) {
        _data.source.Pause();
        _data.source.clip = m_playerSize.isBig() ? _data.big : _data.small;
        _data.source.Play();
    }

    public void death() {
        m_hover.Pause();
        playSound(m_death);
    }

    public void jump() {
        playSound(m_jump);
    }

    public void resize() {
        playSound(m_resize);
    }

    public void startHover() {
        m_hover.Play();
    }

    public void endHover() {
        m_hover.Pause();
    }
}
