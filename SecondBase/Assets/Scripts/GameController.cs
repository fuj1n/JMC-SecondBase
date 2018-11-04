using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public int CurrentHealth { get; private set; }
    public int Score { get; private set; }

    public int maxHealth;

    private Vector3 audioSource;

    [Space(order = 0)]
    [Header("Audio", order = 1)]
    public AudioClip enemyDeathSound;
    public AudioClip enemyHitSound;
    public AudioClip lifeDownSound;
    public AudioClip shootSound;

    [Header("Prefabs")]
    public GameObject enemyDeathEffect;
    public GameObject enemyHitEffect;

    private void Awake()
    {
        EventBus.Register(this);
        Instance = this;

        CurrentHealth = maxHealth;
        audioSource = Camera.main.transform.position;
    }

    [SubscribeEvent]
    public void OnEnemyPassed(EventEnemyPassed e)
    {
        CurrentHealth--;

        if (CurrentHealth <= 0)
        {
            if (Score > ScoreStore.HighScore)
                ScoreStore.HighScore = Score;

            SceneManager.LoadScene("GameOver");
        }
        else if (lifeDownSound)
            AudioSource.PlayClipAtPoint(lifeDownSound, audioSource);
    }

    [SubscribeEvent]
    public void OnEnemyHit(EventEnemyHit e)
    {
        if (e.killed)
        {
            Score += 1;

            if (enemyDeathSound)
                AudioSource.PlayClipAtPoint(enemyDeathSound, audioSource);
            if (enemyDeathEffect)
                Instantiate(enemyDeathEffect, e.position, enemyDeathEffect.transform.rotation);
        }
        else
        {
            if (enemyHitSound)
                AudioSource.PlayClipAtPoint(enemyHitSound, audioSource);
            if (enemyHitEffect)
                Instantiate(enemyHitEffect, e.position, enemyHitEffect.transform.rotation);
        }
    }

    [SubscribeEvent]
    public void OnPlayerShoot(EventPlayerShoot e)
    {
        if (shootSound)
            AudioSource.PlayClipAtPoint(shootSound, audioSource);
    }
}
