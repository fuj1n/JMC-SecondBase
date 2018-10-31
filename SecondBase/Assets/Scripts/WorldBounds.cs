using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(PixelPerfectCamera))]
public class WorldBounds : MonoBehaviour
{
    public static WorldBounds instance;

    public Bounds Bounds { get; private set; }

    private void Awake()
    {
        instance = this;

        Vector2 extents = GetExtents();

        EdgeCollider2D ec2d = gameObject.AddComponent<EdgeCollider2D>();
        ec2d.points = new Vector2[]
        {
            new Vector2(-extents.x, -extents.y),
            new Vector2(-extents.x, extents.y),
            new Vector2(extents.x, extents.y),
            new Vector2(extents.x, -extents.y),
            new Vector2(-extents.x, -extents.y)
        };

        BoxCollider2D bc2d = gameObject.AddComponent<BoxCollider2D>();
        bc2d.isTrigger = true;
        bc2d.size = extents * 2;

        Bounds = new Bounds()
        {
            center = transform.position,
            extents = extents
        };
    }

    private Vector2 GetExtents()
    {
        PixelPerfectCamera ppc = GetComponent<PixelPerfectCamera>();
        return new Vector2(ppc.refResolutionX, ppc.refResolutionY) / ppc.assetsPPU / 2F;
    }

    private void OnDrawGizmos()
    {
        Vector2 extents = GetExtents();

        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position + Vector3.forward, (Vector3)extents * 2 - (Vector3.forward * 1000F));
    }
}
