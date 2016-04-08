using UnityEngine;
using System.Collections;
using InputHandler;

[RequireComponent(typeof(Astronaut))]
public class AstronautController : MonoBehaviour {

	private Astronaut _astronaut;

	public int PlayerNumber;

	// Use this for initialization
	void Start()
	{
		InputManager.Instance.PushActiveContext("Gameplay", PlayerNumber);
		InputManager.Instance.AddCallback(PlayerNumber, HandlePlayerAxis);
		InputManager.Instance.AddCallback(PlayerNumber, HandlePlayerButtons);

		_astronaut = GetComponent<Astronaut>();
	}

	private void HandlePlayerAxis(MappedInput input)
	{
		if (this == null) return;

		// movement 

		float xValue = 0f;

		if (input.Ranges.ContainsKey("MoveLeft"))
		{
			xValue = -input.Ranges["MoveLeft"];
		}
		else if (input.Ranges.ContainsKey("MoveRight"))
		{
			xValue = input.Ranges["MoveRight"];
		}

		float yValue = 0f;

		if (input.Ranges.ContainsKey("MoveUp"))
		{
			yValue = input.Ranges["MoveUp"];
		}
		else if (input.Ranges.ContainsKey("MoveDown"))
		{
			yValue = -input.Ranges["MoveDown"];
		}

		_astronaut.Move(xValue, yValue);

		if (input.Ranges.ContainsKey("Dash"))
		{
			if(input.Ranges["Dash"] > 0.8f)
				_astronaut.Dash();
		}
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

