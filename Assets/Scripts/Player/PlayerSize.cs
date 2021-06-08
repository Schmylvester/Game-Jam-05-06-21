using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    [SerializeField] PlayerParams m_params = null;
    bool m_isBig = false;

    bool m_coroutineActive = false;
    Coroutine m_activeCoroutine;

    public void switchSize()
    {
        if (m_coroutineActive)
            StopCoroutine(m_activeCoroutine);
        m_isBig = !m_isBig;
        m_activeCoroutine = StartCoroutine(m_isBig ? grow() : shrink());
    }

    public SizeData getData()
    {
        return m_isBig ? m_params.m_bigData : m_params.m_smallData;
    }

    IEnumerator grow()
    {
        m_coroutineActive = true;
        while (transform.localScale.x < m_params.m_bigData.scale)
        {
            transform.localScale += Vector3.one * m_params.m_bigData.scaleSpeed * Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one * m_params.m_bigData.scale;
        m_coroutineActive = false;
        yield return null;
    }

    IEnumerator shrink()
    {
        m_coroutineActive = true;
        while (transform.localScale.x > m_params.m_smallData.scale)
        {
            transform.localScale -= Vector3.one * m_params.m_smallData.scaleSpeed * Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one * m_params.m_smallData.scale;
        m_coroutineActive = false;
        yield return null;
    }
}
