using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : Scroll
{
    [SerializeField] float m_posOffset = 0.0f;
    protected override bool checkEndPosReached(GameObject element)
    {
        GameObject other = element == m_elements[0] ? m_elements[1] : m_elements[0];
        if (other.transform.position.x < element.transform.position.x)
        {
            element.transform.position = new Vector3(other.transform.position.x + m_posOffset, element.transform.position.y);
        }
        else
        {
            other.transform.position = new Vector3(element.transform.position.x + m_posOffset, other.transform.position.y);
        }
        return base.checkEndPosReached(element);
    }
}
