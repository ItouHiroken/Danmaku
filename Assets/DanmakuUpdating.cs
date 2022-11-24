using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DanmakuUpdating : MonoBehaviour
{
    [Flags]
    enum CombatForm
    {
        Line = 1 << 0,
        LineAim = 1 << 1,
        LinePlural = 1 << 2,
        LinePluralAim = 1 << 3,
        Guruguru = 1 << 4,
        Happa = 1 << 5,
        Prison = 1 << 6,
    }
    [Header("形態")]
    [Header("今の攻撃パターン")]
    [SerializeField] CombatForm _form;
    [Header("攻撃パターンいくつ始まってる？")]
    [SerializeField] int _nowFormStart;
    [Header("攻撃パターンいくつ終わった？")]
    [SerializeField] int _nowFormEnd;

    [Header("全部に関係すること")]
    [Header("弾いくつ出したいか")]
    [SerializeField, Range(0, 20)] int _howMany;
    [Header("弾の速さ")]
    [SerializeField, Range(0, 10)] float _howEarly;
    [Header("弾の生存時間")]
    [SerializeField, Range(0, 20)] float _whenDie = 10;
    [Header("自機狙いの攻撃パターンのためにプレイヤーを取得")]
    [SerializeField] GameObject _player;

    [Header("直線形態")]
    // [SerializeField, Range(0, 5)] float _lineFormChangeInterval = 0.1f;
    [SerializeField, Range(0, 1)] float _lineRepeatInterval = 0.05f;
    [SerializeField, Range(0, 20)] int _lineRepeatTime = 10;
    [SerializeField, Range(2, 10)] int _lineWay = 5;
    [SerializeField, Range(0, 180)] float _lineWayAngle = 30;
    [Header("ぐるぐる形態")]
    // [SerializeField, Range(0, 1)] float _guruguruFormChangeInterval = 0.1f;
    [SerializeField, Range(0, 1)] float _guruguruRepeatInterval = 0.05f;//  ？
    [SerializeField, Range(0, 100)] float _guruguruRepeatTime = 50;
    [Header("葉っぱ形態")]
    // [SerializeField, Range(0, 1)] float _happaFormChangeInterval = 0.1f;
    [SerializeField, Range(0, 1)] float _happaRepeatInterval = 0.05f;
    [SerializeField, Range(0, 100)] float _happaRepeatTime = 50;
    [Header("監獄形態")]
    //  [SerializeField, Range(0, 1)] float _prisonRepeatInterval = 0.1f;
    [SerializeField, Range(0, 1)] float _prisonRepeatInterval = 0.05f;
    [Header("弾のプレハブ")]
    [SerializeField] GameObject _bullet;
    private void Update()
    {
        //    _gameTime += Time.deltaTime;
        if (_nowFormStart == _nowFormEnd)
        {
            Invoke("SummonPattern", 0);
        }
    }
    void SummonPattern()
    {
        _nowFormStart = 0;
        _nowFormEnd = 0;
        if (_form.HasFlag(CombatForm.Line))
        {
            StartCoroutine(LineSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LineAim))
        {
            StartCoroutine(LineAimSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LinePlural))
        {
            StartCoroutine(LinePluralSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LinePluralAim))
        {
            StartCoroutine(LinePluralAimSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.Guruguru))
        {
            StartCoroutine(GuruguruSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.Happa))
        {
            StartCoroutine(HappaSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.Prison))
        {
            StartCoroutine(PrisonSummonBullet());
            _nowFormStart++;
        }
    }
    IEnumerator LineSummonBullet()
    {
        for (int i = 0; i < _lineRepeatTime; i++)
        {
            for (int j = 0; j < _howMany; j++)
            {

                GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                Rigidbody2D BulletQueenRB = BulletQueen.GetComponent<Rigidbody2D>();
                BulletQueenRB.velocity = new Vector3(0, -1, 0);
                Destroy(BulletQueen, _whenDie);
                yield return new WaitForSeconds(_lineRepeatInterval);
            }
            yield return new WaitForSeconds(_lineRepeatInterval);
        }
        //   yield return new WaitForSeconds(_lineFormChangeInterval);
        _nowFormEnd++;
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
                    GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                    Rigidbody2D BulletQueenRB = BulletQueen.GetComponent<Rigidbody2D>();
                    BulletQueenRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle) + new Vector3(0, -1, 0) * Mathf.Cos(Angle)).normalized * _howEarly;
                    Destroy(BulletQueenRB, _whenDie);
                }
                yield return new WaitForSeconds(_lineRepeatInterval);
            }
            yield return new WaitForSeconds(_lineRepeatInterval);
        }
        // yield return new WaitForSeconds(_lineFormChangeInterval);
        _nowFormEnd++;
    }
    IEnumerator LinePluralAimSummonBullet()
    {
        for (int i = 0; i < _lineRepeatTime; i++)
        {
            for (int j = 0; j < _howMany; j++)
            {
                for (int k = 0; k < _lineWay; k++)
                {
                    float Angle = 0 - (_lineWayAngle / 2f) / 180 * Mathf.PI
                                + k * (_lineWayAngle / (_lineWay - 1)) / 180 * Mathf.PI;
                    GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                    Rigidbody2D BulletQueenRB = BulletQueen.GetComponent<Rigidbody2D>();
                    
                    Vector3 Aim = (_player.transform.position - gameObject.transform.position).normalized;
                    Debug.Log(Aim);
                    BulletQueenRB.velocity = (new Vector3(Aim.x, 0, 0) * Mathf.Sin(Angle) +
                                              new Vector3(0, Aim.y, 0) * Mathf.Cos(Angle)).normalized * _howEarly;
                    Destroy(BulletQueenRB, _whenDie);
                }
                yield return new WaitForSeconds(_lineRepeatInterval);
            }
            yield return new WaitForSeconds(_lineRepeatInterval);
        }
        // yield return new WaitForSeconds(_lineFormChangeInterval);
        _nowFormEnd++;
    }
    IEnumerator LineAimSummonBullet()
    {
        Vector3 Aim = (_player.transform.position - gameObject.transform.position).normalized;
        for (int i = 0; i < _howMany; i++)
        {
            GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D BulletQueenRB = BulletQueen.GetComponent<Rigidbody2D>();
            BulletQueenRB.velocity = Aim;
            Destroy(BulletQueen, _whenDie);
            yield return new WaitForSeconds(_lineRepeatInterval);
        }
        // yield return new WaitForSeconds(_lineFormChangeInterval);
        _nowFormEnd++;
    }
    IEnumerator GuruguruSummonBullet()
    {
        for (int i = 0; i < _guruguruRepeatTime; i++)
        {
            for (int j = 0; (2 * Mathf.PI / _howMany) * j < 2 * Mathf.PI; j++)
            {
                float Angle = (2 * Mathf.PI / _howMany) * j;
                Debug.Log(Angle);
                GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                Rigidbody2D BulletQueenRB = BulletQueen.GetComponent<Rigidbody2D>();
                BulletQueen.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + i * _guruguruRepeatInterval) * 360);
                BulletQueenRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle + i * _guruguruRepeatInterval)
                                        + new Vector3(0, 1, 0) * Mathf.Cos(Angle + i * _guruguruRepeatInterval)).normalized * _howEarly;
                Destroy(BulletQueen, _whenDie);
            }
            yield return new WaitForSeconds(_guruguruRepeatInterval);
        }
        _nowFormEnd++;
    }
    IEnumerator HappaSummonBullet()
    {
        for (int i = 0; i < _happaRepeatTime; i++)
        {
            ////これが時計回り
            for (int j = 0; (2 * Mathf.PI / _howMany) * j < 2 * Mathf.PI; j++)
            {
                float Angle = (2 * Mathf.PI / _howMany) * j;
                Debug.Log(Angle);
                GameObject BulletChan = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                Rigidbody2D BulletChanRB = BulletChan.GetComponent<Rigidbody2D>();
                BulletChan.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + i * _happaRepeatInterval + Mathf.PI / 2 * j) * 360);
                BulletChanRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle + i * _happaRepeatInterval + Mathf.PI / 2 * i) +
                                        new Vector3(0, 1, 0) * Mathf.Cos(Angle + i * _happaRepeatInterval + Mathf.PI / 2 * i)).normalized * _howEarly;
                Destroy(BulletChan, _whenDie);
            }
            //これは反時計回り
            for (int j = 0; (2 * Mathf.PI / _howMany) * j < 2 * Mathf.PI; j++)
            {
                float Angle = (2 * Mathf.PI / _howMany) * j;
                Debug.Log(Angle);
                GameObject BulletKing = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                Rigidbody2D BulletKingRB = BulletKing.GetComponent<Rigidbody2D>();
                BulletKing.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + i * _happaRepeatInterval + Mathf.PI / 2 * j) * 360);
                BulletKingRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle - i * _happaRepeatInterval + Mathf.PI / 2 * i) +
                                        new Vector3(0, 1, 0) * Mathf.Cos(Angle - i * _happaRepeatInterval + Mathf.PI / 2 * i)).normalized * _howEarly;
                Destroy(BulletKing, _whenDie);
            }
            yield return new WaitForSeconds(_happaRepeatInterval);
        }
        //    yield return new WaitForSeconds(_happaFormChangeInterval);
        _nowFormEnd++;
    }
    IEnumerator PrisonSummonBullet()
    {

        yield return new WaitForSeconds(_prisonRepeatInterval);
        _nowFormEnd++;
    }
}

