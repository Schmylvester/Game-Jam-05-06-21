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
        if (File.Exists(m_filePath))
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
            stream.Close();
        }
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
