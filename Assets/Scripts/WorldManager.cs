using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

    private static WorldManager instance = null; 
    private WorldManager(){}             

	// Use this for initialization
	public void Awake () {
        if (!instance)
        {
            instance = new WorldManager();
        }
	}

    public WorldManager getInstance() {
        return instance;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
