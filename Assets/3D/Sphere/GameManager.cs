using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダメージを与えた時、全てのEnemyBulletが★かなんかになってプレイヤーに飛ぶ。
/// スコアを増やす。
/// 敵の攻撃を止める。
/// 全てのEnemyBulletを保持しておく必要がある
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
