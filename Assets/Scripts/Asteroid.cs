using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    Vector3 center;
    public float speed;
    public float step;
    public float rotationSpeed = 1.0f;
    public float rotationDirection = 1.0f;
    public bool  RandomRotationSpeed = true;

    // Use this for initialization
    public void Start()
    {
        speed = Random.Range(1.8F, 3F);
        center = new Vector3(0, 0);
		
        if (RandomRotationSpeed)
            rotationSpeed =  10 * UnityEngine.Random.Range(0.25f, 5f);
		
		rotationDirection = (Mathf.Floor(UnityEngine.Random.Range(0.0f, 1.99f)) * 2 - 1); 
		
    }

    // Update is called once per frame
    public void Update () {
            MoveObject(center);

	}

    public void MoveObject(Vector3 center)
    {
        step = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(transform.position, center, step);



        this.transform.Rotate(new Vector3(0, 0, 1.0f), rotationDirection * rotationSpeed *  Time.deltaTime);
         
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
