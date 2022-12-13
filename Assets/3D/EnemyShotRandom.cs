using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotRandom : MonoBehaviour
{
    [SerializeField] ObjectPool _objectPool;
    [SerializeField] EnemyShotController shotController;

    [Header("�S���Ɋ֌W���邱��")]
    [Header("�e�̑���")]
    [SerializeField, Range(0, 40)] float _howEarly = 3.5f;
    [Header("���@�_���̍U���p�^�[���̂��߂Ƀv���C���[���擾")]
    [SerializeField] GameObject _player;

    [Header("�����`��")]
    [SerializeField, Range(0, 1)] float _lineRepeatInterval = 0.05f;
    [SerializeField, Range(0, 180)] float _lineWayAngle = 30;
    [Header("�e�̃v���n�u")]
    [SerializeField] GameObject _bullet;
    [SerializeField] float _deltaTime;
    [HideInInspector] bool _running;
    void Update()
    {
        _deltaTime += Time.deltaTime;
        if (_deltaTime > _lineRepeatInterval)
        {
            Shot();
            Debug.Log("�o�[��");
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
    void Shot()
    {
        PooledObject pooledObject = _objectPool.GetPooledObject();
      //  pooledObject.gameObject.GetComponent<TrailRenderer>().enabled = false;
        pooledObject.transform.position = this.gameObject.transform.position;
       // pooledObject.gameObject.GetComponent<TrailRenderer>().enabled = true;


        float random = Mathf.Deg2Rad * Random.Range(0, _lineWayAngle);

        float Angle = 0 - (_lineWayAngle / 2f) / 180 * Mathf.PI;
        Rigidbody BulletRB = pooledObject.GetComponent<Rigidbody>();
        /*�P�ʉ~��X��Aim�A�P�ʉ~��Y��AimVertical*/
        Vector3 Aim = (_player.transform.position - gameObject.transform.position).normalized;
        Vector3 AimZ = new(0, 1, 0);
        Vector3 AimVertical = Vector3.Cross(Aim, AimZ);

        BulletRB.velocity = (AimVertical * Mathf.Sin(Angle + random) +
                                  Aim * Mathf.Cos(Angle + random)).normalized * _howEarly;
    }
}
