using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maawarukun : MonoBehaviour
{
    [SerializeField] float _mawaruHiritsu;
    public int Speed { get => _speed; set => _speed = value; }
    int _speed;
    [SerializeField] GameObject _summoner;
    Rigidbody2D _myRigidbody;
    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Mawaaru();
    }
    void Mawaaru()
    {
        Vector3 BokuToSummonerNoDistance = gameObject.transform.position - _summoner.transform.position;
        Vector3 BokuNoZ = gameObject.transform.position + new Vector3(0, 0, 5);

        Vector3 BokuToSummonerNoVertical = Vector3.Cross(BokuToSummonerNoDistance, BokuNoZ).normalized;

        Vector3 BokuNoVerocity = _myRigidbody.velocity;
        _myRigidbody.velocity = (BokuNoVerocity + BokuToSummonerNoVertical * _mawaruHiritsu).normalized * _speed;

    }
}
