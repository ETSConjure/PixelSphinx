using UnityEngine;
using System.Collections;

public class Earthquake : MonoBehaviour {
    int gaugeLevel;
    const int gaugeMax=4;

	// Use this for initialization
    void Start()
    {
        gaugeLevel = 0;
        InvokeRepeating("fillGauge", 1, 1F);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void fillGauge()
    {
        if (gaugeLevel < gaugeMax)
        {
            gaugeLevel += 1;
        }
        else
        {
            gaugeLevel = 0;
        }
       print("gauge is at: " + gaugeLevel);
    }
}
