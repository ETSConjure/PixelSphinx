using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AsteroidSpawner : TimerFunctionsClass
{

    public float NextSpawnTime = 1.0f;
    public GameObject AsteroidPrefab1;
    public GameObject AsteroidPrefab2;
    public GameObject AsteroidPrefab3;
    public GameObject AsteroidPrefab4;
    private List<GameObject> AsteroidPrefabTypes = new List<GameObject>();

    public bool GenerationVersLesjoueurs = false;  //random lorsque false;

	// Use this for initialization
	public void Start ()
	{

	    if (!AsteroidPrefab1 || !AsteroidPrefab2 || !AsteroidPrefab3 || !AsteroidPrefab4)
	    {
           Destroy(this.gameObject);
           print("WARNING un type d'asteroide n'est pas defini dans les prefab. Vérifier l'objet avec un component AsteroidSpawner");
	        return;
	    }
        AsteroidPrefabTypes.Add(AsteroidPrefab1);
        AsteroidPrefabTypes.Add(AsteroidPrefab2);
        AsteroidPrefabTypes.Add(AsteroidPrefab3);
        AsteroidPrefabTypes.Add(AsteroidPrefab4);


        if (GenerationVersLesjoueurs) NextSpawnTime = 3 * NextSpawnTime;
        this.SetTimer(NextSpawnTime, SpawnAsteroidEvent);
        this.StartTimer();
    }

    // Update is called once per frame
    public void Update () {
        base.Update();
    }

    public void SpawnAsteroidEvent()
    {
        if (!GenerationVersLesjoueurs)
        {
            // Random entre 10 et 20, * 1 ou -1
            var x = UnityEngine.Random.Range(10.0f, 20.0f)*(Mathf.Floor(UnityEngine.Random.Range(0.0f, 1.99f))*2 - 1);
            var y = UnityEngine.Random.Range(10.0f, 20.0f)*(Mathf.Floor(UnityEngine.Random.Range(0.0f, 1.99f))*2 - 1);


            //0-3
            var AsteroidType = Mathf.RoundToInt(Mathf.Floor(UnityEngine.Random.Range(0f, 3.999f)));

            //instantiate as child of AsteroidSpawner
            var a = Instantiate(AsteroidPrefabTypes[AsteroidType], new Vector3(x, y, 0.0f), Quaternion.identity);
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

                var AsteroidType = Mathf.RoundToInt(Mathf.Floor(UnityEngine.Random.Range(0f, 3.999f)));
				float direction = (Mathf.Floor(UnityEngine.Random.Range(0.0f, 1.99f)) * 2 - 1); 
		
                Instantiate(AsteroidPrefabTypes[AsteroidType], 
                            direction*planet.GetPlanetCoordinatesFromPlayerXY(angle, UnityEngine.Random.Range(10f,15f)), 
                            Quaternion.identity);
            }

        }


        //Cooldown untill next random spawn
        SetTimer(NextSpawnTime, SpawnAsteroidEvent);
        StartTimer();
    }
}
