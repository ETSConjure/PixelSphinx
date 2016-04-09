using UnityEngine;
using System.Collections;

public class AstronautAnimator : MonoBehaviour {

    //init
    public Astronaut aspi;
    public float WalkAnimSpeed;
    public float WalkAnimAngle;
    public float EjectSpinSpeed;

    public GameObject DustParticlesEmitter;
    // Use this for initialization
    protected void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
	    
	}

    public void Jump()
    {
        aspi.SpriteWalk.gameObject.SetActive(true);
        aspi.SpriteDash.gameObject.SetActive(false);
    }

    public void Dash()
    {
        aspi.SpriteWalk.gameObject.SetActive(false);
        aspi.SpriteDash.gameObject.SetActive(true);
    }

    public void Land()
    {
        
        aspi.SpriteWalk.gameObject.SetActive(true);
        aspi.SpriteDash.gameObject.SetActive(false);
    }

    public void Walk(bool right)
    {
        Debug.Log("Walking!");
        StartCoroutine(Rotate(right? -1 : 1));
    }

    public void Eject()
    {
        StartCoroutine(Spin());
    }

    IEnumerator Spin()
    {
        for (float i = 0f; i < 3000f; i += Time.deltaTime * EjectSpinSpeed)
        {
            transform.rotation = Quaternion.Euler(0, 0, i);
            yield return null;
        }
    }

    IEnumerator Rotate(float side)
    {
        for (float i = 0.5f; i < 1.5f; i+= Time.deltaTime*WalkAnimSpeed)
        {
            /*int roundDown = 10;
            //0.5, 1.5 et 2.5
            if (Mathf.Floor(i * roundDown) == roundDown || Mathf.Floor(i * roundDown) == 2 * roundDown)
            {
                print(i * roundDown + " " + Mathf.Floor(i * roundDown));
                aspi.SpriteWalk.flipX = !aspi.SpriteWalk.flipX;
            }*/
            float position = Mathf.PingPong(i, 1f);
            transform.localRotation = Quaternion.Euler(0, 0, side * (position - 0.5f) * WalkAnimAngle * 2);
            yield return null;
        }

        if (aspi.State == Astronaut.AstronautState.Walking)
        {
            Debug.Log("Walking again");
            StartCoroutine(Rotate(-side));
        }
        else
            Debug.Log("Walking stop");
        //yield return null;
    }

    public void EmitDustParticules()
    {
        if (DustParticlesEmitter)
        {
            var emitter = (GameObject)Instantiate(DustParticlesEmitter, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
