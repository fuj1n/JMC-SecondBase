using TMPro;
using UnityEngine;

public class FinalScoreDisplay : MonoBehaviour
{
    private void Awake()
    {
        if (!ScoreStore.IsHighScore)
        {
            GetComponent<TextMeshProUGUI>().text = "Score: " + ScoreStore.Score;
        }
    }
}
