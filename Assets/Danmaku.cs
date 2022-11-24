using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmaku : MonoBehaviour
{
    [Header("���@�_���̍U���p�^�[���̂��߂Ƀv���C���[���擾")]
    [SerializeField] GameObject _player;
    [Header("�`��")]
    [SerializeField] CombatForm _form;
    /// <summary>360�Ŋ���鐔������Ăق���</summary>
    [Header("�e�����o��������")]
    [SerializeField, Range(0, 20)] int _howMany;
    [Header("�Q�[���̎���")]
    [SerializeField] float _gameTime = 0;
    [Header("�����`��")]
    [SerializeField, Range(0, 5)] float _lineFormChangeIntervalTime = 0.1f;
    [SerializeField, Range(0, 1)] float _lineRepeatIntervalTime = 0.05f;
    [SerializeField, Range(2, 10)] int _lineWay = 5;
    [SerializeField, Range(0, 180)] float _lineWayAngle = 30;
    [Header("���邮��`��")]
    [SerializeField, Range(0, 1)] float _guruguruFormChangeIntervalTime = 0.1f;
    [Header("�t���ό`��")]
    [SerializeField, Range(0, 1)] float _happaFormChangeIntervalTime = 0.1f;
    [Header("�č��`��")]
    [SerializeField, Range(0, 1)] float _prisonFormChangeIntervalTime = 0.1f;
    [Header("�e�̃v���n�u")]
    [SerializeField] GameObject _bullet;

    private void Start()
    {
        SummonPattern();
    }
    private void Update()
    {
        _gameTime += Time.deltaTime;
    }
  
    void SummonPattern()
    {
        switch (_form)
        {
            case CombatForm.Line:
                StartCoroutine(LineSummonBullet());
                break;
            case CombatForm.LineAim:
                StartCoroutine(LineAimSummonBullet());
                break;
            case CombatForm.LinePlural:
                StartCoroutine(LinePluralSummonBullet());
                break;
            case CombatForm.Guruguru:
                StartCoroutine(GuruguruSummonBullet());
                break;
            case CombatForm.Happa:
                StartCoroutine(HappaSummonBullet());
                break;
            case CombatForm.Prison:
                StartCoroutine(PrisonSummonBullet());
                break;
        }
    }
    IEnumerator LineSummonBullet()
    {
        for (int i = 0; i < _howMany; i++)
        {

            GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D BulletQueenRB = BulletQueen.GetComponent<Rigidbody2D>();
            BulletQueenRB.velocity = new Vector3(0, -1, 0);
            yield return new WaitForSeconds(_lineRepeatIntervalTime);
        }
        yield return new WaitForSeconds(_lineFormChangeIntervalTime);
        SummonPattern();
    }
    IEnumerator LinePluralSummonBullet()
    {
        for (int i = 0; i < _howMany; i++)
        {
            for (int j = 0; j < _lineWay; j++)
            {
                float Angle = 0 - (_lineWayAngle / 2f) / 180 * Mathf.PI
                 + j * (_lineWayAngle / (_lineWay - 1)) / 180 * Mathf.PI;
                GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
                Rigidbody2D BulletQueenRB = BulletQueen.GetComponent<Rigidbody2D>();
                BulletQueenRB.velocity = (new Vector3(1, 0, 0) * Mathf.Sin(Angle) + new Vector3(0, -1, 0) * Mathf.Cos(Angle)).normalized;
            }
            yield return new WaitForSeconds(_lineRepeatIntervalTime);
        }
        yield return new WaitForSeconds(_lineFormChangeIntervalTime);
        SummonPattern();
    }
    IEnumerator LineAimSummonBullet()
    {
        Vector3 Aim = (_player.transform.position - gameObject.transform.position).normalized;
        for (int i = 0; i < _howMany; i++)
        {
            GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D BulletQueenRB = BulletQueen.GetComponent<Rigidbody2D>();
            BulletQueenRB.velocity = Aim;
            yield return new WaitForSeconds(_lineRepeatIntervalTime);
        }
        yield return new WaitForSeconds(_lineFormChangeIntervalTime);
        SummonPattern();
    }
    IEnumerator GuruguruSummonBullet()
    {
        Debug.Log("���邮�邵�傤����I");
        for (int i = 0; (2 * Mathf.PI / _howMany) * i < 2 * Mathf.PI; i++)
        {
            float Angle = (2 * Mathf.PI / _howMany) * i;
            Debug.Log(Angle);
            GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D BulletQueenRB = BulletQueen.GetComponent<Rigidbody2D>();
            BulletQueen.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + _gameTime) * 360);
            BulletQueenRB.velocity = new Vector3(1, 0, 0) * Mathf.Sin(Angle + _gameTime) + new Vector3(0, 1, 0) * Mathf.Cos(Angle + _gameTime);
            Destroy(BulletQueen, 10f);
        }
        yield return new WaitForSeconds(_guruguruFormChangeIntervalTime);
        SummonPattern();
    }
    IEnumerator HappaSummonBullet()
    {
        ////���ꂪ���v���
        for (int i = 0; (2 * Mathf.PI / _howMany) * i < 2 * Mathf.PI; i++)
        {
            float Angle = (2 * Mathf.PI / _howMany) * i;
            Debug.Log(Angle);
            GameObject BulletChan = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D BulletChanRB = BulletChan.GetComponent<Rigidbody2D>();
            BulletChan.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + _gameTime + Mathf.PI / 2 * i) * 360);
            BulletChanRB.velocity = new Vector3(1, 0, 0) * Mathf.Sin(Angle + _gameTime + Mathf.PI / 2 * i) + new Vector3(0, 1, 0) * Mathf.Cos(Angle + _gameTime + Mathf.PI / 2 * i);
            Destroy(BulletChan, 10f);
        }
        //����͔����v���
        for (int i = 0; (2 * Mathf.PI / _howMany) * i < 2 * Mathf.PI; i++)
        {
            float Angle = (2 * Mathf.PI / _howMany) * i;
            Debug.Log(Angle);
            GameObject BulletKing = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D BulletKingRB = BulletKing.GetComponent<Rigidbody2D>();
            BulletKing.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + _gameTime + Mathf.PI / 2 * i) * 360);
            BulletKingRB.velocity = new Vector3(1, 0, 0) * Mathf.Sin(Angle - _gameTime + Mathf.PI / 2 * i) + new Vector3(0, 1, 0) * Mathf.Cos(Angle - _gameTime + Mathf.PI / 2 * i);
            Destroy(BulletKing, 10f);
        }
        yield return new WaitForSeconds(_happaFormChangeIntervalTime);
        SummonPattern();
    }
    IEnumerator PrisonSummonBullet()
    {

        yield return new WaitForSeconds(_prisonFormChangeIntervalTime);
        SummonPattern();
    }
}
enum CombatForm
{
    Line,
    LineAim,
    LinePlural,
    Guruguru,
    Happa,
    Prison,
}