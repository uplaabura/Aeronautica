using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    //Fighter variables
    public float fighterSpeed = 5f;
    public float min_X, max_X, min_Y, max_Y;
    //

    //Weapon variables
    public Transform weaponLauncher;

    public GameObject[] weapons;

    private float attackCharger;
    public float attackSpeed = 0.1f;

    public GameObject fighterBullets;
    //

    void Start()
    {
        //fighterBullets = Instantiate(Resources.Load("Bullets Built", typeof(GameObject))) as GameObject;

        attackCharger = attackSpeed;

        //以下這個會在這個script附著的gameObject(在這邊是Fighter)的子目錄找Attack Point
        weaponLauncher = gameObject.transform.Find("Weapon Launcher");

        //以下這個會在整個scene找Weapon Launcher這個GameObject
        //weaponLauncher = GameObject.Find("Weapon Launcher").transform;
}

void Update()
    {
        Move();
        Attack();
        WPchange();
    }

    void Move()
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
            if (attackCharger >= attackSpeed)
            {
                attackCharger = 0f;

                GameObject bullet = ObjectPool.Instance.QueueOut(fighterBullets);
                bullet.transform.position = weaponLauncher.position;
            }
        }
    }
    void WPchange()
    {
        if (Input.GetKeyDown("1"))
        {
            fighterBullets = weapons[0];
        }
        if (Input.GetKeyDown("2"))
        {
            fighterBullets = weapons[1];
        }
    }
}
