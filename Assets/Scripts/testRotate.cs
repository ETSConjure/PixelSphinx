using System;
using UnityEngine;
using System.Collections;

public class testRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Juste pour tester le mouvement du player autour du cercle. 
    /// Le player se déplace de gauche a droite en x et la valeur de x représente l'angle theta
    ///           saute en y 
    /// </summary>
    void FixedUpdate()
    {
        var speed = 13.2;
        var theta = Time.realtimeSinceStartup * speed % 360.0; // Position X du player = angle theta
        var r = 5.0;  //sphereradius


        // XY coordinates 
        double x = r * Math.Cos(theta * Math.PI / 180);
        double y = r * Math.Sin(theta * Math.PI / 180);  // + y0 du player 

        var player = GameObject.Find("CubePlayer").gameObject;
         
        player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(  (float)x, (float)y, 0 ), Time.deltaTime);

    }
}
