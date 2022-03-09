using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : MyCollider
{
    public float radius = 0.5f;

    public override Vector2 ClosestPoint(Vector2 point)
    {
        return (point - (Vector2)transform.position).normalized  * radius + (Vector2)transform.position; 
    }

    public override Vector2 Normal(MyCollider collider)
    {
        return (collider.ClosestPoint(transform.position) - (Vector2)transform.position).normalized; 
    }

}
