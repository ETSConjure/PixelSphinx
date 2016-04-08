using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour
{

    public int NbCartiers = 10;
    public float TailleCartiersEnDegres = 0;  //radian -> valeurs 0 a 360

    public List<Wedge> wedges = new List<Wedge>();
    

    // Use this for initialization
    void Start () {
        TailleCartiersEnDegres =  360.0f / NbCartiers;
          
        for(int i = 0; i < NbCartiers; i++)
        {
            float debutAngleTheta = i* TailleCartiersEnDegres;
            wedges.Add(new Wedge(){tMin = debutAngleTheta, tMax = debutAngleTheta + TailleCartiersEnDegres });
        }
    }
	
	// Update is called once per frame
	void Update () {
	

	}



    public float GetPlanetRadius()
    {
        return 5.0f;
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

    }

}
