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
        Bounds worldBounds = WorldBounds.instance.Bounds;
        hoverX = worldBounds.center.x + Random.Range(-worldBounds.extents.x + 1F, worldBounds.extents.x - 1F);

        transform.position = new Vector3(hoverX, worldBounds.center.y + worldBounds.extents.y + 1F, 10F);

        isBinary = Random.Range(0, 100) < 50;
        num = Random.Range(0, 256);

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera"))
            Destroy(gameObject);
    }
}
