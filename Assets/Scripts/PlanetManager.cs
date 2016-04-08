using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour
{

    public int NbCartiers = 10;
    public float TailleCartiersEnDegres = 0;  //radian -> valeurs 0 a 360
    public float CartierResetRatioSpeedFactor = 0.23f;   //Entre 0.05 et 1 ou plus   on aime que ca restore lentement, randomnly
    public bool  CartierResetRatioSpeedRandomize = true;
    public bool  CartierResetOverTime = true;
    public float CartierMinRatio = 0.4f;
    public float CartierMaxRatio = 2.0f;
    public float CartierStepSize = 0.25f;
    public GameObject WedgePrefab = null;
    public List<Wedge> wedges = new List<Wedge>();

    // Use this for initialization
    public void Awake () {
        TailleCartiersEnDegres =  360.0f / NbCartiers;
          
        for(int i = 0; i < NbCartiers; i++)
        {
            float debutAngleTheta = i* TailleCartiersEnDegres;
            var w = new Wedge() {tMin = debutAngleTheta, tMax = debutAngleTheta + TailleCartiersEnDegres};
           
            //float angle = i * Mathf.PI * 2 / NbCartiers * 360;
            //var wedgePos = GetPlanetCoordinatesFromPlayerXY(debutAngleTheta, 0);
            // wedgePos.x -= Mathf.Cos(debutAngleTheta * Mathf.PI / 180);
            //wedgePos.y -= Mathf.Sin(debutAngleTheta * Mathf.PI / 180);
            var obj = Instantiate(WedgePrefab, new Vector3(0.0f,0.0f, 0.0f), Quaternion.Euler(0, 0, debutAngleTheta));
            obj.name = "wedge_" + i;
            w.sprite = GameObject.Find(obj.name);
            wedges.Add(w);  //pushes at end.
        }
    }

    // Update is called once per frame
    public void Update () {
	

	}

    public void FixedUpdate()
    {
		if (!this.CartierResetOverTime) return;
        //Ramener les plateforme vers leur position initiale 0;

        foreach (var w in wedges)
        {
            if (w.offset <= 1.05f && w.offset >= 0.95f)
            {
                w.offset = 1.0f;
            }
            else if (w.offset > 1.0f)
            {
                if (!CartierResetRatioSpeedRandomize)
                {
                    w.offset -= 0.005f*CartierResetRatioSpeedFactor;
                }
                else
                {
                    w.offset -= 0.005f*CartierResetRatioSpeedFactor * UnityEngine.Random.Range(-0.5f, 2f);
                }
            }
            else if (w.offset < 1.0f)
            {
                if (!CartierResetRatioSpeedRandomize)
                {
                    w.offset += 0.005f*CartierResetRatioSpeedFactor;
                }
                else
                {
                    w.offset += 0.005f*CartierResetRatioSpeedFactor*UnityEngine.Random.Range(0f, 3f);
                }
            }

            w.sprite.transform.localScale = new Vector3(w.offset, w.offset,1.0f);
        }
		//TODO_SR For each player
    }

    public void PushWedge(float thetaPlayerX)
    {
            var index = GetWedgeIndex(thetaPlayerX);
            var w = wedges[index];

            w.offset = w.offset - CartierStepSize;
            if (w.offset < CartierMinRatio)
                w.offset = CartierMinRatio;


            w.sprite.transform.localScale = new Vector3(w.offset, w.offset, 1);

            //push back l'opposée
            var indexOppose = GetWedgeOpposé(index);
            var v = wedges[indexOppose];

            v.offset = v.offset + CartierStepSize;
            if (v.offset > CartierMaxRatio)
                v.offset = CartierMaxRatio;

            v.sprite.transform.localScale = new Vector3(v.offset, v.offset, 1);
    }


    //public void PushWedge(float thetaPlayerX)
    //{
    //    var index = GetWedgeIndex(thetaPlayerX);
    //    var w = wedges[index];



    //    w.offset = w.offset - 0.5f;
    //    if (w.offset < -1.0f)
    //        w.offset = -1.0f;

    //    var angle = w.tMin; //w.tMax - TailleCartiersEnDegres/2;

    //    var normalPos = GetPlanetCoordinatesFromPlayerXY(angle, 0);
    //    normalPos.x -=  Mathf.Cos(angle * Mathf.PI / 180);
    //    normalPos.y -=  Mathf.Sin(angle * Mathf.PI / 180);

    //    var wedgePos = GetPlanetCoordinatesFromPlayerXY(angle, 0);
    //    wedgePos.x -=  Mathf.Cos(angle * Mathf.PI / 180) - 50 * w.offset * Mathf.Cos(angle * Mathf.PI / 180);     
    //    wedgePos.y -=  Mathf.Sin(angle * Mathf.PI / 180) - 50 * w.offset * Mathf.Sin(angle * Mathf.PI / 180);


    //    w.sprite.transform.position = Vector3.Lerp(normalPos, wedgePos, Time.deltaTime);

    //    ///push back l'opposée
    //    var indexOppose = GetWedgeOpposé(index);
    //    var v = wedges[indexOppose];

    //    v.offset = v.offset + 0.5f;
    //    if (v.offset > 1.0f)
    //        v.offset = 1.0f;

    //     angle = v.tMin; //w.tMax - TailleCartiersEnDegres/2;

    //     normalPos = GetPlanetCoordinatesFromPlayerXY(angle, 0);
    //    normalPos.x -= Mathf.Cos(angle * Mathf.PI / 180);
    //    normalPos.y -= Mathf.Sin(angle * Mathf.PI / 180);

    //     wedgePos = GetPlanetCoordinatesFromPlayerXY(angle, 0);
    //    wedgePos.x -= Mathf.Cos(angle * Mathf.PI / 180) - 50 * v.offset * Mathf.Cos(angle * Mathf.PI / 180);
    //    wedgePos.y -= Mathf.Sin(angle * Mathf.PI / 180) - 50 * v.offset * Mathf.Sin(angle * Mathf.PI / 180);


    //    v.sprite.transform.position = Vector3.Lerp(normalPos, wedgePos, Time.deltaTime);


    //}


    /// <summary>
    /// Radius sphere est scale/2
    /// </summary>
    /// <returns></returns>
    public float GetPlanetRadius()
    {
        return gameObject.transform.localScale.x / 2.0f;
    }

    /// <summary>
    /// Radius sphere est scale/2
    /// </summary>
    /// <returns></returns>
	public float GetPlanetRadius(float thetaPlayerX)
	{
		var wedge = GetWedgeFromTheta(thetaPlayerX);
		return GetPlanetRadius() * wedge.offset;
	}


    public Vector3 GetPlanetCoordinatesFromPlayerXY(float playerLocalX, float playerLocalY)
    {
        var theta = playerLocalX;
        var wedgeRadius = GetPlanetRadius(playerLocalX) + playerLocalY;
        var x = wedgeRadius * Mathf.Cos(theta * Mathf.PI / 180);
        var y = wedgeRadius * Mathf.Sin(theta * Mathf.PI / 180) ;  

        return new Vector3(x, y, 0);
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
        return wedges[GetWedgeIndex(thetaPlayerX % 360)];
    }

    /// <summary>
    /// Représente une plateforme qui bouge.
    /// </summary>
    public class Wedge
    {
        public float offset = 1.0f;  //valeurs entre minRatio et maxRatio; < 1 étant renfoncé, 1 position normale, et > 1 vers l'extérieur
        public float tMin = 0; //theta min et theta max : angle thetat de début et fin du cartier; 
        public float tMax = 0;

        public GameObject sprite;         //sprite et collider 2D

    }

}
