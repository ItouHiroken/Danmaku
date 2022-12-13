using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShotLineAim : MonoBehaviour
{
    [SerializeField] ObjectPool _objectPool;
    [SerializeField] EnemyShotController shotController;
    [Header("弾いくつ出したいか")]
    [SerializeField, Range(0, 20)] int _howMany = 8;
    [Header("弾の速さ")]
    [SerializeField, Range(0, 40)] float _howEarly = 3.5f;
    [Header("直線形態")]
    [SerializeField, Range(0, 1)] float _lineRepeatInterval = 0.05f;
    [Header("自機狙いの攻撃パターンのためにプレイヤーを取得")]
    [SerializeField] GameObject _player;
    [Header("弾のプレハブ")]
    [SerializeField] GameObject _bullet;
    [SerializeField] float _deltaTime;
    [HideInInspector] bool _running;
    void Update()
    {
        _deltaTime += Time.deltaTime;
        if (_deltaTime > _lineRepeatInterval)
        {
            PooledObject pooledObject = _objectPool.GetPooledObject();
            Shot(pooledObject);
            Debug.Log("バーン");
            _deltaTime = 0;
        }
        if (_running)
        {
            _deltaTime -= Time.deltaTime;
        }
    }
    private void OnEnable()
    {
        shotController.IsShot += PauseResume;
    }
    private void OnDisable()
    {
        shotController.IsShot -= PauseResume;
    }
    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    public void Pause()
    {
        _running = false;
    }

    public void Resume()
    {
        _running = true;
    }
    void Shot(PooledObject pooledObject)
    {
        Vector3 Aim = (_player.transform.position - gameObject.transform.position).normalized;
        Aim.y = 0;
        for (int i = 0; i < _howMany; i++)
        {
            pooledObject.transform.position = this.gameObject.transform.position;
            Rigidbody BulletRB = pooledObject.GetComponent<Rigidbody>();
            BulletRB.velocity = Aim * _howEarly;
        }
    }
}