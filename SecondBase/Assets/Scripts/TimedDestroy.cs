using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public float time = 2F;

    private void Awake()
    {
        Invoke("OnTimer", time);
    }

    private void OnTimer()
    {
        Destroy(gameObject);
    }
}
