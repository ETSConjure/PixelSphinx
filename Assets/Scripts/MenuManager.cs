using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public bool canPlay = false;
	public GameObject PressStart;

	void Awake()
	{
		WorldManager.Instance.PlayersActive = new bool[] { true, true, true, true } ;
	}

	// Use this for initialization
	void Start () {
		//Place all players
		MenuController[] players = FindObjectsOfType<MenuController>();
		Debug.Log(players.Length);
		foreach (MenuController player in players)
		{
			player.Menu = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		int numPlayers = 0;
		foreach (bool b in WorldManager.Instance.PlayersActive)
			numPlayers += b ? 1 : 0;
		
		canPlay = numPlayers > 1;
		PressStart.SetActive(canPlay);
	}

	public void StartGame()
	{
		if (canPlay)
			SceneManager.LoadScene("Main");
	}
}
