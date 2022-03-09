using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRigidBody : MonoBehaviour
{
    //cookbook 360 updatefunction 3,4 on collision 8,10,11

    public float mass = 1.0f;
    public float bounce = 0.7f;

    private const float Friction = 0.95f;

    public Vector2 _force;
    private Vector2 _oldPosition;

    private MyCollider _collider;

    public Vector2 Velocity
    {
        get
        {
            return (Vector2)transform.position - _oldPosition;
        }
    }

    private void Awake()
    {
        _collider = GetComponent<MyCollider>();

        _collider.OnUpdate.AddListener(OnMyUpdate);
        _collider.onCollision.AddListener(OnMyCollision);
        _oldPosition = transform.position;
    }

    public void AddForce(Vector2 force)
    {
        _force += force;
    }

    public void AddAcceleration(Vector2 force)
    {
        _force += force/Time.fixedDeltaTime;
    }

    private void OnMyUpdate()
    {
        Vector2 velocity = Velocity;
        float deltaTimeSquare = Time.fixedDeltaTime * Time.fixedDeltaTime;

        _oldPosition = transform.position;
        AddForce(PhysicsEngine.Gravity * mass);
        transform.position += (Vector3)(velocity * Friction + _force * deltaTimeSquare);
        _force = Vector2.zero;
    }


    private void OnMyCollision (MyCollider collider)
    {
        Vector2 velocity = Velocity;
        Vector2 normal = collider.Normal(_collider);

        transform.position += (Vector3)normal * 0.003f;
        Vector2 vn = normal * Vector2.Dot(normal, velocity);
        Vector2 vt = Velocity - vn;
        _oldPosition = (Vector2)transform.position - (vt - vn * bounce);
    }
}
