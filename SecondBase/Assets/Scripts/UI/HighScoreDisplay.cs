using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = string.Format(text.text, ScoreStore.HighScore);
    }
}
