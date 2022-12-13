using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletNormalList : MonoBehaviour
{
    [Header("‹…‘Ì‚Ì’e")]
    [SerializeField] GameObject _normalBullet;
    [SerializeField] List<GameObject> _poolNormal;

    [Header("“ñŒÂc‚É‚Â‚È‚ª‚é’e")]
    [SerializeField] List<GameObject> _poolHigh;

    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            Summon(_normalBullet, _poolNormal, 10f);
        }
    }
    void Summon(GameObject Bullet, List<GameObject> BulletList, float WhenDie)
    {
        if (BulletList.TrueForAll(x => x.activeSelf != true))
        {
            GameObject bullet = Instantiate(Bullet, this.transform);
            _poolNormal.Add(bullet);
            StartCoroutine(Release(bullet, WhenDie));
        }
        else
        {
            Debug.Log("a");
            for (int i = 0; i < BulletList.Count; i++)
            {
                if (BulletList[i].activeSelf == false)
                {
                    BulletList[i].SetActive(true);

                }
            }
        }
    }
    IEnumerator Release(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    void Update()
    {

    }
}
