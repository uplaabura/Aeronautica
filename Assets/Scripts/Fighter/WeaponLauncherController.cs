using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLauncherController : MonoBehaviour
{
    public Transform weaponLauncher;

    public GameObject[] weapons;

    private float attackCharger;
    public float attackSpeed = 0.1f;

    public GameObject fighterBullets;

    // Start is called before the first frame update
    void Start()
    {
        attackCharger = attackSpeed;

        //�o�ӷ|�b�o��script���۪�gameObject(�b�o��OFighter)���l�ؿ���Attack Point
        weaponLauncher = gameObject.transform.Find("Weapon Launcher");
        
        //�o�ӷ|�b���scene��Weapon Launcher�o��GameObject
        //weaponLauncher = GameObject.Find("Weapon Launcher").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        
        if (Input.GetKeyDown("1"))
        {
            fighterBullets = weapons[0];
        }
        if (Input.GetKeyDown("2"))
        {
            fighterBullets = weapons[1];
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
}
