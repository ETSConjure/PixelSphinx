using System;
using UnityEngine;
using System.Collections;

public class Earthquake : MonoBehaviour {

	public float CriticalMin;
	public float CriticalMax;
	public float ExplosionTime;
	public GameObject ExplosionParticle;
	private const float WaveSpeed = 1f;
	private const float WaveOffset = 1.3f;

	private SpriteRenderer core;
	PlanetManager pmgr;

	bool isExploding;


	// Use this for initialization
    public void Start()
	{
		isExploding = false;
		pmgr = FindObjectOfType<PlanetManager>();
		core = this.GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    public void Update () {

		if(isExploding) return;

		float disbalance = pmgr.GetDisbalance();
		float val = Mathf.Clamp((disbalance-CriticalMin) / (CriticalMax-CriticalMin),0,1);
		
		float val2 = Mathf.Clamp((val - 0.6f) / 0.4f, 0, 1);
		pmgr.setColor(val2);

		core.color = new Color(1f, 1f - val, 1f - val);

		if (val2 >= CriticalMax + 0.05f)
		{
			EarthquakeBoom();
		}
	}

	private void EarthquakeBoom()
	{
		isExploding = true;
		StartCoroutine(Explode());
		Instantiate(ExplosionParticle);

        var audioBoom = gameObject.GetComponent<AudioSource>();
        audioBoom.bypassListenerEffects = true;
        AudioSource.PlayClipAtPoint(audioBoom.clip, transform.position, audioBoom.volume);


        var camera = GameObject.Find("Main Camera");
	    if (camera)
	    {
	        var shaker = camera.GetComponent<CameraShake>();
	        if (shaker) shaker.shakeTimeAmount = 2.0f;
	    }
	}

	IEnumerator Explode()
	{
		float realPosition;
		for (float i = 0; i < ExplosionTime; i += Time.deltaTime)
		{
			realPosition = WaveSpeed * i + WaveOffset;
			Debug.Log(realPosition);
			pmgr.EjectPlayers(realPosition);
			yield return null;
		}
	}
}
