using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class MyCollider : MonoBehaviour
{
    
    public class OncollisionCallback : UnityEvent<MyCollider>
    {

    }
    public UnityEvent OnUpdate = new UnityEvent();
    public OncollisionCallback onCollision = new OncollisionCallback();
    [HideInInspector] public Vector2 closestPoint;

    public abstract Vector2 ClosestPoint(Vector2 point);
    public abstract Vector2 Normal(MyCollider collider);


}
