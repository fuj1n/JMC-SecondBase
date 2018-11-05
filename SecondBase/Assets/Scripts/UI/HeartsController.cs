using System;
using UnityEngine;
using UnityEngine.UI;

public class HeartsController : MonoBehaviour
{
    private const int SPRITES_LENGTH = 5;

    private RectTransform transform2D;

    public Sprite[] sprites = { };

    private Image[] hearts;

    private int cachedHealth;

    private void Start()
    {
        transform2D = GetComponent<RectTransform>();

        SetupHearts();
    }

    private void Update()
    {
        if (Mathf.CeilToInt(GameController.Instance.maxHealth / 2F) != hearts.Length)
            SetupHearts();

        int health = GameController.Instance.CurrentHealth;
        if (health == cachedHealth)
            return;
        cachedHealth = health;

        float partialHealth = health / 2F;

        for (int i = 0; i < hearts.Length; i++)
        {
            int offset = 0;

            if (i + 1 == hearts.Length && GameController.Instance.maxHealth % 2 != 0)
            {
                offset = 3;
            }

            if (partialHealth >= 1)
            {
                partialHealth -= 1F;
                hearts[i].sprite = sprites[2];
            }
            else if (partialHealth >= 0.5F)
            {
                partialHealth -= 0.5F;
                hearts[i].sprite = sprites[offset + 1];
            }
            else
            {
                hearts[i].sprite = sprites[offset + 0];
            }
        }
    }

    private void SetupHearts()
    {
        if (hearts != null)
            foreach (Image heart in hearts)
                Destroy(heart.gameObject);

        cachedHealth = int.MinValue;

        transform2D.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 16F);
        transform2D.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 16F * Mathf.CeilToInt(GameController.Instance.maxHealth / 2F));

        hearts = new Image[Mathf.CeilToInt(GameController.Instance.maxHealth / 2F)];

        for (int i = 0; i < hearts.Length; i++)
        {
            GameObject go = new GameObject("Heart " + i);

            RectTransform rect = go.AddComponent<RectTransform>();
            rect.SetParent(transform2D, false);
            rect.sizeDelta = new Vector2(16F, 16F);

            hearts[i] = go.AddComponent<Image>();
            hearts[i].sprite = sprites[2];
        }
    }

    private void OnValidate()
    {
        // Ensure sprite array is x elements long during edit-time
        if (sprites.Length != SPRITES_LENGTH)
            Array.Resize(ref sprites, SPRITES_LENGTH);
    }
}
