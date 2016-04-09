using UnityEngine;
using System.Collections;

public class EarthquakeTest : MonoBehaviour {

	public GameObject particle;
	private GameObject obj;

	public float Mod1, Mod2;

	float timeSinceStart = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
	{
		timeSinceStart += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if(obj!=null)
				Destroy(obj);
			obj = (GameObject)Instantiate(particle);
			timeSinceStart = 0f;
		}

		if( Input.GetKeyDown(KeyCode.C))
		{
			Destroy(obj);
			obj = null;
		}

		Debug.DrawLine(Vector3.zero, new Vector3(Mod1 * timeSinceStart + Mod2, 0f, 0f));
	}
}
