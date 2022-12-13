using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyShotLine))]
[RequireComponent(typeof(EnemyShotLineAim))]
[RequireComponent(typeof(EnemyShotLinePlural))]
[RequireComponent(typeof(EnemyShotLinePluralAim))]
[RequireComponent(typeof(EnemyShotClockwise))]
[RequireComponent(typeof(EnemyShotCounterClockwise))]

public class EnemyShotController : MonoBehaviour
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
        Wave = 1 << 6,
    }
    public event Action<bool> IsShot;
    [Header("���̍U���p�^�[��")]
    [SerializeField] CombatForm _form;
    [Header("�U���p�^�[�������n�܂��Ă�H")]
    [SerializeField] int _nowFormStart;
    [Header("�U���p�^�[�������I������H")]
    [SerializeField] int _nowFormEnd;

    [Header("�X�N���v�g����")]
    [SerializeField] EnemyShotLine _enemyShotLine;
    [SerializeField] EnemyShotLineAim _enemyShotLineAim;
    [SerializeField] EnemyShotLinePlural _enemyShotLinePlural;
    [SerializeField] EnemyShotLinePluralAim _enemyShotLinePluralAim;
    [SerializeField] EnemyShotClockwise _enemyShotClockwise;
    [SerializeField] EnemyShotCounterClockwise _enemyCounterClockwise;
    private void Update()
    {
        //    _gameTime += Time.deltaTime;
        if (_nowFormStart == _nowFormEnd)
        {
            SummonPattern();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        }
    }
    void SummonPattern()
    {
        _nowFormStart = 0;
        _nowFormEnd = 0;
        if (_form.HasFlag(CombatForm.Line))//�^��
        {
            _enemyShotLine.enabled = true;
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LineAim))//���@�_��
        {
            _enemyShotLineAim.enabled= true;
             _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LinePlural))//��way
        {
            _enemyShotLinePlural.enabled = true;
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LinePluralAim))//��way�v���X���@�_��
        {
            _enemyShotLinePluralAim.enabled = true;
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.GuruguruClockwise))//���v���
        {
            _enemyShotClockwise.enabled = true;
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.GuruguruCounterClockwise))//�����v���
        {
            _enemyCounterClockwise.enabled = true;
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.Wave))
        {
            _nowFormStart++;
        }
    }
}
