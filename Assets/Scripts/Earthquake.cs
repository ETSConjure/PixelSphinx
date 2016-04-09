using System;
using UnityEngine;
using System.Collections;

public class Earthquake : MonoBehaviour {

	public float CriticalMin;
	public float CriticalMax;
	//public float testValue;

    //public float gaugeLevel;
    //public int gaugeMax=100;
	private SpriteRenderer core;
	PlanetManager pmgr;


	// Use this for initialization
    public void Start()
	{
		pmgr = FindObjectOfType<PlanetManager>();
        //gaugeLevel = 0;
		core = this.GetComponent<SpriteRenderer>();
        //InvokeRepeating("FillGauge", 1, 1F);
	
	}

    // Update is called once per frame
    public void Update () {
		float disbalance = pmgr.GetDisbalance();
		float val = Mathf.Clamp((disbalance-CriticalMin) / (CriticalMax-CriticalMin),0,1);
		
		float val2 = Mathf.Clamp((val - 0.6f) / 0.4f, 0, 1);
		pmgr.setColor(val2);

		core.color = new Color(1f, 1f - val, 1f - val);

	}

    /// <summary>
    /// Actualiser l'affichage de la gauge
    /// </summary>
    public void UpdateFixed()
    {   
    }

    /// <summary>
    /// à être Appelé à chaque fois qu'on enfonce un plateau, le gage se remplis plus vite. (et par le temps)
    /// </summary>
    public void Fill4Gauge()
    {

        /*if (gaugeLevel < gaugeMax)
        {
            gaugeLevel += 1;

            //anim state [0-90] normale, rotation

            //color hue de plus en plus vers le rouge

            //[90-100]
            //anim avec les ripples


        }
        else
        {

            var planet = FindObjectOfType<PlanetManager>();

            planet.CallEarthQuake();

            gaugeLevel = 0;
            
        }
        print("gauge is at: " + gaugeLevel);*/
    }


    
}
