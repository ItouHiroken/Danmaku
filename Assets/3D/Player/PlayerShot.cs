using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [Header("形態")]
    [Header("今の攻撃パターン")]
    [SerializeField] ShotForm _form;
    [Header("攻撃パターンいくつ始まってる？")]
    [SerializeField] int _nowFormStart;
    [Header("攻撃パターンいくつ終わった？")]
    [SerializeField] int _nowFormEnd;
    [Header("全部に関係すること")]
    [Header("弾いくつ出したいか")]
    [SerializeField, Range(0, 20)] int _howMany = 10;
    [Header("弾の速さ")]
    [SerializeField, Range(0, 10)] float _howEarly = 5;
    [Header("弾の生存時間")]
    [SerializeField, Range(0, 20)] float _whenDie = 10;
    [Header("直線形態")]
    [SerializeField, Range(0, 1)] float _lineRepeatInterval = 0.05f;
    [SerializeField, Range(0, 20)] int _lineRepeatTime = 10;
    [SerializeField, Range(2, 10)] int _lineWay = 6;
    [SerializeField, Range(0, 180)] float _lineWayAngle = 45;
    [Header("弾のプレハブ")]
    [SerializeField] GameObject _bullet;
    private void Update()
    {
        if (_nowFormStart == _nowFormEnd)
        {
            SummonPattern();
        }
    }
    [Flags]
    enum ShotForm
    {
        LinePlural = 1 << 0,
    }
    void SummonPattern()
    {
        _nowFormStart = 0;
        _nowFormEnd = 0;
        if (_form.HasFlag(ShotForm.LinePlural))//何way
        {
            StartCoroutine(LinePluralSummonBullet());
            _nowFormStart++;
        }
    }
    IEnumerator LinePluralSummonBullet()
    {
        for (int i = 0; i < _lineRepeatTime; i++)
        {
            for (int j = 0; j < _howMany; j++)
            {
                for (int k = 0; k < _lineWay; k++)
                {
                    float Angle = 0 - (_lineWayAngle / 2f) / 180 * Mathf.PI
                     + k * (_lineWayAngle / (_lineWay - 1)) / 180 * Mathf.PI;
                    GameObject Bullet = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                    Rigidbody BulletRB = Bullet.GetComponent<Rigidbody>();
                    BulletRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle) + new Vector3(0, 0, 1) * Mathf.Cos(Angle)).normalized * _howEarly;
                    Destroy(BulletRB, _whenDie);
                }
                yield return new WaitForSeconds(_lineRepeatInterval);
            }
            yield return new WaitForSeconds(_lineRepeatInterval);
        }
        _nowFormEnd++;
    }
}
