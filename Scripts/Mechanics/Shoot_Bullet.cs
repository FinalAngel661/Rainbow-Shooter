using UnityEngine;

public class Shoot_Bullet : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
	

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            Shoot();
        }
	}

    void Shoot()
    {

    }
}
