using UnityEngine;
using System.Collections;

public class SpawnAsteroids : MonoBehaviour {

    public bool doSpawn;

	// Use this for initialization
	void Start () {
        doSpawn = true;
        InvokeRepeating("LaunchProjectile", 0, 0.4F);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
