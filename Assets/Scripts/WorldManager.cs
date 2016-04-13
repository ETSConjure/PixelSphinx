using UnityEngine;
using System.Collections;

public class WorldManager
{
	private static WorldManager instance = new WorldManager();
	public static WorldManager Instance
	{
		get { return instance; }
	}
	private WorldManager() { }

	public bool[] PlayersActive = { true, true, true, true };
}
