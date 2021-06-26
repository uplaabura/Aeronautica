using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBomb : MonoBehaviour
{
    public float deactivateTime = 2f;
    public float bulletSpeed = 2f;
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
    }

    void Update()
    {
        //��V*�t��
        //�H�U�O�����o�g
        //rigidbody.velocity = transform.up * bulletSpeed

        //�H�U�O�����Hz�b����bulletAngleOffset��HbulletSpeed�o�g
        bulletAngleOffset = Random.Range(-10f, 10f);
        rbody.velocity = (Quaternion.AngleAxis(bulletAngleOffset, Vector3.forward) * Vector3.up) * bulletSpeed;

        
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