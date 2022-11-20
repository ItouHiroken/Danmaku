using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmaku : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] float _time = 1f;
    /// <summary>360Ç≈äÑÇÍÇÈêîéöì¸ÇÍÇƒÇŸÇµÇ¢</summary>
    [SerializeField] int _howMany;
    [SerializeField] float _guruguruTime = 0;

    private void Start()
    {
        StartCoroutine(SummonBullet());
    }
    private void Update()
    {
        _guruguruTime += Time.deltaTime;
    }
    IEnumerator SummonBullet()
    {
        Debug.Log("ÇµÇÂÇ§Ç©ÇÒÅI");
        for (int i = 0; (2 * Mathf.PI / _howMany) * i < 2 * Mathf.PI; i++)
        {
            float Angle = (2 * Mathf.PI / _howMany) * i;
            Debug.Log(Angle);
            GameObject BulletChan = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D BulletChanRB = BulletChan.GetComponent<Rigidbody2D>();
            BulletChanRB.velocity = new Vector3(1, 0, 0) * Mathf.Sin(Angle + _guruguruTime) + new Vector3(0, 1, 0) * Mathf.Cos(Angle + _guruguruTime);
            Destroy(BulletChan, 10f);
        }

        yield return new WaitForSeconds(_time);
        StartCoroutine(SummonBullet());
    }
}
