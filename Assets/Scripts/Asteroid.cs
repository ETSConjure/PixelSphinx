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
   // private AudioSource audio;

    public GameObject CrashFlamesEmitter;  //Emitter on impact

    public GameObject TrailFlamesEmitter;  // trailing smoke

    // Use this for initialization
    public void Start()
    {
        //audio = gameObject.GetComponent<AudioSource>();
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

            if (CrashFlamesEmitter)
            {
                var crashPosition = this.transform.position;
                //crashPosition.z = 1.15f;

                var asteroidTheta = Mathf.Atan2(this.transform.position.y, this.transform.position.x);
                var angleImpact = (360.0f + (((asteroidTheta * 180)) / Mathf.PI)) % 360;

                var emitter = (GameObject)Instantiate(CrashFlamesEmitter, crashPosition, Quaternion.identity);

                //fix du prefab, il point à l'inverse de la caméra, ramenner avec rotation 90 deg en y 
                //et donner l'angle d'impact inverse en z (vers l'extérieur de la planete)
                //emitter.transform.Rotate(0,90.0f,angleImpact);
                emitter.transform.localRotation = Quaternion.Euler(0, 180.0f, angleImpact);


                var audio = GetComponent<AudioSource>();
                audio.bypassListenerEffects = true;
                AudioSource.PlayClipAtPoint(audio.clip, transform.position, audio.volume);

                var wait = new WaitForSeconds(emitter.GetComponent<ParticleSystem>().duration);
                Destroy(emitter);

            }

            Destroy(this.gameObject);
           
        }
    }


}
