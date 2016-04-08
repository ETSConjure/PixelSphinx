using UnityEngine;
using System.Collections;
using System;

public class AsteroidSpawner : TimerFunctionsClass
{

    public float NextSpawnTime = 1.0f;
    public GameObject AsteroidPrefab;
    public bool GenerationVersLesjoueurs = false;  //random lorsque false;

	// Use this for initialization
	void Start ()
	{

	    if (GenerationVersLesjoueurs) NextSpawnTime = 3 * NextSpawnTime;
        this.SetTimer(NextSpawnTime, SpawnAsteroidEvent);
        this.StartTimer();
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
    }

    public void SpawnAsteroidEvent()
    {
        if (!GenerationVersLesjoueurs)
        {
            // Random entre 10 et 20, * 1 ou -1
            var x = UnityEngine.Random.Range(10.0f, 20.0f)*(Mathf.Floor(UnityEngine.Random.Range(0.0f, 1.99f))*2 - 1);
            var y = UnityEngine.Random.Range(10.0f, 20.0f)*(Mathf.Floor(UnityEngine.Random.Range(0.0f, 1.99f))*2 - 1);

            //instantiate as child of AsteroidSpawner
            var a = Instantiate(AsteroidPrefab, new Vector3(x, y, 0.0f), Quaternion.identity);
            //a.tranform.parent = this.transform;

        }
        else
        {
            var players =  GameObject.FindGameObjectsWithTag("Player");
            var planet = FindObjectOfType<PlanetManager>();
            foreach (var p in players)
            {
                var playerTheta = Mathf.Atan2(p.transform.position.y, p.transform.position.x); 
                var angle = ( 360.0f + (((playerTheta * 180)) / Mathf.PI)) % 360;  ///TODO : a changer pour p.theta
                print("angle:" + angle);
                Instantiate(AsteroidPrefab, planet.GetPlanetCoordinatesFromPlayerXY(angle, UnityEngine.Random.Range(10f,15f)), Quaternion.identity);
            }

        }


        //Cooldown untill next random spawn
        SetTimer(NextSpawnTime, SpawnAsteroidEvent);
        StartTimer();
    }
}
