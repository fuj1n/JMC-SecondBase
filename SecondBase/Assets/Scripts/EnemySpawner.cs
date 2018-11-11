using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static float EnemyMoveSpeed { get; private set; }

    public float spawnSpeed;
    public float spawnSpeedIncrease;

    public float enemyMoveSpeed;
    public float enemyMoveSpeedIncrease;

    public GameObject enemyTemplate;

    private Transform anchor;

    private float spawnTimer;

    private void Awake()
    {
        EventBus.Register(this);

        spawnTimer = spawnSpeed;
        anchor = GameController.ObjectAnchor;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0F)
        {
            spawnTimer = spawnSpeed;

            Instantiate(enemyTemplate).transform.SetParent(anchor, true);
        }

        EnemyMoveSpeed = enemyMoveSpeed;
    }

    [SubscribeEvent]
    public void OnEnemyPassed(EventEnemyPassed e)
    {
        foreach (Transform t in anchor)
            Destroy(t.gameObject);

        spawnSpeed += spawnSpeedIncrease * 5;
        enemyMoveSpeed = Mathf.Max(0.1F, enemyMoveSpeed - enemyMoveSpeedIncrease * 5);
    }

    [SubscribeEvent]
    public void OnEnemyHit(EventEnemyHit e)
    {
        if (!e.killed)
            return;

        spawnSpeed -= spawnSpeedIncrease;
        enemyMoveSpeed += enemyMoveSpeedIncrease;
    }
}