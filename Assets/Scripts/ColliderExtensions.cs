using UnityEngine;

public static class ColliderExtensions
{
    public static Vector3 GetRandomPoint(this Collider collider)
    {
        return new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );
    }

    public static Vector3 GetRandomPointWithX(this Collider collider, float fixedX)
    {
        return new Vector3(
            fixedX,
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );
    }

    public static Vector3 GetRandomPointWithY(this Collider collider, float fixedY)
    {
        return new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            fixedY,
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );
    }

    public static Vector3 GetRandomPointWithZ(this Collider collider, float fixedZ)
    {
        return new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            fixedZ
        );
    }
}
