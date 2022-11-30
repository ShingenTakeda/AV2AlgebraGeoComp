using Microsoft.Xna.Framework;
using System;

public struct Point2D
{
    public float x, y;

    public Point2D(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Point2D(Vector2 r)
    {
        this.x = r.X;
        this.y = r.Y;
    }
}

public struct Line2D
{
    Point2D start, end;
    public float length;
    public float lengthSqr;
    public Line2D(Point2D p1, Point2D p2)
    {
        start = p1;
        end = p2;

        length = Vector2.Distance(new Vector2(p1.x, p1.y), new Vector2(p2.x, p2.y));
        lengthSqr = Vector2.DistanceSquared(new Vector2(p1.x, p1.y), new Vector2(p2.x, p2.y));
    }

    public Line2D(Vector2 v1, Vector2 v2)
    {
        start = new Point2D(v1);
        end = new Point2D(v2);
        length = Vector2.Distance(v1, v2);
        lengthSqr = Vector2.DistanceSquared(v1, v2);
    }
}

public struct PhysicsCircle
{
    public Point2D position;
    public float radius;

    public PhysicsCircle (Point2D p)
    {
        position = p;
        radius = 1.0f;
    }

    public PhysicsCircle(Point2D p, float radius)
    {
        position = p;
        this.radius = radius;
    }

    public PhysicsCircle(Vector2 v)
    {
        position = new Point2D(v);
        radius = 1.0f;
    }

    PhysicsCircle(Vector2 v, float radius)
    {
        position = new Point2D(v);
        this.radius = radius;
    }
}

public struct PhysicsRectangle
{
    public Point2D origin;
    public Vector2 size;

    public PhysicsRectangle()
    {
        size = new Vector2(1, 1);
        origin = new Point2D(0, 0);
    }

    public PhysicsRectangle(Point2D p, Vector2 v)
    {
        origin = p;
        size = v;
    }

    public PhysicsRectangle(float x, float y, float w, float h)
    {
        origin = new Point2D(x, y);
        size = new Vector2(w, h);
    }

    public PhysicsRectangle(Vector2 origin, Vector2 size)
    {
        this.origin = new Point2D(origin);
        this.size = size;
    }

    public Vector2 GetMin()
    {
        Vector2 p1 = new Vector2(origin.x, origin.y);
        Vector2 p2 = new Vector2(origin.x, origin.y) + size;

        return new Vector2(MathF.Min(p1.X, p2.X), MathF.Min(p1.Y, p2.Y));
    }

    public Vector2 GetMax()
    {
        Vector2 p1 = new Vector2(origin.x, origin.y);
        Vector2 p2 = new Vector2(origin.x, origin.y) + size;

        return new Vector2(MathF.Max(p1.X, p2.X), MathF.Max(p1.Y, p2.Y));
    }
}

public struct Interval2D
{
    public float min;
    public float max;
}

public struct Point3D
{
    public float x, y, z;
    public Point3D(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Point3D(Vector3 v)
    {
        this.x = v.X;
        this.y = v.Y;
        this.z = v.Z;
    }
}

public struct Line3D
{
    public Point3D start;
    public Point3D end;
    public float length;
    public float lengthSqr;

    public Line3D(Point3D s, Point3D e)
    {
        start = s;
        end = e;

        length = Vector3.Distance(new Vector3(s.x, s.y, s.z), new Vector3(s.x, s.y, s.z));
        lengthSqr = Vector3.DistanceSquared(new Vector3(s.x, s.y, s.z), new Vector3(s.x, s.y, s.z));
    }

    public Line3D(Vector3 s, Vector3 e)
    {
        start = new Point3D(s);
        end = new Point3D(e);

        length = Vector3.Distance(s, e);
        lengthSqr = Vector3.DistanceSquared(s, e);
    }
}

public struct PhysicsRay
{
    public Point3D origin;
    public Vector3 direction;

    public PhysicsRay(Point3D origin)
    {
        this.origin = origin;
        direction = new Vector3(0.0f, 0.0f, 1.0f);
    }

    public PhysicsRay(Point3D origin, Vector3 direction)
    {
        this.direction = direction;
        this.origin = origin;
    }

    public void NormalizeDirection()
    {
        direction = Vector3.Normalize(direction);
    }
}

public struct PhysicsSphere
{
    public Point3D position;
    public float radius;

    public PhysicsSphere(Point3D p)
    {
        position = p;
        radius = 1.0f;
    }

    public PhysicsSphere(Point3D p, float radius)
    {
        position = p;
        this.radius = radius;
    }
}

//3D impl
public struct AABB
{
    Point3D origin;
    Vector3 size;

    public AABB(Point3D p)
    {
        origin = p;
        size = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public AABB(Point3D p, Vector3 v)
    {
        origin = p;
        size = v;
    }

    public Vector3 GetMin()
    {
        Vector3 p1 = new Vector3(this.origin.x, this.origin.y, this.origin.z) + this.size;
        Vector3 p2 = new Vector3(this.origin.x, this.origin.y, this.origin.z) - this.size;

        return new Vector3(MathF.Min(p1.X, p2.X), MathF.Min(p1.Y, p2.Y), MathF.Min(p1.Z, p2.Z));
    }

    public Vector3 GetMax()
    {
        Vector3 p1 = new Vector3(this.origin.x, this.origin.y, this.origin.z) + this.size;
        Vector3 p2 = new Vector3(this.origin.x, this.origin.y, this.origin.z) - this.size;

        return new Vector3(MathF.Max(p1.X, p2.X), MathF.Max(p1.Y, p2.Y), MathF.Max(p1.Z, p2.Z));
    }
}

public struct OBB
{
    Point3D origin;
    Vector3 size;
    Mat3 orientation;

    public OBB(Point3D p, Mat3 m)
    {
        origin = p;
        orientation = m;
        size = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public OBB(Point3D p, Vector3 v, Mat3 m)
    {
        orientation = m;
        origin = p;
        size = v;
    }

    public Vector3 GetMin()
    {
        Vector3 p1 = new Vector3(this.origin.x, this.origin.y, this.origin.z) + this.size;
        Vector3 p2 = new Vector3(this.origin.x, this.origin.y, this.origin.z) - this.size;

        return new Vector3(MathF.Min(p1.X, p2.X), MathF.Min(p1.Y, p2.Y), MathF.Min(p1.Z, p2.Z));
    }

    public Vector3 GetMax()
    {
        Vector3 p1 = new Vector3(this.origin.x, this.origin.y, this.origin.z) + this.size;
        Vector3 p2 = new Vector3(this.origin.x, this.origin.y, this.origin.z) - this.size;

        return new Vector3(MathF.Max(p1.X, p2.X), MathF.Max(p1.Y, p2.Y), MathF.Max(p1.Z, p2.Z));
    }
}

//Just an actual plane, but for physics
public struct PhysicsPlane
{
    public Vector3 normal;
    public float distance;

    public PhysicsPlane(float d)
    {
        distance = d;
        normal = new Vector3(1.0f, 0, 0);
    }

    public float PlaneEquation(Point3D p, PhysicsPlane plane)
    {
        return Vector3.Dot(new Vector3(p.x, p.y, p.z), plane.normal);
    }
}

public struct Triangle
{
    public Point3D a;
    public Point3D b;
    public Point3D c;

    public Point3D []points = new Point3D[3];
    public float []values = new float[9];

    public Triangle(Point3D a, Point3D b, Point3D c)
    {
        this.a = a;
        this.b = b;
        this.c = c;

        points[0] = a;
        points[1] = b;
        points[2] = c;

        values[0] = a.x;
        values[1] = a.y;
        values[2] = a.z;
        values[3] = b.x;
        values[4] = b.y;
        values[5] = b.z;
        values[6] = c.x;
        values[7] = c.y;
        values[8] = c.z;  
    }
}

