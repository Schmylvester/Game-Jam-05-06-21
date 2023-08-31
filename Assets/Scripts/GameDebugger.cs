using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDebugger : MonoBehaviour
{
    float[] m_rates = new float[]{0,0,0,0,0};
    int i = 0;
    void Update()
    {
        m_rates[i] = Time.unscaledDeltaTime;
        if (++i >= m_rates.Length) {
            i = 0;
        }
        float currentFrameRate = m_rates.Length / m_rates.Aggregate((sum, next) => sum + next);
        Debug.Log(currentFrameRate);
    }
}
