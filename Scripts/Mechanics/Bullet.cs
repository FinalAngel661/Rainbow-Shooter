using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletInitialVelocity = 200f;
    public float despawnTime = 2f;

    private Rigidbody rb;
    private float time = 0;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        rb.velocity = Vector3.zero;

        Vector3 bulletPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);
        float angle = Vector3.Angle(Input.mousePosition - bulletPosOnScreen, Vector3.right);
        float xForce = Mathf.Cos(Mathf.Deg2Rad * angle);
        float zForce = Mathf.Sin(Mathf.Deg2Rad * angle);
        if (Input.mousePosition.y < bulletPosOnScreen.y) {
            zForce *= -1;
        }
        //rb.AddForce(new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y) * bulletInitialVelocity, 0, Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y) * bulletInitialVelocity));
        rb.AddForce(new Vector3(xForce * bulletInitialVelocity, 0, zForce * bulletInitialVelocity));

        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= despawnTime)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.tag == "Boss")
        {
            if (other.gameObject.GetComponent<Boss2>().selectColor == 0)
            {
                Debug.Log("case1");
                if (this.name == "Blue Bullet(Clone)")
                {
                    other.gameObject.GetComponent<Boss2>().health -= 1;
                }
            }
            if (other.gameObject.GetComponent<Boss2>().selectColor == 1)
            {
                if (this.name == "Red Bullet(Clone)")
                {
                    other.gameObject.GetComponent<Boss2>().health -= 1;
                }
            }
            if (other.gameObject.GetComponent<Boss2>().selectColor == 2)
            {
                if (this.name == "Yellow Bullet(Clone)")
                {
                    other.gameObject.GetComponent<Boss2>().health -= 1;
                }
            }
            if (other.gameObject.GetComponent<Boss2>().selectColor == 3)
            {
                if (this.name == "Yellow Bullet(Clone)")
                {
                    other.gameObject.GetComponent<Boss2>().health -= 1;
                }
            }
        }
        if (other.tag == "Enemy")
        {
            int enemyColor = other.gameObject.GetComponent<Enemy>().enemyColor;
            //Debug.Log(enemyColor);
            switch (enemyColor)
            {
                case 0:
                    //Debug.Log("case 0");
                    if (this.name == "Blue Bullet(Clone)")
                    {
                        other.gameObject.GetComponent<Enemy>().health -= 1;
                    }
                    break;

                case 1:
                    //Debug.Log("case 1");
                    if (this.name == "Red Bullet(Clone)")
                    {
                        other.gameObject.GetComponent<Enemy>().health -= 1;
                    }
                    break;
                case 2:
                    //Debug.Log("case 2");
                    if (this.name == "Yellow Bullet(Clone)")
                    {
                        other.gameObject.GetComponent<Enemy>().health -= 1;
                    }
                    break;
            }
        }
    }
}