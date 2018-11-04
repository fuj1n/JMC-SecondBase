using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupTemplate;
    public int powerupKills = 12;
    public int specialFrequency = 6;

    private int killCounter;
    private int specialCounter;

    private void Awake()
    {
        EventBus.Register(this);
    }

    [SubscribeEvent]
    public void OnEnemyHit(EventEnemyHit e)
    {
        if (!e.killed)
            return;

        killCounter++;

        if (killCounter >= powerupKills)
        {
            killCounter = 0;
            specialCounter++;
            GameObject go = Instantiate(powerupTemplate, e.position, powerupTemplate.transform.rotation);
            go.transform.SetParent(transform, true);

            if (specialCounter >= specialFrequency)
            {
                specialCounter = 0;
                go.GetComponent<Powerup>().SetSpecial();
            }
        }
    }
}
