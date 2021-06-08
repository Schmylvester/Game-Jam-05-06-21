using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] AudioSource m_deathSound = null;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle"
            && GameManager.instance.m_gameStatus == GameManager.GameStatus.Active)
        {
            m_deathSound.Play();
            GameManager.instance.gameOver();
        }
    }
}