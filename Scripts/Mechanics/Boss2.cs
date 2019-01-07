using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;
using UnityEngine.SceneManagement;

public class Boss2 : MonoBehaviour {

    private EZObjectPool yellowBulletPool;
    private EZObjectPool redBulletPool;
    private EZObjectPool blueBulletPool;
    public GameObject yellowBulletPrefab;
    public GameObject redBulletPrefab;
    public GameObject blueBulletPrefab;
    public GameObject destroyedObject;
    public int health = 15;
    public float time = 0f;
    public float shotDelay = .8f;
    public int selectColor = 0;
    bool started = false;
    int count = 0;
    public int choose = 0;
    Vector3 newHeight;
    Scene scene;
    // Use this for initialization
    void Start () {
        scene = SceneManager.GetActiveScene();
        newHeight = new Vector3(transform.position.x, GameObject.Find("Player").transform.position.y, transform.position.z);
        Debug.Log(scene.name);
        if (scene.name == "Level1")
        {
            GetComponent<Renderer>().material.color = Color.red;
            selectColor = 0;
        }
        else if (scene.name == "Level2")
        {
            GetComponent<Renderer>().material.color = Color.yellow;
            selectColor = 1;
        }
        else if (scene.name == "Level3")
        {
            GetComponent<Renderer>().material.color = Color.blue;
            selectColor = 2;
        }
        else if (scene.name == "Level4")
        {
            GetComponent<Renderer>().material.color = Color.red;
            selectColor = 3;
        }
        yellowBulletPool = EZObjectPool.CreateObjectPool(yellowBulletPrefab, "Yellow Boss Bullets", 500, true, true, true);
        redBulletPool = EZObjectPool.CreateObjectPool(redBulletPrefab, "Red Boss Bullets", 500, true, true, true);
        blueBulletPool = EZObjectPool.CreateObjectPool(blueBulletPrefab, "Blue Boss Bullets", 500, true, true, true);
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(selectColor);
        Death();
        time += Time.deltaTime;
    }
    void FixedUpdate()
    {
        //Death();
        if (time > shotDelay && SceneManager.GetActiveScene().name == "Level2")
        {
            StartCoroutine(firstPattern());
            time = 0;
        }
        else if (SceneManager.GetActiveScene().name == "Level1")
        {
            /*if (time > shotDelay)
            {
                //StartCoroutine(SpinningShot());
                ShootRed();
                time = 0;
            }*/
            ShootRed();
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (time > shotDelay)
            {
                StartCoroutine(SpinningShot());
                time = 0;
            }
        }
        else
        {

            choose = Random.Range(0, 3);
            if (time > shotDelay)
            {
                StartCoroutine(DivideShot());
                //StartCoroutine(CircleShield());
                time = 0;
            }
            //time += Time.deltaTime;
        }
    }

    void ShootYellow()
    {
        
        yellowBulletPool.TryGetNextObject(newHeight, transform.rotation);
    }

    void ShootRed()
    {
        redBulletPool.TryGetNextObject(newHeight, transform.rotation);
    }

    void ShootBlue()
    {
        blueBulletPool.TryGetNextObject(newHeight, transform.rotation);
    }


    IEnumerator firstPattern()
    {
        for (int i = 0; i < 360; i++)
        {
            ShootYellow();
            yield return new WaitForSeconds(.1f);
            //yield return new WaitForSeconds(0);
        }
        count++;
    }

    IEnumerator DivideShot()
    {
        //count++;
        for (int i = 0; i < 360; i += 2)
        {
            if (choose == 1)
            {
                ShootBlue();
            }
            else
            {
                ShootRed();
            }
        }
        yield return new WaitForSeconds(3f);
    }

    IEnumerator SpinningShot()
    {
        //count++;
        for (int i = 0; i < 360; i += 2)
        {
            ShootBlue();
        }
        yield return new WaitForSeconds(3f);

    }
    void Death()
    {
        if (health <= 0)
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
