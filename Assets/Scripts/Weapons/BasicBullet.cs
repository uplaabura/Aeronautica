using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float deactivateTime = 1f;
    public float bulletSpeed = 9f;
    public float bulletAngleOffset;

    private Rigidbody2D rbody;
    //public GameObject explosionPrefab;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("DeactivateGameObject", deactivateTime);
        bulletAngleOffset = Random.Range(-10f, 10f);
    }

    void Update()
    {
        //��V*�t��
        //�H�U�O�����o�g
        //rigidbody.velocity = transform.up * bulletSpeed

        //�H�U�O�����Hz�b����bulletAngleOffset��HbulletSpeed�o�g
        rbody.velocity = (Quaternion.AngleAxis(bulletAngleOffset, Vector3.forward) * Vector3.up) * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullets")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider, true);
        }
    }

    //���e�bUpdate()�̭��Ϊ�Movement();
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

    void DeactivateGameObject()
    {
        //gameObject.SetActive(false);
        //Destroy(gameObject);
        ObjectPool.Instance.QueueIn(gameObject);
    }


}