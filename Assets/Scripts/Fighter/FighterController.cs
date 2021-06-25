using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public float fighterSpeed = 5f;
    public float min_X, max_X, min_Y, max_Y;

    private float attackCharger;
    public float attackSpeedThreshold = 0.1f;

    public GameObject fighterBullets;
    public Transform attackPoint;

    // Start is called before the first frame update
    void Start()
    {
        attackCharger = attackSpeedThreshold;

        //fighterBullets = Instantiate(Resources.Load("Bullets Built", typeof(GameObject))) as GameObject;

        //這個會在整個scene找Attack Point
        //attackPoint = GameObject.Find("Attack Point").transform;
        //這個會在這個script附著的gameObject(在這邊是Fighter)的子目錄找Attack Point
        attackPoint = gameObject.transform.Find("Attack Point");
    }

    // Update is called once per frame
    void Update()
    {
        FighterMovement();
        Attack();
    }

    void FighterMovement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += Time.deltaTime * fighterSpeed;

            if (newPosition.x > max_X)
            {
                newPosition.x = max_X;
            }

            transform.position = newPosition;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.x -= Time.deltaTime * fighterSpeed;

            if (newPosition.x < min_X)
            {
                newPosition.x = min_X;
            }

            transform.position = newPosition;
        }

        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.y += Time.deltaTime * fighterSpeed;

            if (newPosition.y > max_Y)
            {
                newPosition.y = max_Y;
            }

            transform.position = newPosition;
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.y -= Time.deltaTime * fighterSpeed;

            if (newPosition.y < min_Y)
            {
                newPosition.y = min_Y;
            }

            transform.position = newPosition;
        }
    }

    void Attack()
    {
        //attackCharger += Time.deltaTime;
        //if (attackCharger >= attackSpeedThreshold)
        //{
        //    canAttack = true;
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (canAttack)
        //    {
        //        canAttack = false;
        //        attackCharger = 0f;

        //        Instantiate(fighterBullets, attackPoint.position, Quaternion.identity);

        //        //play sound FX
        //    }
        //}
        attackCharger += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (attackCharger >= attackSpeedThreshold)
            {
                attackCharger = 0f;

                GameObject bullet = ObjectPool.Instance.QueueOut(fighterBullets);
                bullet.transform.position = attackPoint.position;

            }
        }
    }
}
