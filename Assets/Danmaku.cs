using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmaku : MonoBehaviour
{
    [SerializeField] GameObject _bullet;
    [SerializeField] float _time = 1f;
    /// <summary>360Ç≈äÑÇÍÇÈêîéöì¸ÇÍÇƒÇŸÇµÇ¢</summary>
    [SerializeField,Range(0,20)] int _howMany;
    [SerializeField] float _guruguruTime = 0;
    [SerializeField] float _happaTime = 0;
    [SerializeField] CombatForm _form;

    private void Start()
    {
        StartCoroutine(GuruguruSummonBullet());
    }
    private void Update()
    {
        _guruguruTime += Time.deltaTime;
    }
    enum CombatForm
    {
        Guruguru,
        Happa,
    }
    void SummonPattern()
    {
        switch (_form)
        {
            case CombatForm.Guruguru:
                StartCoroutine(GuruguruSummonBullet());
                break;
            case CombatForm.Happa:
                StartCoroutine(HappaSummonBullet());
                break;
        }
    }
    IEnumerator GuruguruSummonBullet()
    {
        Debug.Log("ÇÆÇÈÇÆÇÈÇµÇÂÇ§Ç©ÇÒÅI");
        for (int i = 0; (2 * Mathf.PI / _howMany) * i < 2 * Mathf.PI; i++)
        {
            float Angle = (2 * Mathf.PI / _howMany) * i;
            Debug.Log(Angle);
            GameObject BulletQueen = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D BulletChanRB = BulletQueen.GetComponent<Rigidbody2D>();
            BulletQueen.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + _guruguruTime) * 360);
            BulletChanRB.velocity = new Vector3(1, 0, 0) * Mathf.Sin(Angle + _guruguruTime) + new Vector3(0, 1, 0) * Mathf.Cos(Angle + _guruguruTime);
            Destroy(BulletQueen, 10f);
        }
        yield return new WaitForSeconds(_time);
        SummonPattern();
    }
    IEnumerator HappaSummonBullet()
    {
        ////Ç±ÇÍÇ™éûåvâÒÇË
        for (int i = 0; (2 * Mathf.PI / _howMany) * i < 2 * Mathf.PI; i++)
        {
            float Angle = (2 * Mathf.PI / _howMany) * i;
            Debug.Log(Angle);
            GameObject BulletChan = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D BulletChanRB = BulletChan.GetComponent<Rigidbody2D>();
            BulletChan.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + _guruguruTime + Mathf.PI / 2 * i) * 360);
            BulletChanRB.velocity = new Vector3(1, 0, 0) * Mathf.Sin(Angle + _guruguruTime + Mathf.PI / 2 * i) + new Vector3(0, 1, 0) * Mathf.Cos(Angle + _guruguruTime + Mathf.PI / 2 * i);
            Destroy(BulletChan, 10f);
        }
        //Ç±ÇÍÇÕîΩéûåvâÒÇË
        for (int i = 0; (2 * Mathf.PI / _howMany) * i < 2 * Mathf.PI; i++)
        {
            float Angle = (2 * Mathf.PI / _howMany) * i;
            Debug.Log(Angle);
            GameObject BulletKing = Instantiate(_bullet, gameObject.transform.position, gameObject.transform.rotation);
            Rigidbody2D BulletChanRB = BulletKing.GetComponent<Rigidbody2D>();
            BulletKing.transform.Rotate(new Vector3(0, 0, 1) * Mathf.Cos(Angle + _guruguruTime + Mathf.PI / 2 * i) * 360);
            BulletChanRB.velocity = new Vector3(1, 0, 0) * Mathf.Sin(Angle - _guruguruTime + Mathf.PI / 2 * i) + new Vector3(0, 1, 0) * Mathf.Cos(Angle - _guruguruTime + Mathf.PI / 2 * i);
            Destroy(BulletKing, 10f);
        }
        yield return new WaitForSeconds(_happaTime);
        SummonPattern();
    }
}
