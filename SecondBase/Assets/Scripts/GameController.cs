using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public int CurrentHealth { get; private set; }
    public int Score { get; private set; }

    public int maxHealth;

    private void Awake()
    {
        EventBus.Register(this);
        Instance = this;

        CurrentHealth = maxHealth;
    }

    [SubscribeEvent]
    public void OnEnemyPassed(EventEnemyPassed e)
    {
        CurrentHealth--;

        if (CurrentHealth <= 0)
            SceneManager.LoadScene(0); // TODO game over
    }

    [SubscribeEvent]
    public void OnEnemyKilled(EventEnemyKilled e)
    {
        Score += 1;
    }
}
