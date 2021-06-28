using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float deactivateTime = 1f;
    public float speed = 9f;
    public float offsetAngle = 10;

    private Rigidbody2D rbody;
    //public GameObject explosionPrefab;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("DeactivateGameObject", deactivateTime);
        OffsetThenPush();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

    void OffsetThenPush()
    {
        float _offset = Random.Range(-offsetAngle, offsetAngle);
        //方向*速度
        //以下是直直發射
        //rigidbody.velocity = transform.up * bulletSpeed

        //以下是角度隨z軸旋轉bulletAngleOffset後以bulletSpeed發射
        rbody.velocity = (Quaternion.AngleAxis(_offset, Vector3.forward) * Vector3.up) * speed;
    }

    void DeactivateGameObject()
    {
        //gameObject.SetActive(false);
        //Destroy(gameObject);
        ObjectPool.Instance.QueueIn(gameObject);
    }


}