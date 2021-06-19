using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameData : MonoBehaviour
{
    [System.Serializable]
    struct DataToLoad
    {
        public bool tutorialRequired;
        public HighScoreData[] highScoreData;
    }
    public static GameData instance;
    public TutorialManager m_tutorialManager = null;
    public bool m_playTutorialOnReload = false;
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
            if (data.tutorialRequired)
                activateTutorial();
        }
        else
        {
            activateTutorial();
            FileStream stream = new FileStream(m_filePath, FileMode.Create);
            populateFakeHighScores();
            stream.Close();
        }
    }

    string getRandomName()
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
            "Rich"
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

    void activateTutorial()
    {
        m_tutorialManager.m_tutorialActive = true;
        m_playTutorialOnReload = true;
    }

    private void OnApplicationQuit()
    {
        DataToLoad data = new DataToLoad()
        {
            tutorialRequired = m_tutorialManager.m_tutorialActive,
            highScoreData = HighScoreManager.instance.getHighScores()
        };
        FileStream stream = new FileStream(m_filePath, FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Close();
    }
}
