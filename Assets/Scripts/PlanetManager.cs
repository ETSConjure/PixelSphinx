using UnityEngine;
using System.Collections;

public class PlanetManager : MonoBehaviour
{

    public int NbCartiers = 10;
    public float TailleCartiersEnDegres = 0;  //radian -> valeurs 0 a 360

    var wedges = new List<Wedge>();
    

    // Use this for initialization
    void Start () {
        TailleCartiersEnRadiants = 360.0 / NbCartiers;

        float debutAngleTheta = 0.0;

        for(int i = 0; i < NbCartiers; i++)
        {
            debutAngleTheta = i*TailleCartiersEnRadiants;
            wedges.Add(new wedges(){tMin = debutAngleTheta, tMax = debutAngleTheta + TailleCartiersEnRadiants});
        }
    }
	
	// Update is called once per frame
	void Update () {
	

	}



    public float GetPlanetRadius()
    {

    }


    public Vector3 GetPlanetCoordinatesFromPlayerXY(float playerLocalX, float playerLocalY)
    {
        var theta = playerLocalX;
        double x = r * Math.Cos(theta * Math.PI / 180);
        double y = r * Math.Sin(theta * Math.PI / 180) + playerLocalY;  


        return new Vector3(x,y,0);
    }


    /// <summary>
    /// retourn le no de plateforme
    /// </summary>
    /// <param name="thetaPlayerX"></param>
    public int GetWedgeIndex(float thetaPlayerX)
    {
        return  Math.Floor(thetaPlayerX / TailleCartiersEnRadiants);
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
        float yoffset = 0;  //valeurs entre -1 et 1; -1 étant renfoncé, 0 position normale, et 1 vers l'extérieur
        float tMin = 0; //theta min et theta max : angle thetat de début et fin du cartier; 
        float tMax = 0;

    }

}
