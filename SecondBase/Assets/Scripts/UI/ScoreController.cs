using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int cachedScore = int.MinValue;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        int score = GameController.Instance.Score;

        if (score == cachedScore)
            return;

        cachedScore = score;
        text.text = score.ToString();
    }
}
