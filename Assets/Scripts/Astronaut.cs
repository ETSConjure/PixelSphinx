using UnityEngine;
using System.Collections;

public class Astronaut : MonoBehaviour {

	private enum AstronautState
	{
		Idle, Walking, Jumping, Dashing, Ejecting, Dead
	}

	public GameObject Rotator;
	public GameObject SpriteWalk;
	public GameObject SpriteDash;

	public float StepTime;
	public float JumpSpeed;

	private AstronautState _state;
	private AstronautState State
	{
		get
		{
			return _state;
		}
		set
		{
			AstronautState oldState = _state;
			_state = value;

			if (oldState == _state) return;
			
			if (oldState == AstronautState.Dashing)
			{
				SpriteWalk.SetActive(false);
				SpriteDash.SetActive(true);
			}
			else
			{
				SpriteWalk.SetActive(true);
				SpriteDash.SetActive(false);
			}

			if (_state == AstronautState.Walking)
			{
				StartCoroutine(WalkingStance());
			}
		}
	}

	private float vSpeed = 0;
	private float height = 0;
	private float angle = 0;
	private float walkTime = 0;

	private int nextStep = 1;

	// Use this for initialization
	void Start () {
		State = AstronautState.Idle;
	}
	
	// Update is called once per frame
	void Update () {

		/*
		switch (State)
		{
			case AstronautState.Dashing:

				break;
			case AstronautState.Ejecting:

				break;
			case AstronautState.Idle:

				break;
			case AstronautState.Jumping:

				break;
			case AstronautState.Walking:

				break;
		}
		 */
	}

	public void Move(float x, float y)
	{
		if (State >= AstronautState.Ejecting )
			return;

		if (State < AstronautState.Jumping)
		{
			if (Mathf.Approximately(x, 0))
			{
				State = AstronautState.Idle;
			}
			else
			{
				Debug.Log("walking");
				State = AstronautState.Walking;
			}
		}
	}

	public void Jump()
	{
		if (_state >= AstronautState.Ejecting)
			return;
	}

	public void Dash()
	{
		if (_state >= AstronautState.Ejecting)
			return;
	}

	public void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 150, 50), State.ToString()))
			Debug.Log("Clicked the button with an image");
	}

	IEnumerator WalkingStance()
	{
		Debug.Log("walking stance");
		walkTime += Time.deltaTime / StepTime;
		while (State <= AstronautState.Walking && walkTime <= 1f)
		{
			Vector3 rotation = transform.rotation.eulerAngles;
			rotation.z = Mathf.Sin(walkTime*Mathf.PI);
			transform.rotation = Quaternion.Euler(rotation);
			yield return null;
		}

		/*walkTime = 0f;
		if(State == AstronautState.Walking)
		{
			StartCoroutine("WalkingStance");
		}*/
	}
}
