using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

//This class spawns enemies within two radii of whatever object it's attached to. Put it on an empty game object to create an area of enemies,
//attach it to the player to make enemies spawn around them, etc.
public class Radius_Enemy_Spawner : MonoBehaviour {

	public float innerRadius = 0f;
	public float outerRadius = 5f;
	public float spawnTime = 3f;
    public int numberOfEnemiesToSpawn = 20;
    public float bossTimer = 30f;
    int enemyCounter = 0;

	//you can use the same prefab multiple times
	//enemy_Prob determines how likely each enemy will appear in a spawner relative to other enemies.
	//For example, you can make it an 80% chance that a spawned enemy will be enemy1, and a 20% chance of enemy 2. Keep unused enemies at 0%.
	public GameObject enemy1Prefab;
	[Range(0f, 1f)] public float enemy1Prob = 1f;
	public GameObject enemy2Prefab;
	[Range(0f, 1f)] public float enemy2Prob = 1f;
	public GameObject enemy3Prefab;
	[Range(0f, 1f)] public float enemy3Prob = 1f;

	private EZObjectPool enemy1Pool;
    private EZObjectPool enemy2Pool;
    private EZObjectPool enemy3Pool;

	private float timer = 0f;

	void Awake () {
		//Object pool parameters: (object, name of pool, starting pool size, auto resize (should be true), instantiate immediate (should be true), shared pools)
		enemy1Pool = EZObjectPool.CreateObjectPool(enemy1Prefab, "Enemy Type 1", 100, true, true, true);
		enemy2Pool = EZObjectPool.CreateObjectPool(enemy2Prefab, "Enemy Type 2", 100, true, true, true);
		enemy3Pool = EZObjectPool.CreateObjectPool(enemy3Prefab, "Enemy Type 3", 100, true, true, true);
	}
	
	// Update is called once per frame
	void Update () {
        if (timer < bossTimer && enemyCounter < numberOfEnemiesToSpawn)
        {
            if (timer > spawnTime)
            {
                SpawnEnemy();
                timer = 0f;
            }

            timer += Time.deltaTime;
        }
        else
        {
            Debug.Log("Boss has spawned");
        }
	}

	void SpawnEnemy() {
        enemyCounter++;
		float rand = Random.Range(0f, enemy1Prob + enemy2Prob + enemy3Prob);
		float radius = Random.Range(innerRadius, outerRadius);
		int angle = Random.Range(0, 359);
		Vector3 pos = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle) * radius, 1.35f, Mathf.Cos(Mathf.Deg2Rad * angle) * radius);
		Quaternion rot = Quaternion.Euler(0, Random.Range(0, 359), 0);
		if (rand < enemy1Prob) {
			enemy1Pool.TryGetNextObject(pos, rot);
		} else if (rand < enemy1Prob + enemy2Prob) {
			enemy2Pool.TryGetNextObject(pos, rot);
		} else {
			enemy3Pool.TryGetNextObject(pos, rot);
		}
	}
}
