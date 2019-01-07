using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Bullet : MonoBehaviour
{

    public float bulletInitialVelocity = 200f;
    public float despawnTime = 3f;

    private Rigidbody rb;
    private float time = 0;
    private static float _angle = 0;
    private Vector3 center;
    GameObject target;
    private static float bossAngle = 0;
    private static float circle = 0;
    private static float bossAngle2 = 0;
    // Use this for initialization
    bool crossSwitch = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnEnable()
    {
        center = transform.position;
        if (this.name == "Yellow Enemy Bullet(Clone)" || this.name == "Red Enemy Bullet(Clone)")
        {
            BulletPatternNormal();
        }
        else if (this.name == "Blue Enemy Bullet(Clone)")
        {
            BulletPatternCircle();
        }
        else if (this.name == "Yellow Boss Bullet(Clone)" && SceneManager.GetActiveScene().name == "Level2")
        {
            BulletBossCircle();
        }
        else if (this.name == "Red Boss Bullets(Clone)" && SceneManager.GetActiveScene().name == "Level1")
        {
            BulletBossSpin();
        }
        else if (this.name == "Blue Boss Bullets(Clone)" && SceneManager.GetActiveScene().name == "Level3")
        {
            BulletBossSpin();
        }
        else
        {
            if (GameObject.Find("Boss4").GetComponent<Boss2>().choose == 0)
            {


                if (!crossSwitch)
                {
                    CrossUp();
                }
                else
                {
                    CrossDown();
                }
            }
            else if (GameObject.Find("Boss4").GetComponent<Boss2>().choose == 1)
            {
                BulletBossDivide();
            }
            else if (GameObject.Find("Boss4").GetComponent<Boss2>().choose == 2)
            {
                Circle();
            }
        }
    }

    void BulletPatternNormal()
    {
        target = GameObject.Find("Player");
        rb.velocity = Vector3.zero;

        float angle = Vector3.Angle(target.transform.position - transform.position, Vector3.right);
        float xForce = Mathf.Cos(Mathf.Deg2Rad * angle);
        float zForce = Mathf.Sin(Mathf.Deg2Rad * angle);
        if (target.transform.position.z < transform.position.z)
        {
            zForce *= -1;
        }
        //rb.AddForce(new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y) * bulletInitialVelocity, 0, Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y) * bulletInitialVelocity));
        rb.AddForce(new Vector3(xForce * bulletInitialVelocity, 0, zForce * bulletInitialVelocity));

        time = 0;
    }

    void BulletPatternCircle()
    {
        float pointX = Mathf.Cos(_angle);
        float pointZ = Mathf.Sin(_angle);
        rb.AddForce(new Vector3(pointX * bulletInitialVelocity, 0, pointZ * bulletInitialVelocity));
        _angle += 18;
        time = 0;
    }

    void BulletBossCircle()
    {
        float pointX = Mathf.Cos(bossAngle);
        float pointZ = Mathf.Sin(bossAngle);
        rb.AddForce(new Vector3(pointX * bulletInitialVelocity, 0, pointZ * bulletInitialVelocity));
        bossAngle += 18;
        time = 0;
    }

    void BulletBossSpin()
    {
        if (bossAngle > 360)
        {
            bossAngle = 0;
        }
        float pointX = Mathf.Cos(bossAngle);
        float pointZ = Mathf.Sin(bossAngle);
        rb.AddForce(new Vector3(pointX * bulletInitialVelocity, 0, pointZ * bulletInitialVelocity));
        bossAngle += 2;
        time = 0;
    }

    void Circle()
    {

        float pointX = Mathf.Cos(Mathf.Deg2Rad * circle);
        float pointZ = Mathf.Sin(Mathf.Deg2Rad * circle);
        rb.AddForce(new Vector3(pointX * bulletInitialVelocity, 0, pointZ * bulletInitialVelocity));
        circle += 1;
        time = 0;
    }

    void CrossUp()
    {
        crossSwitch = !crossSwitch;
        float pointX = Mathf.Cos(Mathf.Deg2Rad * circle);
        float pointZ = Mathf.Sin(Mathf.Deg2Rad * circle);
        rb.AddForce(new Vector3(pointX * bulletInitialVelocity, 0, pointZ * bulletInitialVelocity));
        time = 0;
        //crossSwitch = !crossSwitch;
        circle += 1;
    }
    void CrossDown()
    {
        //crossSwitch = !crossSwitch;
        float pointX = Mathf.Cos(Mathf.Deg2Rad * -circle);
        float pointZ = Mathf.Sin(Mathf.Deg2Rad * -circle);
        rb.AddForce(new Vector3(pointX * bulletInitialVelocity, 0, pointZ * bulletInitialVelocity));
        time = 0;
        circle += 1;
    }

    void BulletBossDivide()
    {
        float pointX = Mathf.Cos(bossAngle);
        float pointZ = Mathf.Sin(bossAngle);
        rb.AddForce(new Vector3(pointX * bulletInitialVelocity, 0, pointZ * bulletInitialVelocity));

        //StartCoroutine(BulletBossDivideHelper());
        bool leftAngle = ((bossAngle2 % 8) < 4);
        if (leftAngle)
        {
            rb.AddForce(pointX * bulletInitialVelocity * -0.6f, 0, pointZ * bulletInitialVelocity * 0.1f);
        }
        else
        {
            rb.AddForce(pointX * bulletInitialVelocity * 0.1f, 0, pointZ * bulletInitialVelocity * -0.6f);
        }

        bossAngle2 += 1;
        bossAngle = (bossAngle2 * 90.5f) % 360;
        time = 0;
    }

    IEnumerator BulletBossDivideHelper()
    { //unused
        bool leftAngle = ((bossAngle2 % 8) < 4);
        yield return new WaitForSeconds(2f);
        if (leftAngle)
        {
            rb.AddForce(-rb.velocity.z, 0, rb.velocity.x);
        }
        else
        {
            rb.AddForce(rb.velocity.z, 0, -rb.velocity.x);
        }
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (time >= despawnTime)
                this.gameObject.SetActive(false);
        }
        else
        {
            if (time >= despawnTime)
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.tag == "Player")
        {
            int playerColor = other.gameObject.GetComponent<Player>().selectColor;
            //Debug.Log(playerColor);
            switch (playerColor % 3)
            {
                case 0:
                    //Debug.Log("case 0");
                    if (this.name == "Blue Enemy Bullet(Clone)" || this.name == "Blue Boss Bullets(Clone)")
                    {
                        other.gameObject.GetComponent<Player>().TakeDamage(1);
                    }
                    break;

                case 1:
                    //Debug.Log("case 1");
                    if (this.name == "Red Enemy Bullet(Clone)" || this.name == "Red Boss Bullets(Clone)")
                    {
                        other.gameObject.GetComponent<Player>().TakeDamage(1);
                    }
                    break;
                case 2:
                    //Debug.Log("case 2");
                    if (this.name == "Yellow Enemy Bullet(Clone)" || this.name == "Yellow Boss Bullet(Clone)")
                    {
                        other.gameObject.GetComponent<Player>().TakeDamage(1);
                    }
                    break;
            }
        }
    }
}