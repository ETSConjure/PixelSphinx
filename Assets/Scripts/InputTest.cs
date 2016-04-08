using UnityEngine;
using System.Collections;
using InputHandler;

public class InputTest : MonoBehaviour {

	private int PlayerNumber;

	// Use this for initialization
	void Start () {
		InputManager.Instance.PushActiveContext("Gameplay", PlayerNumber);
		InputManager.Instance.AddCallback(PlayerNumber, HandlePlayerAxis);
		InputManager.Instance.AddCallback(PlayerNumber, HandlePlayerButtons);
	}

	private void HandlePlayerAxis(MappedInput input)
	{
		if (this == null) return;

		// movement 

		float xValue = 0f;

		if (input.Ranges.ContainsKey("MoveLeft"))
		{
			xValue = -input.Ranges["MoveLeft"];
			Debug.Log("Moved left!");
		}
		else if (input.Ranges.ContainsKey("MoveRight"))
		{
			xValue = input.Ranges["MoveRight"];
			Debug.Log("Moved right!");
		}

		float zValue = 0f;

		if (input.Ranges.ContainsKey("MoveUp"))
		{
			zValue = input.Ranges["MoveUp"];
			Debug.Log("Moved up!");
		}
		else if (input.Ranges.ContainsKey("MoveDown"))
		{
			zValue = -input.Ranges["MoveDown"];
			Debug.Log("Moved down!");
		}
		
		if (input.Ranges.ContainsKey("Dash"))
		{
			//zValue = -input.Ranges["Dash"];
			Debug.Log("Dashed!");
		}
	}

	private void HandlePlayerButtons(MappedInput input)
	{
		if (this == null) return;

		if (input.Actions.Contains("Jump"))
		{
			Debug.Log("Jumped!");
		}
	}
}
