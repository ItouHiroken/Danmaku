using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �p�x�o���Ăق���
/// </summary>
public class EnemyShot : MonoBehaviour
{
    [Flags]
    enum CombatForm
    {
        Line = 1 << 0,
        LineAim = 1 << 1,
        LinePlural = 1 << 2,
        LinePluralAim = 1 << 3,
        GuruguruClockwise = 1 << 4,
        GuruguruCounterClockwise = 1 << 5,
        Happa = 1 << 6,
        Wave = 1 << 7,
    }
    [Header("�`��")]
    [Header("���̍U���p�^�[��")]
    [SerializeField] CombatForm _form;
    [Header("�U���p�^�[�������n�܂��Ă�H")]
    [SerializeField] int _nowFormStart;
    [Header("�U���p�^�[�������I������H")]
    [SerializeField] int _nowFormEnd;

    [Header("�S���Ɋ֌W���邱��")]
    [Header("�e�����o��������")]
    [SerializeField, Range(0, 20)] int _howMany;
    [Header("�e�̑���")]
    [SerializeField, Range(0, 40)] float _howEarly;
    [Header("�e�̐�������")]
    [SerializeField, Range(0, 20)] float _whenDie = 10;
    [Header("���@�_���̍U���p�^�[���̂��߂Ƀv���C���[���擾")]
    [SerializeField] GameObject _player;

    [Header("�����`��")]
    [SerializeField, Range(0, 1)] float _lineRepeatInterval = 0.05f;
    [SerializeField, Range(0, 20)] int _lineRepeatTime = 10;
    [SerializeField, Range(2, 10)] int _lineWay = 5;
    [SerializeField, Range(0, 180)] float _lineWayAngle = 30;
    [Header("���邮��`��")]
    [SerializeField, Range(0, 1)] float _guruguruRepeatInterval = 0.05f;
    [SerializeField, Range(0, 100)] float _guruguruRepeatTime = 50;
    [Header("�t���ό`��")]
    [SerializeField, Range(0, 1)] float _happaRepeatInterval = 0.05f;
    [SerializeField, Range(0, 100)] float _happaRepeatTime = 50;
    [Header("�g�`��")]
    [SerializeField, Range(0, 1)] float _waveRepeatInterval = 0.05f;
    [Header("�e�̃v���n�u")]
    [SerializeField] GameObject _bullet;
    private void Update()
    {
        //    _gameTime += Time.deltaTime;
        if (_nowFormStart == _nowFormEnd)
        {
            SummonPattern();
        }
    }
    void SummonPattern()
    {
        _nowFormStart = 0;
        _nowFormEnd = 0;
        if (_form.HasFlag(CombatForm.Line))//�^��
        {
            StartCoroutine(LineSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LineAim))//���@�_��
        {
            StartCoroutine(LineAimSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LinePlural))//��way
        {
            StartCoroutine(LinePluralSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LinePluralAim))//��way�v���X���@�_��
        {
            StartCoroutine(LinePluralAimSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.GuruguruClockwise))//���v���
        {
            StartCoroutine(GuruguruClockwiseSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.GuruguruCounterClockwise))//�����v���
        {
            StartCoroutine(GuruguruCounterClockwiseSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.Happa))//�t����
        {
            StartCoroutine(HappaSummonBullet());
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.Wave))
        {
            StartCoroutine(WaveSummonBullet());
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
                Rigidbody BulletQueenRB = BulletQueen.GetComponent<Rigidbody>();
                BulletQueenRB.velocity = new Vector3(0, 0, 1).normalized * _howEarly;
                Destroy(BulletQueen, _whenDie);
                yield return new WaitForSeconds(_lineRepeatInterval);
            }
            yield return new WaitForSeconds(_lineRepeatInterval);
        }
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
                    Rigidbody BulletQueenRB = BulletQueen.GetComponent<Rigidbody>();
                    BulletQueenRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle) + new Vector3(0, 0, -1) * Mathf.Cos(Angle)).normalized * _howEarly;
                    Destroy(BulletQueenRB, _whenDie);
                }
                yield return new WaitForSeconds(_lineRepeatInterval);
            }
            yield return new WaitForSeconds(_lineRepeatInterval);
        }
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
                    Rigidbody BulletQueenRB = BulletQueen.GetComponent<Rigidbody>();
                    /*�P�ʉ~��X��Aim�A�P�ʉ~��Y��AimVertical*/
                    Vector3 Aim = (_player.transform.position - gameObject.transform.position).normalized;
                    Vector3 AimZ = new(0, 1, 0);
                    Vector3 AimVertical = Vector3.Cross(Aim, AimZ);

                    BulletQueenRB.velocity = (AimVertical * Mathf.Sin(Angle) +
                                              Aim * Mathf.Cos(Angle)).normalized * _howEarly;
                    Destroy(BulletQueenRB, _whenDie);
                }
                yield return new WaitForSeconds(_lineRepeatInterval);
            }
            yield return new WaitForSeconds(_lineRepeatInterval);
        }
        _nowFormEnd++;
    }
    IEnumerator LineAimSummonBullet()
    {
        Vector3 Aim = (_player.transform.position - gameObject.transform.position).normalized;
        for (int i = 0; i < _howMany; i++)
        {
            GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody BulletQueenRB = BulletQueen.GetComponent<Rigidbody>();
            BulletQueenRB.velocity = Aim;
            Destroy(BulletQueen, _whenDie);
            yield return new WaitForSeconds(_lineRepeatInterval);
        }
        _nowFormEnd++;
    }
    IEnumerator GuruguruClockwiseSummonBullet()
    {
        for (int i = 0; i < _guruguruRepeatTime; i++)
        {
            for (int j = 0; (2 * Mathf.PI / _howMany) * j < 2 * Mathf.PI; j++)
            {
                float Angle = (2 * Mathf.PI / _howMany) * j;
                Debug.Log(Angle);
                GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                Rigidbody BulletQueenRB = BulletQueen.GetComponent<Rigidbody>();
                BulletQueen.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + i * _guruguruRepeatInterval) * 360);
                BulletQueenRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle + i * _guruguruRepeatInterval)
                                        + new Vector3(0, 0, 1) * Mathf.Cos(Angle + i * _guruguruRepeatInterval)).normalized * _howEarly;
                Destroy(BulletQueen, _whenDie);
            }
            yield return new WaitForSeconds(_guruguruRepeatInterval);
        }
        _nowFormEnd++;
    }
    IEnumerator GuruguruCounterClockwiseSummonBullet()
    {
        for (int i = 0; i < _guruguruRepeatTime; i++)
        {
            for (int j = 0; (2 * Mathf.PI / _howMany) * j < 2 * Mathf.PI; j++)
            {
                float Angle = (2 * Mathf.PI / _howMany) * j;
                GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                Rigidbody BulletQueenRB = BulletQueen.GetComponent<Rigidbody>();
                BulletQueen.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + i * _guruguruRepeatInterval) * 360);
                BulletQueenRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle + i * _guruguruRepeatInterval * -1)
                                        + new Vector3(0, 0, 1) * Mathf.Cos(Angle + i * _guruguruRepeatInterval * -1)).normalized * _howEarly;
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
            ////���ꂪ���v���
            for (int j = 0; (2 * Mathf.PI / _howMany) * j < 2 * Mathf.PI; j++)
            {
                float Angle = (2 * Mathf.PI / _howMany) * j;
                Debug.Log(Angle);
                GameObject BulletChan = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                Rigidbody BulletChanRB = BulletChan.GetComponent<Rigidbody>();
                BulletChan.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + i * _happaRepeatInterval + Mathf.PI / 2 * j) * 360);
                BulletChanRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle + i * _happaRepeatInterval + Mathf.PI / 2 * i) +
                                        new Vector3(0, 0, 1) * Mathf.Cos(Angle + i * _happaRepeatInterval + Mathf.PI / 2 * i)).normalized * _howEarly;
                Destroy(BulletChan, _whenDie);
            }
            //����͔����v���
            for (int j = 0; (2 * Mathf.PI / _howMany) * j < 2 * Mathf.PI; j++)
            {
                float Angle = (2 * Mathf.PI / _howMany) * j;
                Debug.Log(Angle);
                GameObject BulletKing = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                Rigidbody BulletKingRB = BulletKing.GetComponent<Rigidbody>();
                BulletKing.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + i * _happaRepeatInterval + Mathf.PI / 2 * j) * 360);
                BulletKingRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle - i * _happaRepeatInterval + Mathf.PI / 2 * i) +
                                        new Vector3(0, 0, 1) * Mathf.Cos(Angle - i * _happaRepeatInterval + Mathf.PI / 2 * i)).normalized * _howEarly;
                Destroy(BulletKing, _whenDie);
            }
            yield return new WaitForSeconds(_happaRepeatInterval);
        }
        _nowFormEnd++;
    }
    IEnumerator WaveSummonBullet()
    {

        yield return new WaitForSeconds(_waveRepeatInterval);
        _nowFormEnd++;
    }
}

