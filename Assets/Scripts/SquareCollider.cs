using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCollider : MyCollider
{
    public Vector2 extend
    {
        get
        {
            return transform.localScale / 2;
        }
    }

    public Vector2 Min
    {
        get
        {
            return ((Vector2)transform.position) - extend;
        }
    }
    
    public Vector2 Max
    {
        get
        {
            return ((Vector2)transform.position) + extend;
        }
    }

    public override Vector2 ClosestPoint (Vector2 point)
    {
        return new Vector2(Mathf.Clamp(point.x, Min.x, Max.x), Mathf.Clamp(point.y, Min.y, Max.y));
    }


    public override Vector2 Normal(MyCollider collider)
    {
        Vector2 closestPoint = collider.ClosestPoint (collider.transform.position);
        Vector2 direction = closestPoint - (Vector2)transform.position;
        Vector2 min = Min; 
        Vector2 max = Max;
        Vector2[] middleFaces = new Vector2[4]
        {
            new Vector2(transform.position.x, max.y), // Top
            new Vector2(min.x, transform.position.y), // Left
            new Vector2(transform.position.x, min.y), //Bottom
            new Vector2(max.x, transform.position.y) // Right
        };

        Vector2[] Normals = new Vector2[4]
       {
            new Vector2(0.0f, 1.0f),
            new Vector2(-1.0f, 0.0f),
            new Vector2(0.0f, -1.0f),
            new Vector2(1.0f, 0.0f)
       };

        int index = 0;
        float minDistanceValue = middleFaces[0].sqrMagnitude;


        for (int i=1; i < middleFaces.Length; ++i)
        {
            float distance = middleFaces[i].sqrMagnitude;

            if (distance < minDistanceValue)
            {
                minDistanceValue = distance;
                index = i;
            }
        }

        return Normals[index];

    }

}
