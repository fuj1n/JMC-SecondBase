using UnityEngine;

[AddComponentMenu("")]
public class ScoreStore : MonoBehaviour
{
    private const string ENTRY_KEY = "fuj1n.secondbase.highscore";

    private static bool isHighScore = false;

    public static bool IsHighScore
    {
        get
        {
            bool ihs = isHighScore;
            isHighScore = false;
            return ihs;
        }
    }

    public static int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(ENTRY_KEY);
        }
        set
        {
            isHighScore = true;
            PlayerPrefs.SetInt(ENTRY_KEY, value);
        }
    }

    private static ScoreStore Instance;

    private void Start()
    {
        if (Instance != this)
            Destroy(this);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        // We need to listen for OnApplicationQuit, so we need an object
        GameObject go = new GameObject("Score Store Listener");
        Instance = go.AddComponent<ScoreStore>();
        DontDestroyOnLoad(go);
    }
}
