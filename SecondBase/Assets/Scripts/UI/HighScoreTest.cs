using UnityEngine;

public class HighScoreTest : MonoBehaviour
{
    private void Awake()
    {
        if (!ScoreStore.IsHighScore)
            gameObject.SetActive(false);
    }
}
