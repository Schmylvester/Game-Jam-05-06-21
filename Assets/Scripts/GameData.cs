using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData : MonoBehaviour
{
    [System.Serializable]
    struct DataToLoad
    {
        public ushort tutotialProgress;
        public HighScoreData[] highScoreData;
    }
    public static GameData instance;
    public TutorialManager m_tutorialManager = null;
    public ushort m_tutorialReached = 0;
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
            m_tutorialReached = data.tutotialProgress;
            if (data.tutotialProgress < 4)
                activateTutorial(data.tutotialProgress);
        }
        else
        {
            activateTutorial(0);
            FileStream stream = new FileStream(m_filePath, FileMode.Create);
            populateFakeHighScores();
            stream.Close();
        }
        m_tutorialManager.onTutorialAdvanced.AddListener(() => ++m_tutorialReached);
    }

    public string getRandomName()
    {
        string[] names = new string[]
        {
            "Thomas",
            "Sasha",
            "Arthur",
            "Bryn",
            "Joel",
            "Alex",
            "Ryan",
            "Andy",
            "Joe",
            "Sam",
            "Amy",
            "Stephie",
            "Pri",
            "Emma",
            "Xavier",
            "Rich",
            "Josie"
        };
        return names[Random.Range(0, names.Length - 1)];
    }

    void populateFakeHighScores()
    {
        HighScoreData[] data = new HighScoreData[]
        {
            new HighScoreData {
                name = getRandomName(),
                score = 15
            },
            new HighScoreData {
                name = getRandomName(),
                score = 12
            },
            new HighScoreData {
                name = getRandomName(),
                score = 9
            },
            new HighScoreData {
                name = getRandomName(),
                score = 6
            },
            new HighScoreData {
                name = getRandomName(),
                score = 3
            }
        };
        HighScoreManager.instance.loadHighScores(data);
    }

    void activateTutorial(ushort _tutorialIndex)
    {
        m_tutorialManager.m_currentTutorialPart = _tutorialIndex;
        m_tutorialManager.updateText();
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
            tutotialProgress = m_tutorialReached,
            highScoreData = HighScoreManager.instance.getHighScores()
        };
        FileStream stream = new FileStream(m_filePath, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Close();
    }
}
