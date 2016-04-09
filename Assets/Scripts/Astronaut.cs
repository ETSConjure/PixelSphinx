using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AstronautAnimator))]
public class Astronaut : MonoBehaviour {

    private AstronautAnimator _astronautAnimator;
	public enum AstronautState
	{
		Idle, Walking, Jumping, Dashing, Ejecting, Dead
	}

	public GameObject Rotator;
	public SpriteRenderer SpriteWalk;
	public GameObject SpriteDash;

	public float StepTime;
	public float JumpSpeed;

	private AstronautState _state;
	public AstronautState State
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
                SpriteWalk.gameObject.SetActive(true);
                SpriteDash.gameObject.SetActive(false);
			}
			else
            {
                SpriteWalk.gameObject.SetActive(true);
                SpriteDash.gameObject.SetActive(false);
			}
            
			if (_state == AstronautState.Walking)
			{
				//StartCoroutine(WalkingStance());
                _astronautAnimator.Walk();
			}
		}
	}

	private float vSpeed = 0;
	private float height = 0;
	private float angle = 0;
	private float walkTime = 0;
	private int nextStep = 1;

	// Use this for initialization
    void Start()
    {
        _astronautAnimator = GetComponent<AstronautAnimator>();
        _astronautAnimator.aspi = this;
		State = AstronautState.Idle;
	}
	
	// Update is called once per frame
	void Update () {
        /*
		if (State == AstronautState.Walking)
		{
			walkTime += Time.deltaTime / StepTime;
			Vector3 rotation = transform.rotation.eulerAngles;
			rotation.z = Mathf.Sin(walkTime * Mathf.PI)*50;
			transform.rotation = Quaternion.Euler(rotation);
			Debug.Log(rotation.z);
		}*/

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
				State = AstronautState.Walking;
				walkTime = 0f;
			}
		}
	}

	public void Jump()
	{
		if (_state >= AstronautState.Ejecting)
			return;
        _astronautAnimator.Jump();
	}

	public void Dash()
	{
		if (_state >= AstronautState.Ejecting)
			return;
	}

	public void OnGUI()
	{
        if (GUI.Button(new Rect(10, 10, 150, 50), "Jump"))
        {
            Debug.Log("Clicked the button with an image");
             _astronautAnimator.Jump();
        }

        if (GUI.Button(new Rect(10, 70, 150, 50), "Land"))
        {
            Debug.Log("Clicked the 2nd button");
            _astronautAnimator.Land();
        }

        if (GUI.Button(new Rect(10, 130, 150, 50), "Walk"))
        {
            Debug.Log("Clicked the 3rd button");
            State = AstronautState.Walking;
            _astronautAnimator.Walk();
        }

        if (GUI.Button(new Rect(10, 190, 150, 50), "Eject"))
        {
            Debug.Log("Clicked the 4th button");
            _astronautAnimator.Eject();
        }
	}
    /*
	IEnumerator WalkingStance()
	{
		Debug.Log("walking stance");
		walkTime += Time.deltaTime / StepTime;
		while (State <= AstronautState.Walking || walkTime <= 1f)
		{
			Vector3 rotation = transform.rotation.eulerAngles;
			rotation.z = Mathf.Sin(walkTime*Mathf.PI)*50;
            //print("rotation " + rotation);
			transform.rotation = Quaternion.Euler(rotation);
			yield return null;
		}

		walkTime = 0f;
		if(State == AstronautState.Walking)
		{
			StartCoroutine("WalkingStance");
		}
	}*/
}
