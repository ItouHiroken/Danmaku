using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �_���[�W��^�������A�S�Ă�EnemyBullet�������Ȃ񂩂ɂȂ��ăv���C���[�ɔ�ԁB
/// �X�R�A�𑝂₷�B
/// �G�̍U�����~�߂�B
/// �S�Ă�EnemyBullet��ێ����Ă����K�v������
/// </summary>
public class GameManager : MonoBehaviour
{
    public List<GameObject> BulletGameObject;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _enemy;
    void Damaging()
    {
        for (int i = 0; i < BulletGameObject.Count; i++)
        {
            BulletGameObject[0].gameObject.GetComponent<Rigidbody>().velocity =
                (_player.gameObject.transform.position - _enemy.gameObject.transform.position)/2;
            BulletGameObject.RemoveAt(0);
        }
        _enemy.GetComponent<EnemyShot>().enabled = false;
    }
}
