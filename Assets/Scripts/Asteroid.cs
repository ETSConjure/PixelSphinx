using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    Vector3 center;
    public float speed;
    public float step;

    // Use this for initialization
    public void Start()
    {
        speed = Random.Range(1.8F, 3F);
        // print(speed);
        center = new Vector3(0, 0);
	}

    // Update is called once per frame
    public void Update () {
            MoveObject(center);

	}

    public void MoveObject(Vector3 center)
    {
        step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(transform.position, center, step);
    }

    //collider must be set as "isTrigger" in unity for this method to work
    public void OnTriggerEnter(Collider otherCol)
    {


        if (otherCol.gameObject.tag == "Player")
        {
            ///Stun the player
            otherCol.gameObject.GetComponent<Astronaut>().Stun();
        }
        if (otherCol.gameObject.tag == "Wedge")
        {
            var pmgr = FindObjectOfType<PlanetManager>();
            pmgr.PushWedge(otherCol.gameObject.transform.parent.eulerAngles.z);
            Destroy(this.gameObject);
        }
    }

}
