using Microsoft.Xna.Framework;

class VectorOperations
{ 
    public Vector2 Project(Vector2 length, Vector2 direction)
    {
        float dot = Vector2.Dot(length, direction);
        float magSqr = Vector2.DistanceSquared(length,direction);
        return direction * (dot / magSqr);
    }

    public Vector3 Project(Vector3 length, Vector3 direction)
    {
        float dot = Vector3.Dot(length, direction);
        float magSqr = Vector3.DistanceSquared(length,direction);
        return direction * (dot / magSqr);
    }
    public Vector2 Reflection(Vector2 vec, Vector2 normal)
    {
        float d = Vector2.Dot(vec, normal);
        return vec - normal * (Vector2.Dot(vec, normal * 2.0f));
    }

    public Vector3 Reflection(Vector3 vec, Vector3 normal)
    {
        float d = Vector3.Dot(vec, normal);
        return vec - normal * (Vector3.Dot(vec, normal * 2.0f));
    }
}