using UnityEngine;
using System.Collections;
using System;

public class AsteroidSpawner : TimerFunctionsClass
{

    public float NextSpawnTime = 1.0f;
    public GameObject AsteroidPrefab;

	// Use this for initialization
	void Start () {
        this.SetTimer(NextSpawnTime, SpawnAsteroidEvent);
        this.StartTimer();
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
    }

    public void SpawnAsteroidEvent()
    {

        // Random entre 10 et 20, * 1 ou -1
        var x = UnityEngine.Random.Range(10.0f, 20.0f) * (Mathf.Floor(UnityEngine.Random.Range(0.0f, 1.99f)) * 2 - 1);
        var y = UnityEngine.Random.Range(10.0f, 20.0f) * (Mathf.Floor(UnityEngine.Random.Range(0.0f, 1.99f)) * 2 - 1);

        //instantiate as child of AsteroidSpawner
        var a = Instantiate(AsteroidPrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
        //a.tranform.parent = this.transform;

        //Cooldown untill next random spawn
        SetTimer(NextSpawnTime, SpawnAsteroidEvent);
        StartTimer();
    }
}
