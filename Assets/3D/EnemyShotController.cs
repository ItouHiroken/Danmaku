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
    [Header("今の攻撃パターン")]
    [SerializeField] CombatForm _form;
    [Header("攻撃パターンいくつ始まってる？")]
    [SerializeField] int _nowFormStart;
    [Header("攻撃パターンいくつ終わった？")]
    [SerializeField] int _nowFormEnd;

    [Header("スクリプトたち")]
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
        if (_form.HasFlag(CombatForm.Line))//真下
        {
            _enemyShotLine.enabled = true;
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LineAim))//自機狙い
        {
            _enemyShotLineAim.enabled= true;
             _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LinePlural))//何way
        {
            _enemyShotLinePlural.enabled = true;
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.LinePluralAim))//何wayプラス自機狙い
        {
            _enemyShotLinePluralAim.enabled = true;
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.GuruguruClockwise))//時計回り
        {
            _enemyShotClockwise.enabled = true;
            _nowFormStart++;
        }
        if (_form.HasFlag(CombatForm.GuruguruCounterClockwise))//反時計回り
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
