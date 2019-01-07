using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Broken : MonoBehaviour {

	private float timer = 0;

	// Use this for initialization
	void Start () {
		//destructible effect
		transform.GetChild(Random.Range(1,200)).GetComponent<Rigidbody>().AddForce(10,-100,10);
		transform.GetChild(Random.Range(1,200)).GetComponent<Rigidbody>().AddForce(10,-100,0);
		transform.GetChild(Random.Range(1,200)).GetComponent<Rigidbody>().AddForce(10,-100,-10);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.childCount == 0) {
			Destroy(gameObject);
		}
		//destroy one mini block every 0.004 seconds
		else {
			while (timer > 3.004) {
				Destroy(transform.GetChild(Random.Range(0,transform.childCount)).gameObject);
				timer -= 0.004f;
			}
			
		}
		timer += Time.deltaTime;
	}
}
