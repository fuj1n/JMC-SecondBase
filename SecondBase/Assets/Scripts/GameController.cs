using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public int CurrentHealth { get; private set; }

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
    }
}
