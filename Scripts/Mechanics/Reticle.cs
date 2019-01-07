using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour {

	private GameObject redBulletReticle;
	private GameObject yellowBulletReticle;
	private GameObject blueBulletReticle;
	private Gun gun;

	void Start () {
		redBulletReticle = transform.Find("Red Bullet (Reticle)").gameObject;
		yellowBulletReticle = transform.Find("Yellow Bullet (Reticle)").gameObject;
		blueBulletReticle = transform.Find("Blue Bullet (Reticle)").gameObject;

		gun = gameObject.GetComponentInParent(typeof(Gun)) as Gun;
	}
	
	void Update () {
		if (Input.GetKeyDown("z")) {
			redBulletReticle.SetActive(gun.gunColor == 0);
			yellowBulletReticle.SetActive(gun.gunColor == 1);
			blueBulletReticle.SetActive(gun.gunColor == 2);
		}
	}
}
