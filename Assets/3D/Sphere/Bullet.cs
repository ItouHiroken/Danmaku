using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour
{
    public Vector3 Acceleration { get => _acceletation; set => _acceletation = value; }
    [SerializeField] private Vector3 _acceletation = new Vector3(0, 0, 0);

    [SerializeField] GameManager _gameManager;
    Rigidbody _rigidBody;
    float _time;
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.BulletGameObject.Add(gameObject);
    }
    private void Update()
    {
        _time += Time.deltaTime;
        _rigidBody.velocity = (new Vector3( _acceletation.x * Mathf.Sin(_time) + _rigidBody.velocity.x ,
                                            _acceletation.y * Mathf.Sin(_time) + _rigidBody.velocity.y ,
                                            _acceletation.z * Mathf.Cos(_time) + _rigidBody.velocity.z)).normalized * _rigidBody.velocity.magnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            _gameManager.BulletGameObject.Remove(gameObject);
        }
    }
}
