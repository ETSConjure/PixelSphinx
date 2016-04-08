using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    Vector3 center;
    public float speed;
    public float step;

	// Use this for initialization
	void Start () {
        center = new Vector3(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
            MoveObject(center);

	}

    void MoveObject(Vector3 center)
    {
        step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(transform.position, center, step);
    }
}
