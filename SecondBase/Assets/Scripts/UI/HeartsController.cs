using System;
using UnityEngine;
using UnityEngine.UI;

public class HeartsController : MonoBehaviour
{
    private RectTransform transform2D;

    public Sprite[] sprites = { };

    private Image[] hearts;

    private int cachedHealth = -1337;

    private void Start()
    {
        transform2D = GetComponent<RectTransform>();

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

    private void Update()
    {
        int health = GameController.Instance.CurrentHealth;
        if (health == cachedHealth)
            return;
        cachedHealth = health;

        float partialHealth = health / 2F;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (partialHealth >= 1)
            {
                partialHealth -= 1F;
                hearts[i].sprite = sprites[2];
            }
            else if (partialHealth >= 0.5F)
            {
                partialHealth -= 0.5F;
                hearts[i].sprite = sprites[1];
            }
            else
            {
                hearts[i].sprite = sprites[0];
            }
        }
    }

    private void OnValidate()
    {
        // Ensure sprite array is 3 elements long during edit-time
        if (sprites.Length != 3)
            Array.Resize(ref sprites, 3);
    }
}
