using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int cachedScore = int.MinValue;
    private int cachedMultiplier = int.MinValue;

    private string scoreFormat;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        scoreFormat = text.text;
    }

    private void Update()
    {
        int score = GameController.Instance.Score;
        int multiplier = GameController.Instance.Multiplier;

        if (score == cachedScore && multiplier == cachedMultiplier)
            return;

        cachedScore = score;
        cachedMultiplier = multiplier;
        text.text = string.Format(scoreFormat, score, multiplier);
    }
}