using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] float _moveSpeed = 5;
    [Header("ジャンプ力")]
    [SerializeField] float _jumpPower = 5;
    [HideInInspector] int _jumpCount = 0;
    [Header("RB")]
    [SerializeField] Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _jumpCount = 0;
    }
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump") && _jumpCount == 0)//まだ二段ジャンプ実装する予定がないから
        {
            Debug.Log("ジャンプ押したよ");
            _rigidBody.AddForce(new Vector3(0, _jumpPower, 0), ForceMode.Impulse);
            _jumpCount++;
        }
        //上が平面移動
        //下がジャンプ
        _rigidBody.velocity = (Vector3.forward * v + Vector3.right * h).normalized * _moveSpeed
                                + new Vector3(0, _rigidBody.velocity.y, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _jumpCount = 0;
        }
    }
}
