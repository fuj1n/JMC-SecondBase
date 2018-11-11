using DG.Tweening;
using TMPro;
using UnityEngine;

public class MultiplierEffectNumber : MonoBehaviour
{
    public float speed = 2.5F;
    public float aliveTime = 2F;

    public GameObject root;

    private Vector2 direction;

    private void Awake()
    {
        if (!GameController.Instance)
            return;

        TextMeshPro tmp = GetComponent<TextMeshPro>();
        tmp.text = string.Format(tmp.text, GameController.Instance.Multiplier);

        direction = new Vector2(Random.Range(-1F, 1F), Random.Range(-1F, 1F));

        tmp.DOFade(0F, aliveTime).OnComplete(() => Destroy(root));
    }

    private void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
}
