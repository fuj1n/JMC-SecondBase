using UnityEngine;

public static class ScoreStore
{
    private const string ENTRY_KEY = "fuj1n.secondbase.highscore";

    private static bool isHighScore = false;
    private static int score;

    public static bool IsHighScore
    {
        get
        {
            bool ihs = isHighScore;
            isHighScore = false;
            return ihs;
        }
    }

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;

            if (value > HighScore)
                HighScore = Score;
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
            isHighScore = value > PlayerPrefs.GetInt(ENTRY_KEY);
            PlayerPrefs.SetInt(ENTRY_KEY, value);
            PlayerPrefs.Save();
        }
    }
}
