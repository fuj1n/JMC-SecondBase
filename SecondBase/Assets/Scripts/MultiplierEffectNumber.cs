using TMPro;
using UnityEngine;

public class MultiplierEffectNumber : MonoBehaviour
{
    public float speed = 2.5F;

    private Vector2 direction;

    private void Start()
    {
        TextMeshPro tmp = GetComponent<TextMeshPro>();
        tmp.text = string.Format(tmp.text, GameController.Instance.Multiplier);

        direction = new Vector2(Random.Range(-1F, 1F), Random.Range(-1F, 1F));
    }

    private void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
}
