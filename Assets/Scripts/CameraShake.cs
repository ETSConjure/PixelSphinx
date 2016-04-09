using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    
    public float shakeTimeAmount = 0.0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Camera camera;

	// Use this for initialization
	public void Start ()
	{
        camera = gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
	    if (shakeTimeAmount > 0)
	    {
            var tempShakePosition = camera.transform.localPosition = Random.insideUnitSphere * shakeAmount;
	        tempShakePosition.z = -10f; // lock z from shaking...
            camera.transform.localPosition = tempShakePosition; //
            shakeTimeAmount -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeTimeAmount = 0.0f;
        }
    }
}


 
