using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using EZObjectPools;

public class Gun : MonoBehaviour {

	public GameObject redBulletPrefab;
    public GameObject yellowBulletPrefab;
    public GameObject blueBulletPrefab;
    public GameObject redBulletReticlePrefab;
    public GameObject yellowBulletReticlePrefab;
    public GameObject blueBulletReticlePrefab;
    public int gunColor = 0; //0 = red, 1 = yellow, 2 = blue
    public float shotDelay = 0.3f;
    public Image RedArrow, YellowArrow, BlueArrow;

    private EZObjectPool redBulletPool;
    private EZObjectPool yellowBulletPool;
    private EZObjectPool blueBulletPool;
    //private GameObject bullet;
    private float time = 99f;

    private GameObject redBulletReticle;
	private GameObject yellowBulletReticle;
	private GameObject blueBulletReticle;

	void Awake () {
		//Object pool parameters: (object, name of pool, starting pool size, auto resize (should be true), instantiate immediate (should be true), shared pools)
		redBulletPool = EZObjectPool.CreateObjectPool(redBulletPrefab, "Red Bullets", 100, true, true, true);
		yellowBulletPool = EZObjectPool.CreateObjectPool(yellowBulletPrefab, "Yellow Bullets", 100, true, true, true);
		blueBulletPool = EZObjectPool.CreateObjectPool(blueBulletPrefab, "Blue Bullets", 100, true, true, true);
	}

	void Start () {
		RedArrow.enabled = true;
        YellowArrow.enabled = false;
        BlueArrow.enabled = false;
		//redBulletReticle = Instantiate(redBulletReticlePrefab, transform.position, Quaternion.identity);
		//yellowBulletReticle = Instantiate(yellowBulletReticlePrefab, transform.position, Quaternion.identity);
		//blueBulletReticle = Instantiate(blueBulletReticlePrefab, transform.position, Quaternion.identity);
		//redBulletReticle.transform.SetParent(transform);
		//yellowBulletReticle.transform.SetParent(transform);
		//blueBulletReticle.transform.SetParent(transform);
		//UpdateReticle();
	}
	
	void Update () {
		if (Input.GetKeyDown("z")) {
			gunColor = (gunColor + 1) % 3;
			RedArrow.enabled = gunColor == 0;
            YellowArrow.enabled = gunColor == 1;
            BlueArrow.enabled = gunColor == 2;
            //UpdateReticle();
           // FindObjectOfType<AudioManager>().Play("SwitchColor");
        }

		//can fire every 0.3 seconds while right click is held
		if (Input.GetButton("Fire2") && time > shotDelay)
        {
          //  FindObjectOfType<AudioManager>().Play("PlayerShoot");
            if (gunColor == 0) {
            	ShootRed();
            } else if (gunColor == 1) {
            	ShootYellow();
            } else {
            	ShootBlue();
            }
            time = 0;
        }

        time += Time.deltaTime;
	}

	void ShootRed() {
		redBulletPool.TryGetNextObject(transform.position, transform.rotation);
        //if (redBulletPool.TryGetNextObject(transform.position, transform.rotation, out bullet)) {
        //	print("APPLE");
        //	bullet.GetComponent<Bullet>().bulletInitialVelocity = 50f;
        //}
    }

	void ShootYellow() {
		yellowBulletPool.TryGetNextObject(transform.position, transform.rotation);
	}

	void ShootBlue() {
		blueBulletPool.TryGetNextObject(transform.position, transform.rotation);
	}

	void UpdateReticle() {
		redBulletReticle.SetActive(gunColor == 0);
		yellowBulletReticle.SetActive(gunColor == 1);
		blueBulletReticle.SetActive(gunColor == 2);
	}
}