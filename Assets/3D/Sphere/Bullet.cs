using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float XAcceleration { get => _xAcceletation; set => _xAcceletation = value; }
    public float YAcceleration { get => _yAcceletation; set => _yAcceletation = value; }
    public float ZAcceleration { get => _zAcceletation; set => _zAcceletation = value; }

    [SerializeField] private float _xAcceletation = 0;
    [SerializeField] private float _yAcceletation = 0;
    [SerializeField] private float _zAcceletation = 0;
    Rigidbody _rigidBody;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _rigidBody.velocity = (new Vector3(_rigidBody.velocity.x + _xAcceletation,
                                           _rigidBody.velocity.y + _yAcceletation,
                                           _rigidBody.velocity.z + _zAcceletation)
                              ).normalized*_rigidBody.velocity.magnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
