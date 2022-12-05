using System.Collections.Generic;
using UnityEngine;
using System;
using Overdose.Data;

/// <summary>
/// Bullet�̃I�u�W�F�N�g�v�[�����Ǘ�����X�N���v�g
/// </summary>
public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
    [SerializeField]
    private ObjectsPoolData _objectPoolData = default;

    private List<Pool> _pool = new List<Pool>();
    private int _poolCountIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        _poolCountIndex = 0;
        CreatePool();
        //�f�o�b�O�p
        //_pool.ForEach(x => Debug.Log($"�I�u�W�F�N�g��:{x.Object.name} ���:{x.Type}"));
    }

    /// <summary>
    /// �ݒ肵���I�u�W�F�N�g�̎��,�������v�[���ɃI�u�W�F�N�g�𐶐����Ēǉ�����
    /// �ċA�Ăяo����p���Ă���
    /// </summary>
    private void CreatePool()
    {
        if (_poolCountIndex >= _objectPoolData.Data.Length)
        {
            //Debug.Log("���ׂẴI�u�W�F�N�g�𐶐����܂����B");
            return;
        }

        for (int i = 0; i < _objectPoolData.Data[_poolCountIndex].MaxCount; i++)
        {
            var bullet = Instantiate(_objectPoolData.Data[_poolCountIndex].Prefab, this.transform);
            bullet.SetActive(false);
            _pool.Add(new Pool { Object = bullet, Type = _objectPoolData.Data[_poolCountIndex].Type });
        }

        _poolCountIndex++;
        CreatePool();
    }

    /// <summary>
    /// �I�u�W�F�N�g���g�������Ƃ��ɌĂяo���֐�
    /// </summary>
    /// <param name="position">�I�u�W�F�N�g�̈ʒu���w�肷��</param>
    /// <param name="objectType">�I�u�W�F�N�g�̎��</param>
    /// <returns>���������I�u�W�F�N�g</returns>
    public GameObject UseObject(Vector2 position, PoolObjectType objectType)
    {
        foreach (var pool in _pool)
        {
            if (pool.Object.activeSelf == false && pool.Type == objectType)
            {
                pool.Object.SetActive(true);
                pool.Object.transform.position = position;
                return pool.Object;
            }
        }


        var newObj = Instantiate(Array.Find(_objectPoolData.Data, x => x.Type == objectType).Prefab, this.transform);
        newObj.transform.position = position;
        newObj.SetActive(true);
        _pool.Add(new Pool { Object = newObj, Type = objectType });

        Debug.LogWarning($"{objectType}�̃v�[���̃I�u�W�F�N�g��������Ȃ��������ߐV���ɃI�u�W�F�N�g�𐶐����܂�" +
        $"\n���̃I�u�W�F�N�g�̓v�[���̍ő�l�����Ȃ��\��������܂�" +
        $"����{objectType}�̐���{_pool.FindAll(x => x.Type == objectType).Count}�ł�");

        return newObj;
    }

    /// <summary>�I�u�W�F�N�g���v�[�����邽�߂̃N���X</summary>
    private class Pool
    {
        public GameObject Object { get; set; }
        public PoolObjectType Type { get; set; }
    }
}

public enum PoolObjectType
{
    Player01Power1 = 0,
    Player01Power2,
    Player01Power3,

    Player02Power1,
    Player02Power2,
    Player02Power3,

    Player01Bomb01,
    Player02Bomb01,

    Player01BombChild,
    Player02BombChild,

    Player01SuperPower1,
    Player01SuperPower2,
    Player01SuperPower3,

    Player02SuperPower1,
    Player02SuperPower2,
    Player02SuperPower3,

    Player01ChargePower1,
    Player01ChargePower2,
    Player01ChargePower3,

    Player02ChargePower1,
    Player02ChargePower2,
    Player02ChargePower3,

    //�{�X�̒e

    //�{�X���ʂ̒e
    BossDefaultBullet1,
    BossDefaultBullet2,
    BossDefaultBullet3,
    BossDefaultBullet4,
    BossDefaultBullet5,
    BossDefaultBullet6,
    BossDefaultBullet7,
    //�{�X5��p�̒e
    Boss05DefaultBullet,

    //�{�X1�̕K�E�Z�̒e
    Boss01SuperAttackBullet1,
    Boss01SuperAttackBullet2,
    Boss01SupetAttackBullet3,

    //�{�X2�̕K�E�Z�̒e
    Boss02SuperAttackBullet1,
    Boss02SuperAttackBullet2,
    Boss02SupetAttackBullet3,

    //�{�X3�̕K�E�Z�̒e
    Boss03SuperAttackBullet1,
    Boss03SuperAttackBullet2,
    Boss03SupetAttackBullet3,

    //�{�X4�̕K�E�Z�̒e
    Boss04SuperAttackBullet1,
    Boss04SuperAttackBullet2,
    Boss04SupetAttackBullet3,


    //�{�X5�̕K�E�Z�̒e
    Boss05SuperAttackBullet1,
    Boss05SuperAttackBullet2,
    Boss05SupetAttackBullet3,
    Boss05SuperAttackBullet4,
    Boss05SuperAttackBullet5,
    Boss05SupetAttackBullet6,


    OneUpItem,
    BombItem,
    Invincible,
    ScoreItem,
    PowerItem,

    //���u�G�̒e
    EnemyStraightUpBullet,
    EnemyStraightDownBullet,
    EnemyStraightRightBullet,
    EnemyStraightLeftBullet,

    //AutoBullet�͎����ɂƂ��Ă̏�����ɔ��
    EnemyAutoBullet,
    EnemyFastAutoBullet,
    EnemySlowAutoBullet,

    EnemyFollowBullet,
}