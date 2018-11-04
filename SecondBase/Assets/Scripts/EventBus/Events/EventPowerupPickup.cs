public struct EventPowerupPickup : IEventBase
{
    public Powerup.PowerupType type;

    public EventPowerupPickup(Powerup.PowerupType type)
    {
        this.type = type;
    }
}
