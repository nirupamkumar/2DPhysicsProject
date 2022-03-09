using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public static readonly Vector2 Gravity = new Vector2 (0, -9.81f);

    private void FixedUpdate()
    {
        MyCollider[] myColliders = FindObjectsOfType<MyCollider>();
        for (int e=0; e<myColliders.Length; e++)
        {
            myColliders[e].OnUpdate.Invoke();
            for (int h=e+1; h<myColliders.Length; h++)
            {
                if (CheckCollision(myColliders[e], myColliders[h]))
                {
                    myColliders[e].onCollision.Invoke(myColliders[h]);
                    myColliders[h].onCollision.Invoke(myColliders[e]);
                    Debug.Log("Objects collided");
                }
            }
        }
    }

    private bool CheckCollision(MyCollider myCollider1, MyCollider myCollider2)
    {
        CircleCollider circleCollider1 = myCollider1 as CircleCollider;
        CircleCollider circleCollider2 = myCollider2 as CircleCollider;

        if (circleCollider1 != null)
        {
            if (circleCollider2 != null)
            {
                return TestCollision(circleCollider1, circleCollider2);
            }
            else
            {
                return TestCollision(myCollider2 as SquareCollider, circleCollider1);
            }
        }
        else
        {
            SquareCollider squareCollider1 = myCollider1 as SquareCollider;
            //SquareCollider squareCollider2 = myCollider2 as SquareCollider;

            if (circleCollider2 != null)
            {
                return TestCollision(squareCollider1, circleCollider2);
            }
            else
            {
                SquareCollider squareCollider2 = myCollider2 as SquareCollider;
                return TestCollision(squareCollider1, squareCollider2);
            }
        }

        
    }

    private bool TestCollision(CircleCollider circleCollider1, CircleCollider circleCollider2)
    {
        float sumRadius = circleCollider1.radius + circleCollider2.radius;
        Vector2 distanceVector = circleCollider2.transform.position - circleCollider1.transform.position;
        return distanceVector.sqrMagnitude <= sumRadius * sumRadius;
    }

    private bool TestCollision(SquareCollider squareCollider1, SquareCollider squareCollider2)
    {
        Vector2 closestPoint = squareCollider1.ClosestPoint(squareCollider2.transform.position);
        return (closestPoint.x <= squareCollider2.Max.x && closestPoint.x >= squareCollider2.Min.x &&
       closestPoint.y <= squareCollider2.Max.y && closestPoint.y >= squareCollider2.Min.y);
    }

    private bool TestCollision(SquareCollider squareCollider, CircleCollider circleCollider)
    {
        Vector2 closestPoint = squareCollider.ClosestPoint(circleCollider.transform.position);
        Vector2 distance = closestPoint - (Vector2)circleCollider.transform.position;
        return distance.sqrMagnitude <= circleCollider.radius * circleCollider.radius;
    }
}
