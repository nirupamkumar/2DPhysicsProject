using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    private Vector3 _initialPosition;
    private MyRigidBody _rb;
    private bool _isBalloon = false;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<MyRigidBody>();
        _rb.AddAcceleration(new Vector2(30.0f, 0.0f));
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(Application.loadedLevel);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            _isBalloon = !_isBalloon;
        }
        if (_isBalloon)
        {
            _rb.AddForce(-PhysicsEngine.Gravity * _rb.mass + new Vector2(0.0f, 0.3f)); // Remove Gravity + Add floating
            _rb.AddAcceleration(new Vector2(Random.Range(-0.5f, 0.5f), 0.0f));
        }
    }
}
