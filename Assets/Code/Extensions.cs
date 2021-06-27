using UnityEngine;

public static class Extensions
{
    public static Vector3 RandomPointInCollider(Collider collider)
    {
        Bounds bounds = collider.bounds;
        Vector3 result = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
            );
        return collider.ClosestPoint(result);
    }
}