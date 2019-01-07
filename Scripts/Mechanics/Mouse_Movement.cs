using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Movement : MonoBehaviour
{

    // Use this for initialization
    public float speed = 1f;
    float timeStarted;
    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

    }

    void PlayerMove()
    {
        //Thanks to Ryan Singh at StackOverflow for part of this code
        if (Input.GetMouseButton(0))
        {
            timeStarted = Time.time;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 newPos = Vector3.MoveTowards(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), Time.deltaTime * speed);
                Vector3 _direction = (hit.point - transform.position).normalized;
                Quaternion lookDirection = Quaternion.LookRotation(_direction);
                lookDirection.x = 0;
                lookDirection.z = 0;
                gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 20f);
                transform.position = newPos;

            }
        }
    }
}