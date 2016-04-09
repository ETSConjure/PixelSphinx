using UnityEngine;
using System.Collections;
using InputHandler;

public class MenuController : MonoBehaviour
{
	private Astronaut _astronaut;

	public GameObject ReadySprite;
	public int PlayerNumber;

	public void Update()
	{
		ReadySprite.SetActive(_astronaut.Height < 3.5);
		//Debug.Log(_astronaut.Height);
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
	}
}
