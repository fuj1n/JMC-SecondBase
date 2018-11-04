using System.Globalization;
using System.Linq;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float hoverScale = .2F;

    public PowerupSpriteMap[] sprites = { };
    public Sprite specialSprite;

    private PowerupType type;

    private float hoverX;
    private float time;

    private void Awake()
    {
        hoverX = transform.position.x;

        PowerupType[] types = System.Enum.GetValues(typeof(PowerupType)).Cast<PowerupType>().ToArray();
        type = types[Random.Range(0, types.Length)];

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr)
            sr.sprite = sprites[(int)type].sprite;
    }

    public void SetSpecial()
    {
        type = (PowerupType)(-1);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr)
            sr.sprite = specialSprite;
    }

    private void Update()
    {
        time += Time.deltaTime;
        transform.SetAxisPosition(Utility.Axis.X, hoverX + Mathf.Sin(time) * hoverScale);

        while (time >= 2 * Mathf.PI)
            time -= 2 * Mathf.PI;

        transform.position += Vector3.down * EnemySpawner.EnemyMoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);

            switch (type)
            {
                case (PowerupType)(-1):
                    GameController.Instance.maxHealth++;
                    GameController.Instance.Heal(1);
                    break;
                case PowerupType.HEALTH_UP:
                    GameController.Instance.Heal(1);
                    break;
            }

            EventBus.Post(new EventPowerupPickup(type));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
            Destroy(gameObject);
    }

    private void OnValidate()
    {
        PowerupType[] values = System.Enum.GetValues(typeof(PowerupType)).Cast<PowerupType>().ToArray();

        if (sprites.Length != values.Length)
            System.Array.Resize(ref sprites, values.Length);

        for (int i = 0; i < sprites.Length; i++)
        {
            TextInfo culture = CultureInfo.CurrentCulture.TextInfo;
            sprites[i].id = culture.ToTitleCase(values[i].ToString().ToLower().Replace('_', ' '));
        }
    }

    [System.Serializable]
    public struct PowerupSpriteMap
    {
        public string id;
        public Sprite sprite;
    }

    public enum PowerupType
    {
        HEALTH_UP
    }
}
