using UnityEngine;
using System.Collections;
using InputHandler;

public class MenuController : MonoBehaviour
{
	private Astronaut _astronaut;

	public GameObject ReadySprite;
	public MenuManager Menu;
	public int PlayerNumber;

	public void Update()
	{
		bool active = _astronaut.Height < 3.5;
		ReadySprite.SetActive(active);
		WorldManager.Instance.PlayersActive[PlayerNumber] = active;
	}

	// Use this for initialization
	public void Start()
	{
		InputManager.Instance.PushActiveContext("Gameplay", PlayerNumber);
		InputManager.Instance.AddCallback(PlayerNumber, HandlePlayerAxis);
		InputManager.Instance.AddCallback(PlayerNumber, HandlePlayerButtons);

		_astronaut = GetComponent<Astronaut>();
	}

	private void HandlePlayerAxis(MappedInput input)
	{
		if (this == null) return;
	}

	private void HandlePlayerButtons(MappedInput input)
	{
		if (this == null) return;

		if (input.Actions.Contains("Jump"))
		{
			_astronaut.Jump();
		}

		if (input.Actions.Contains("Start"))
		{
			Menu.StartGame();
		}
	}
}
