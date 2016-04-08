using UnityEngine;
using System.Collections;

public class SpawnAsteroids : MonoBehaviour {

    public GameObject myAsteroid;
    public Vector3 initialPosition;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Spawn", 0, 0.5F);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Spawn()
    {
        GameObject instance = Instantiate(myAsteroid);
        instance.transform.position = new Vector3(10, 10, 0);
    }
}
