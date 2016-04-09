using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour
{
    public GameObject PrefabPs;

    private ParticleSystem ps;

	// Use this for initialization
	public void Start ()
	{
	    ps = PrefabPs.GetComponent<ParticleSystem>();
	}

    // Update is called once per frame
    public void FixedUpdate () {
	    if (ps && !ps.IsAlive())
	    {
            Destroy(this.gameObject);
	    }
	}
}
