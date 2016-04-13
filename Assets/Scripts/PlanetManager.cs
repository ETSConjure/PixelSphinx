using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlanetManager : MonoBehaviour
{
	public float PlayerAngle;
	public float PlayerOffset;
	public bool InMenu;
	public GameObject YouWin;
    public  int _NbActivePlayersRemaining = 0;
    public int NbCartiers = 10;
    public float TailleCartiersEnDegres = 0;  //radian -> valeurs 0 a 360
    public float CartierResetRatioSpeedFactor = 0.23f;   //Entre 0.05 et 1 ou plus   on aime que ca restore lentement, randomnly
    public bool  CartierResetRatioSpeedRandomize = true;
    public bool  CartierResetOverTime = true;
    public float CartierMinRatio = 0.4f;
    public float CartierMaxRatio = 2.0f;
    public float CartierStepSize = 0.25f;
    public float CartierWaitBeforeRaise = 2f;
	public float balanceValue;
	private float disbalance = 0f;
    public GameObject WedgePrefab = null;
    public List<Wedge> wedges = new List<Wedge>();
	private int numPlayer;
	private bool gameEnded = false;

    // Use this for initialization
    public void Awake () {
		numPlayer = 0;
        TailleCartiersEnDegres =  360.0f / NbCartiers;
		balanceValue = (CartierMaxRatio + CartierMinRatio) / 2; 
          
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
            w.gameObject = (GameObject)obj;
            wedges.Add(w);  //pushes at end.
        }

        var worldMgr = WorldManager.Instance;



        if (!worldMgr.PlayersActive[0])
        {
            Destroy(GameObject.Find("Astronaut_0"));
        }
        if (!worldMgr.PlayersActive[1])
        {
            Destroy(GameObject.Find("Astronaut_1"));
        }
        if (!worldMgr.PlayersActive[2])
        {
            Destroy(GameObject.Find("Astronaut_2"));
        }
        if (!worldMgr.PlayersActive[3])
        {
            Destroy(GameObject.Find("Astronaut_3"));
        }
    }

    // Update is called once per frame
    public void Update () {
		
	}

	public void addPlayer()
	{
		numPlayer++;
	}

	public void setColor(float val)
	{
		foreach (Wedge w in wedges) {
			//w = new Color(1f, 1f - val, 1f - val);
			w.sprite.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, (1-val), (1-val));
		}

		//TODO make planet red
		//TODO screen shake
		//TODO controller shake?
	}

	public float GetDisbalance()
	{
		disbalance = 0;
		foreach (var w in wedges)
		{
			var temp = Math.Abs((w.offset - balanceValue) / (CartierMaxRatio - balanceValue));
			disbalance += temp;
		}
		disbalance /= NbCartiers;
		return disbalance;
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
            else if (w.offset > 1.0f && Time.time >= w.timeSinceLastPushedBack + CartierWaitBeforeRaise)
            {
                if (!CartierResetRatioSpeedRandomize)
                {
                    w.offset -= 0.005f*CartierResetRatioSpeedFactor;
                }
                else
                {
                    w.offset -= 0.005f*CartierResetRatioSpeedFactor * UnityEngine.Random.Range(-0.5f, 1.8f);
                }
            }
            else if ((w.offset < 1.0f)  && Time.time >= w.timeSincePushedToMinimum + CartierWaitBeforeRaise )
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
        VerifierPlayersActif();

        if (_NbActivePlayersRemaining <= 1)
        {
            //TODO  Call WorldManger.EndGame ou whatever
        }

    }

    private void VerifierPlayersActif()
    {
        
        int nbJoueursTrouves = 0;
        var players = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (var p in players)
        {
            if (p.GetComponent<Astronaut>().State < Astronaut.AstronautState.Ejecting)
            {
                nbJoueursTrouves++;
            }
        }
        
        _NbActivePlayersRemaining = nbJoueursTrouves;
    }

    public void PushWedge(float thetaPlayerX)
    {
        var index = GetWedgeIndex(thetaPlayerX);
        var w = wedges[index];


        var difference = CartierStepSize;

        var wOffsetBefore = w.offset;
        w.offset = w.offset - CartierStepSize;
        if (w.offset <= CartierMinRatio)
        {
            difference -= CartierMinRatio - w.offset; //enlever du push pour la plateforme qui va faire pousser dans le sens opposé
            w.timeSincePushedToMinimum = Time.time;
            w.offset = CartierMinRatio;
        }

        if (w.offset < wOffsetBefore)
        {
            var audio = w.gameObject.GetComponent<AudioSource>();
            audio.bypassListenerEffects = true;
            AudioSource.PlayClipAtPoint(audio.clip, transform.position, audio.volume);
        }


        w.sprite.transform.localScale = new Vector3(w.offset, w.offset, 1);

        //push back l'opposée
        var indexOppose = GetWedgeOpposé(index);
        var v = wedges[indexOppose];

       // if (Time.time >= v.timeSincePushedToMinimum + CartierWaitBeforeRaise)  // résultats étranges ;)
       // if (wOffsetBefore >= 0.9f)
       // {
            if (v.offset < CartierMaxRatio) v.timeSinceLastPushedBack = Time.time;

            v.offset = v.offset + difference;  //CartierStepSize; //diférentiel au lieu du step size
            if (v.offset >= CartierMaxRatio)
            {
                v.offset = CartierMaxRatio;
               

                //checker si on éjecte des players
                var players = FindObjectsOfType<Astronaut>();
                foreach (var p in players)
                {
                    if (v.tMax >= p.GetTheta() && p.GetTheta() >= v.tMin && p.IsGrounded())
                    {
                        p.Eject();
                    }
                }
            }
            v.sprite.transform.localScale = new Vector3(v.offset, v.offset, 1);
       // }
       
            // call fill gauge after every hit.
            //var earthQuakeGauge = FindObjectOfType<Earthquake>();
            //earthQuakeGauge.FillGauge();
    }

	public void EjectPlayers(float range)
	{
		if (InMenu) return;
		Astronaut[] players = FindObjectsOfType<Astronaut>();
		foreach (Astronaut p in players)
		{
			if (p.State < Astronaut.AstronautState.Ejecting && p.Height <= range)
				p.Eject();
		}
	}

    /// <summary>
    /// On a earthquake, everything expands by a step
    /// </summary>
    public void CallEarthQuake()
    {
        foreach (var w in wedges)
        {
            w.offset = w.offset + CartierStepSize;
            if (w.offset >= CartierMaxRatio)
            {
                w.offset = CartierMaxRatio;

                //checker si on éjecte des players
                var players = FindObjectsOfType<Astronaut>();
                foreach (var p in players)
                {
                    //si player sur la plateforme et grounded
                    if (w.tMax >= p.GetTheta() && p.GetTheta() >= w.tMin && p.IsGrounded())
                    {
						p.Eject();
                    }
                }
            }
        }
    }

	public void playerDeath(Astronaut aPlayer)
	{
		numPlayer--;
		//check if all players are dead
		if (numPlayer < 2)
		{
			if (!gameEnded)
			{
				StartCoroutine(EndGame());
				WorldManager.Instance.PlayersActive = new bool[] { true, true, true, true };
				YouWin.SetActive(true);
				gameEnded = true;
			}
			/*if (numPlayer < 1)
			{
				print("game is lost");
			}
			else
			{
				print("winner is you!");
			}*/
		}
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

	IEnumerator EndGame()
	{
		for (float i = 0; i < 5; i+= Time.deltaTime)
		{
			yield return null;
		}
		SceneManager.LoadScene("Selection");
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
        return wedges[GetWedgeIndex((360 + thetaPlayerX) % 360)];
    }

    /// <summary>
    /// Représente une plateforme qui bouge.
    /// </summary>
    public class Wedge
    {
        public float offset = 1.0f;  //valeurs entre minRatio et maxRatio; < 1 étant renfoncé, 1 position normale, et > 1 vers l'extérieur
        public float tMin = 0; //theta min et theta max : angle thetat de début et fin du cartier; 
        public float tMax = 0;
        public float timeSincePushedToMinimum = 0.0f;
        public float timeSinceLastPushedBack = 0.0f;
        public GameObject sprite;         //sprite et collider 2D
        public GameObject gameObject;    //wedge prefab avec collider
    }
}
