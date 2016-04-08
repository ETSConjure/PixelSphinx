using System;
using UnityEngine;
using System.Collections;

public class testRotate : MonoBehaviour {


  
    public  float fireRate = 1.0f;
    private float lastShot = 0.0f;


    void Update()
    {
        if(Input.GetKeyDown("space") || Input.GetKey("s"))
        {

            Fire();
        }
    }

    private void Fire()
    {
        if (Time.time > fireRate + lastShot)
        {
            lastShot = Time.time;

            var speed = 13.2f;
            var theta = Time.realtimeSinceStartup * speed % 360.0f;



            var pmgr = FindObjectOfType<PlanetManager>();
            pmgr.PushWedge(theta);


          
            var index = pmgr.GetWedgeIndex(theta);
            

        }
    }

 
	// Use this for initialization
	void Start () {
	
	}
	
	  
 


    /// <summary>
    /// Juste pour tester le mouvement du player autour du cercle. 
    /// Le player se déplace de gauche a droite en x et la valeur de x représente l'angle theta
    ///           saute en y 
    /// </summary>
    void FixedUpdate()
    {
        var speed = 13.2f;
        var theta = Time.realtimeSinceStartup * speed % 360.0f; // Position X du player = angle theta
        var r = 4.5f;  //sphereradius


        // XY coordinates 
        var x = r * Mathf.Cos(theta * Mathf.PI / 180);
        var y = r * Mathf.Sin(theta * Mathf.PI / 180);  // + y0 du player 

        var player = GameObject.Find("CubePlayer").gameObject;
         
        player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(x, y, 0 ), Time.deltaTime);

    }
}
