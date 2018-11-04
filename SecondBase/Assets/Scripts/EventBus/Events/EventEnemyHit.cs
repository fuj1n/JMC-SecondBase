using UnityEngine;

public struct EventEnemyHit : IEventBase
{
    public bool killed;
    public Vector3 position;

    public EventEnemyHit(bool killed, Vector3 position)
    {
        this.killed = killed;
        this.position = position;
    }
}
