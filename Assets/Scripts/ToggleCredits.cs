using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCredits : MonoBehaviour
{
    [SerializeField] GameObject m_credits = null;
    [SerializeField] Button m_startGameButton = null;
    [SerializeField] Button m_showCreditsButton = null;

    public void setCreditsActive(bool setTo)
    {
        m_credits.SetActive(setTo);
        m_startGameButton.enabled = !setTo;
        m_showCreditsButton.enabled = !setTo;
    }
}
