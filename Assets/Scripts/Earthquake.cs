using UnityEngine;
using System.Collections;

public class Earthquake : MonoBehaviour {
    int gaugeLevel;
    const int gaugeMax=100;

	// Use this for initialization
    public void Start()
    {
        gaugeLevel = 0;
        InvokeRepeating("FillGauge", 1, 1F);
	
	}

    // Update is called once per frame
    public void Update () {
	
	}

    /// <summary>
    /// Actualiser l'affichage de la gauge
    /// </summary>
    public  void UpdateFixed()
    {
        



    }

    /// <summary>
    /// à être Appelé à chaque fois qu'on enfonce un plateau, le gage se remplis plus vite. (et par le temps)
    /// </summary>
    public void FillGauge()
    {
        if (gaugeLevel < gaugeMax)
        {
            gaugeLevel += 1;


            //anim state [0-90] normale, rotation

            //color hue de plus en plus vers le rouge

            //[90-100]
            //anim avec les ripples


        }
        else
        {
            gaugeLevel = 0;
            
        }
        print("gauge is at: " + gaugeLevel);
    }


    
}
