using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : Singleton<Settings>
{
    Button m_muteButton = null;
    Button m_unmuteButton = null;
    bool m_isMuted = false;
    float m_defaultVolume = 0.0f;

    void Start() {
        m_defaultVolume = AudioListener.volume;
        onSceneLoad();
        SceneManager.sceneLoaded += (Scene _scene, LoadSceneMode _mode) => onSceneLoad();
    }

    void onSceneLoad() {
        m_muteButton = GameObject.Find("Mute").GetComponent<Button>();
        m_muteButton.onClick.AddListener(() => setMuted(true));
        m_unmuteButton = GameObject.Find("Unmute").GetComponent<Button>();
        m_unmuteButton.onClick.AddListener(() => setMuted(false));
        setMuted(m_isMuted);
    }

    public void setMuted(bool _value) {
        m_isMuted = _value;
        AudioListener.volume = m_isMuted ? 0 : m_defaultVolume;
        m_muteButton.gameObject.SetActive(!m_isMuted);
        m_unmuteButton.gameObject.SetActive(m_isMuted);
    }
}
