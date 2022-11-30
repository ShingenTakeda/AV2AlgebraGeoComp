using Microsoft.Xna.Framework;

public class CollisionWorld2D
{
    public bool CircleCicle(PhysicsCircle c1, PhysicsCircle c2)
    {
        Line2D line = new Line2D(c1.position, c2.position);
        float radSum = c1.radius + c2.radius;
        return line.lengthSqr <= radSum * radSum;
    }

    public bool CircleRectangle(PhysicsCircle c, PhysicsRectangle r)
    {
        Vector2 min = r.GetMin();
        Vector2 max = r.GetMax();

        Point2D closestPoint = c.position;

        if(closestPoint.x < min.X)
        {
            closestPoint.x = min.X;
        }
        else if(closestPoint.x > max.X)
        {
            closestPoint.x = max.X;
        }

        closestPoint.y = (closestPoint.y < min.Y) ?
        min.Y : closestPoint.y;

        closestPoint.y = (closestPoint.y > max.Y) ?
        max.Y : closestPoint.y;

        Line2D line = new Line2D(c.position, closestPoint);
        return line.lengthSqr <= c.radius * c.radius;
    }

    public bool RectangleRectangle(PhysicsRectangle r1, PhysicsRectangle r2)
    {
        Vector2 min1 = r1.GetMin();
        Vector2 max1 = r1.GetMax();
        Vector2 min2 = r2.GetMin();
        Vector2 max2 = r2.GetMax();

        bool overX = ((min2.X <= max1.X) && (min1.X <= max2.X));
        bool overY = ((min2.Y <= max1.Y) && (min1.Y <= max2.Y));

        return overX && overY;
    }

    
}