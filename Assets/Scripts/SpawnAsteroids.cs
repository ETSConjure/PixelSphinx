using UnityEngine;
using System.Collections;

public class SpawnAsteroids : MonoBehaviour {

    public GameObject myAsteroid;
    Vector3 center;
    float x;
    float y;
    float d;

	// Use this for initialization
    void Start()
    {
        center = new Vector3(0, 0);
        d = 4;
        InvokeRepeating("Spawn", 0, 0.5F);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Spawn()
    {
        GameObject instance = Instantiate(myAsteroid);
        instance.transform.position = getPositions();
    }

    Vector3 getPositions()
    {
        float theta = Random.Range(0F, 360F);
        x = center.x - Mathf.Sin(theta) * d;
        y = center.y - Mathf.Cos(theta) * d;
        return new Vector3(x, y);
    }


}
