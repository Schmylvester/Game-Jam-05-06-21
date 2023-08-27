using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData : MonoBehaviour
{
    [System.Serializable]
    struct DataToLoad
    {
        public HighScoreData[] highScoreData;
    }
    public static GameData instance;
    string m_filePath;
    [SerializeField] bool m_clearDataOnPlay = false;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {
        m_filePath = Application.persistentDataPath + "/" + "highScores.txt";
        if (File.Exists(m_filePath) && !m_clearDataOnPlay)
        {
            // read it and put it in the high scores
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(m_filePath, FileMode.Open);
            DataToLoad data = (DataToLoad)formatter.Deserialize(stream);
            stream.Close();
            HighScoreManager.instance.loadHighScores(data.highScoreData);
        }
        else
        {
            FileStream stream = new FileStream(m_filePath, FileMode.Create);
            populateFakeHighScores();
            stream.Close();
        }
    }

    public string[] getRandomNames()
    {
        string[] names = new string[]
        {
            "Sasha",
            "Arthur",
            "Bryn",
            "Sam",
            "Stephie",
            "Pri",
            "Josie"
        };
        for (int i = 0; i < 50; ++i) {
            for (int j = 1; j < names.Length; ++j) {
                if (Random.value < 0.5f) {
                    var temp = names[j - 1];
                    names[j - 1] = names[j];
                    names[j] = temp;
                }
            }
        }
        return names;
    }

    void populateFakeHighScores()
    {
        var names = getRandomNames();
        HighScoreData[] data = new HighScoreData[]
        {
            new HighScoreData {
                name = names[0],
                score = 15
            },
            new HighScoreData {
                name = names[1],
                score = 12
            },
            new HighScoreData {
                name = names[2],
                score = 9
            },
            new HighScoreData {
                name = names[3],
                score = 6
            },
            new HighScoreData {
                name = names[4],
                score = 3
            }
        };
        HighScoreManager.instance.loadHighScores(data);
    }

    private void OnApplicationQuit()
    {
        saveData();
    }

    private void OnApplicationPause(bool status)
    {
        if (status)
            saveData();
    }

    void saveData()
    {
        DataToLoad data = new DataToLoad()
        {
            highScoreData = HighScoreManager.instance.getHighScores()
        };
        FileStream stream = new FileStream(m_filePath, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Close();
    }
}
