using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    public string Text
    {
        get
        {
            return text.text;
        }
        set
        {
            text.text = value;
        }
    }

    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
            Destroy(gameObject);
    }
}
