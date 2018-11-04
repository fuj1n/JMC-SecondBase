using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3F;

    public float hoverScale = .2F;

    private float hoverX;
    private float time;

    private bool isBinary = false;
    private int num;

    private void Awake()
    {
        Bounds worldBounds = WorldBounds.Instance.Bounds;
        hoverX = worldBounds.center.x + Random.Range(-worldBounds.extents.x + 1F, worldBounds.extents.x - 1F);

        transform.position = new Vector3(hoverX, worldBounds.center.y + worldBounds.extents.y + 1F, 10F);

        isBinary = Random.Range(0, 100) < 50;

        // Simple random generator with tendency to pick lower numbers
        num = Random.Range(0, 32);
        if (num == 31)
            num = Random.Range(16, 128);
        if (num == 127)
            num = Random.Range(64, 256);

        if (isBinary)
            GetComponentInChildren<TextMeshPro>().text = System.Convert.ToString(num, 2).PadLeft(8, '0');
        else
            GetComponentInChildren<TextMeshPro>().text = num.ToString();


    }

    private void Update()
    {
        time += Time.deltaTime;
        transform.SetAxisPosition(Utility.Axis.X, hoverX + Mathf.Sin(time) * hoverScale);

        while (time >= Mathf.PI)
            time -= 2 * Mathf.PI;

        Debug.Log(time + " " + Mathf.Sin(time));

        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    private void EnemyPassed()
    {
        Destroy(gameObject);
        EventBus.Post(new EventEnemyPassed());
    }

    private void EnemyKilled()
    {
        Destroy(gameObject);
        EventBus.Post(new EventEnemyKilled());
    }

    private void Compare(string s)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Killzone"))
            EnemyPassed();
        else if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            Compare(bullet.Text);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
            EnemyPassed();
    }
}
