using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public int CurrentHealth { get; private set; }
    public int Score { get; private set; }
    public int Multiplier { get; private set; } = 1;

    public int maxMultiplier = 16;
    public int maxHealth;

    public static Transform ObjectAnchor
    {
        get
        {
            if (!objectAnchor)
            {
                objectAnchor = new GameObject("Anchor").transform;
            }

            return objectAnchor;
        }
    }
    private static Transform objectAnchor;

    private Vector3 audioSource;

    [Space(order = 0)]
    [Header("Audio", order = 1)]
    public AudioClip enemyDeathSound;
    public AudioClip enemyHitSound;
    public AudioClip lifeDownSound;
    public AudioClip shootSound;
    public AudioClip powerupSound;

    [Header("Prefabs")]
    public GameObject enemyDeathEffect;
    public GameObject enemyHitEffect;

    private void Awake()
    {
        EventBus.Register(this);
        Instance = this;

        CurrentHealth = maxHealth;
        audioSource = Camera.main.transform.position;

        ObjectAnchor.SetParent(transform, true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    private void OnApplicationQuit()
    {
        SyncScore();
    }

    public void Heal(int health)
    {
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + health);
    }

    [SubscribeEvent]
    public void OnEnemyPassed(EventEnemyPassed e)
    {
        CurrentHealth--;
        Multiplier = 1;

        if (CurrentHealth <= 0)
        {
            Exit();
        }
        else if (lifeDownSound)
            AudioSource.PlayClipAtPoint(lifeDownSound, audioSource);
    }

    [SubscribeEvent]
    public void OnEnemyHit(EventEnemyHit e)
    {
        if (e.killed)
        {
            Score += 1 * Multiplier;

            if (enemyDeathSound)
                AudioSource.PlayClipAtPoint(enemyDeathSound, audioSource);
            if (enemyDeathEffect)
                Instantiate(enemyDeathEffect, e.position, enemyDeathEffect.transform.rotation).transform.SetParent(ObjectAnchor, true);

            Multiplier = Mathf.Clamp(Multiplier * 2, 1, maxMultiplier);
        }
        else
        {
            if (enemyHitSound)
                AudioSource.PlayClipAtPoint(enemyHitSound, audioSource);
            if (enemyHitEffect)
                Instantiate(enemyHitEffect, e.position, enemyHitEffect.transform.rotation).transform.SetParent(ObjectAnchor, true);

            Multiplier = Mathf.Clamp(Multiplier / 2, 1, maxMultiplier);
        }
    }

    [SubscribeEvent]
    public void OnPlayerShoot(EventPlayerShoot e)
    {
        if (shootSound)
            AudioSource.PlayClipAtPoint(shootSound, audioSource);
    }

    [SubscribeEvent]
    public void OnPowerupPickup(EventPowerupPickup e)
    {
        if (powerupSound)
            AudioSource.PlayClipAtPoint(powerupSound, audioSource);
    }

    public void SyncScore()
    {
        ScoreStore.Score = Score;
    }

    public void Exit()
    {
        SyncScore();
        SceneManager.LoadScene("GameOver");
    }
}
