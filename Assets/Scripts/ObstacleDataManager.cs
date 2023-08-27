using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ScrollableObstacle {
    public string name;
    public float _endPos;
    public float _nextSpawnPos;
    public float _speedMod;
}
public class ObstacleDataManager : MonoBehaviour
{
    [SerializeField] ScrollableObstacle[] m_obstacleData = null;

    public ScrollableObstacle getData(int index) {
        return m_obstacleData[index];
    }
}
