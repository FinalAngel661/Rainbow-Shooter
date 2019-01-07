using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    Quaternion lockRotation;
    public GameObject target;
	// Use this for initialization
	void Start () {
        lockRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.rotation = new Quaternion(95f, 0, 0, 0);
        transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
    }
}
