using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    //具體概念
    //  abstract 抽象方法，是空的方法，沒有方法實體，派(衍)生類必須以 override 實現此方法。
    //  virtual 虛擬方法，若希望或預料到基礎類別的這個方法在將來的派(衍)生類別中會被覆寫（override 或 new），則此方法必須被聲明為 virtual。
    //  override 覆寫繼承自基礎類別的virtural方法，可以理解為拆掉老房子，在原址上建新房子，老房子再也找不到了（基礎類別方法永遠調用不到了）。
    //  new 隱藏繼承自基礎類別的virtual方法，老房子還留着，在旁邊蓋個新房子，想住新房子的住新房子（作為衍生類別對象調用），想住老房子住老房子（作為基礎類別對象調用）。
    //  當派(衍)生類別中出現與基礎類別同名的方法，而此方法前面未加 override 或 new 修飾符時，編譯器會報警告，但不報錯，真正執行時等同於加了new。
    //abstract 和 virtual 的區別：
    //  abstract 方法還沒實現，連累着基礎類別也不能被實例化，除了作為一種規則或符號外沒啥用；virtual 則比較好，派(衍)生類別想覆寫就覆寫，不想覆寫就吃老子的。
    //  而且繼承再好也是少用為妙，繼承層次越少越好，派(衍)生類別新擴展的功能越少越好，virtual 深合此意。

    //override 和 new 的區別：
    //n 當派(衍)生類別對象作為基類類型使用時，override 的執行派(衍)生類別方法，new 的執行基礎類別方法。
    //n 如果作為派(衍)生類別類型調用，則都是執行 override 或 new 之後的。

    public int bulletAmount = 1;
    public float bulletAngle = 0;
    public float deactivateTime = 1f;
    public float speed = 10f;
    public float offsetAngle = 0;

    protected Rigidbody2D rbody;
    //public GameObject explosionPrefab;

    protected virtual void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        Invoke("DeactivateGameObject", deactivateTime);
        OffsetThenPush();
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullets")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider, true);
        }
    }

    //之前在Update()裡面用的Movement();
    //void Movement()
    //{
    //    Vector3 bulletPosition = transform.position;
    //    bulletPosition.y += bulletSpeed * Time.deltaTime;
    //    transform.position = bulletPosition;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
    //    Destroy(gameObject);
    //}

    protected virtual void OffsetThenPush()
    {
        float _offset = Random.Range(-offsetAngle, offsetAngle);
        //方向*速度
        //以下是直直發射
        //rigidbody.velocity = transform.up * bulletSpeed

        //以下是角度隨z軸旋轉bulletAngleOffset後以bulletSpeed發射
        rbody.velocity = (Quaternion.AngleAxis(_offset, Vector3.forward) * Vector3.up) * speed;
    }

    protected virtual void DeactivateGameObject()
    {
        //gameObject.SetActive(false);
        //Destroy(gameObject);
        ObjectPool.Instance.QueueIn(gameObject);
    }
}
