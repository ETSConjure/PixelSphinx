using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour
{

    public int NbCartiers = 10;
    public float TailleCartiersEnDegres = 0;  //radian -> valeurs 0 a 360

    public GameObject WedgePrefab = null;

    public List<Wedge> wedges = new List<Wedge>();

   

    // Use this for initialization
    void Start () {
        TailleCartiersEnDegres =  360.0f / NbCartiers;
          
        for(int i = 0; i < NbCartiers; i++)
        {
            float debutAngleTheta = i* TailleCartiersEnDegres;
            var w = new Wedge() {tMin = debutAngleTheta, tMax = debutAngleTheta + TailleCartiersEnDegres};
            wedges.Add(w);  //pushes at end.

            //float angle = i * Mathf.PI * 2 / NbCartiers * 360;
            var wedgePos = GetPlanetCoordinatesFromPlayerXY(debutAngleTheta, 0);
            wedgePos.x -= 8/ Mathf.PI * Mathf.Cos(debutAngleTheta * Mathf.PI / 180);
            wedgePos.y -= 8/ Mathf.PI * Mathf.Sin(debutAngleTheta * Mathf.PI / 180);
            Instantiate(WedgePrefab, wedgePos, Quaternion.Euler(0, 0, debutAngleTheta));
         

        }
       
    }
	
	// Update is called once per frame
	void Update () {
	

	}

    void FixedUpdate()
    {
        //Ramener les plateforme vers leur position initiale 0;

        foreach (var w in wedges)
        {

        }

    }


    /// <summary>
    /// Radius sphere est scale/2
    /// </summary>
    /// <returns></returns>
    public float GetPlanetRadius()
    {
        return gameObject.transform.localScale.x / 2.0f;
    }


    public Vector3 GetPlanetCoordinatesFromPlayerXY(float playerLocalX, float playerLocalY)
    {
        var theta = playerLocalX;
        var x = GetPlanetRadius() * Math.Cos(theta * Math.PI / 180);
        var y = GetPlanetRadius() * Math.Sin(theta * Math.PI / 180) + playerLocalY;  

        return new Vector3((float)x, (float)y, 0);
    }


    /// <summary>
    /// retourn le no de plateforme
    /// </summary>
    /// <param name="thetaPlayerX"></param>
    public int GetWedgeIndex(float thetaPlayerX)
    {
        return  (int)Math.Floor(thetaPlayerX / TailleCartiersEnDegres);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="wedgeIndex"></param>
    /// <returns></returns>
    public int GetWedgeOpposé(int wedgeIndex)
    {
        //(i + 5) % 10 => [0,9]
        return (wedgeIndex + NbCartiers / 2) % (NbCartiers);
    }


    /// <summary>
    /// retourne l'objet interne
    /// </summary>
    /// <param name="thetaPlayerX"></param>
    /// <returns></returns>
    public Wedge GetWedgeFromTheta(float thetaPlayerX)
    {
        return wedges[GetWedgeIndex(thetaPlayerX)];
    }

    /// <summary>
    /// Représente une plateforme qui bouge.
    /// </summary>
    public class Wedge
    {
        public float yoffset = 0;  //valeurs entre -1 et 1; -1 étant renfoncé, 0 position normale, et 1 vers l'extérieur
        public float tMin = 0; //theta min et theta max : angle thetat de début et fin du cartier; 
        public float tMax = 0;

        public GameObject sprite;         //sprite et collider 2D

    }

}
