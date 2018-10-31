using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimedFader : MonoBehaviour
{
    public float fadeoutTime = 120F;

    private void Awake()
    {
        foreach (Graphic g in GetComponentsInChildren<Graphic>())
            g.DOColor(g.color - new Color(0, 0, 0, 1), fadeoutTime);
    }
}
