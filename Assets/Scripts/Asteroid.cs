using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    Vector3 center;
    public float speed;
    public float step;

	// Use this for initialization
    void Start()
    {
        speed = Random.Range(0.1F, 2F);
        print(speed);
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

    //collider must be set as "isTrigger" in unity for this method to work
    void OnCollisionEnter2D(Collider otherCol)
    {


        if (otherCol.gameObject.tag == "Player")
        {
            //Stun the player
        }
        if (otherCol.gameObject.tag == "Wedge")
        {
            var pmgr = FindObjectOfType<PlanetManager>();
            pmgr.PushWedge(otherCol.gameObject.transform.rotation.z);
        }
    }

}
