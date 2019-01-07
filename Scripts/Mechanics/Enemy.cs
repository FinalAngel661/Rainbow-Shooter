using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class Enemy : MonoBehaviour
{
    public int health = 1;
    public int enemyColor;
    public float shotDelay = 0.8f;
    public GameObject target;
    private EZObjectPool redBulletPool;
    private EZObjectPool yellowBulletPool;
    private EZObjectPool blueBulletPool;
    public GameObject redBulletPrefab;
    public GameObject yellowBulletPrefab;
    public GameObject blueBulletPrefab;
    public GameObject destroyedObject;
    private float time = 0f;
    float angle, angle1, angle2;
    float pointx, pointy;
    float radius = .1f;
    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Player");
        //enemyColor = Random.Range(0, 2);
        switch (enemyColor)
        {
            case 0:
                GetComponent<Renderer>().material.color = Color.red;
                redBulletPool = EZObjectPool.CreateObjectPool(redBulletPrefab, "Red Enemy Bullets", 100, true, true, true);
                break;
            case 1:
                GetComponent<Renderer>().material.color = Color.yellow;
                yellowBulletPool = EZObjectPool.CreateObjectPool(yellowBulletPrefab, "Yellow Enemy Bullets", 100, true, true, true);
                break;
            case 2:
                GetComponent<Renderer>().material.color = Color.blue;
                blueBulletPool = EZObjectPool.CreateObjectPool(blueBulletPrefab, "Blue Enemy Bullets", 100, true, true, true);
                break;
        }
    }

    void OnEnable()
    {
        health = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = target.transform.position - transform.position;
        if (distance.magnitude > 5)
        {
            Move();
        }
        else if (time > shotDelay)
        {
            switch (enemyColor)
            {
                case 0:
                    ShootRed();
                    break;
                case 1:
                    //case2Flag = true;
                    StartCoroutine(Case2Coroutine());
                    break;
                    
                case 2:
                    //ShootBlue();
                    StartCoroutine(Case3Coroutine());
                    break;
            }
            time = 0;
        }
        Death();
        time += Time.deltaTime;
    }

    IEnumerator Case2Coroutine()
    {
        for (int i = 0; i < 20; i++)
        {
            ShootYellow();
                yield return new WaitForSeconds(0.1f);
        }
        //case2Flag = false;
        //time = 0;
    }

    IEnumerator Case3Coroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            ShootBlue();
            yield return new WaitForSeconds(.1f);
        }
        //yield return new WaitForSeconds(5f);

    }

    public void Death()
    {
        if (health <= 0)
        {
            Instantiate(destroyedObject, transform.position, transform.rotation);
            this.gameObject.SetActive(false);
        }
    }

    void Move()
    {
        Vector3 newPos = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), Time.deltaTime * 5f);
        Vector3 _direction = (target.transform.position - transform.position).normalized;
        Quaternion lookDirection = Quaternion.LookRotation(_direction);
        lookDirection.x = 0;
        lookDirection.z = 0;
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 20f);
        transform.position = newPos;
    }

    void ShootRed()
    {
        redBulletPool.TryGetNextObject(transform.position, transform.rotation);
    }

    void ShootYellow()
    {
        yellowBulletPool.TryGetNextObject(transform.position, transform.rotation);
    }

    void ShootBlue()
    {
        blueBulletPool.TryGetNextObject(transform.position, transform.rotation);
    }
}
