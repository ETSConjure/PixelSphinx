﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AstronautAnimator))]
public class Astronaut : MonoBehaviour {

    private AstronautAnimator _astronautAnimator;
	public enum AstronautState
	{
		Idle, Walking, Jumping, Dashing, Stun, Ejecting, Dead
	}

	public GameObject Rotator;
	public SpriteRenderer SpriteWalk;
    public GameObject SpriteDash;
    public GameObject SpriteStun;

	public int PlayerNum;
	public float SpriteWidth;
	public float SpriteHeight;
    public float DashTime = 0.4f; //Temps de l'animation et rate limiting
    private float lastDashTime = 0f;
	public float StepTime;
	public float JumpSpeed;
	public float Gravity;
	public float Speed;
	public float EjectSpeed;

	//public float DashSpeed;

	public PlanetManager planet;

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
                _astronautAnimator.Land();
                //_astronautAnimator.Idle();
			}
            else if (State == AstronautState.Walking)
			{
                _astronautAnimator.Walk(walkRight);
			}
		}
	}

	private float theta = 0;
	private float height = 0;
	public float Height
	{
		get { return height; }
	}
    private float vSpeed = 0;
	private bool grounded = false;
    private bool walkRight = false;

	private float walkTime = 0;
	private int nextStep = 1;

    public float GetTheta()
    {
        return theta;
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    // Use this for initialization
    protected void Start()
    {
        _astronautAnimator = GetComponent<AstronautAnimator>();
        _astronautAnimator.aspi = this;
		State = AstronautState.Idle;

	    if (!planet)
	    {
	        planet = FindObjectOfType<PlanetManager>();
	    }
		planet.addPlayer();

	    State = AstronautState.Idle;
		theta = PlayerNum * planet.PlayerAngle + planet.PlayerOffset;
		//TODO Check world manager
		height = planet.GetPlanetRadius(theta);
		UpdatePosition();
	}

	private void UpdatePosition()
	{
		//float heightAtPos = planet.GetPlanetRadius(theta);
		transform.localPosition = new Vector3(0, height + SpriteHeight / 2, 0);
		Rotator.transform.localRotation = Quaternion.Euler(0, 0, theta - 108);
	}

	private float GetGroundRadius()
	{
		return GetGroundRadius(theta);
	}

	private float GetGroundRadius(float theta)
	{
		float displacement = PlanetUtilities.GetDisplacementAngle(SpriteWidth / 2, height);
		float radius1 = planet.GetPlanetRadius(Repeat(theta + displacement, 360));
		float radius2 = planet.GetPlanetRadius(Repeat(theta - displacement, 360));
		//float x1, y1, x2, y2;
		//PlanetUtilities.Spheric2Cartesian(theta + displacement, height, out x1, out y1);
		//PlanetUtilities.Spheric2Cartesian(theta - displacement, height, out x2, out y2);
		//Debug.DrawLine(new Vector3(x1, y1, 0), new Vector3(x2, y2, 0));
		return Mathf.Max(radius1, radius2);
	}

	private float Repeat(float num, float limit)
	{
        //This is a modulus
		return Mathf.Repeat(num + limit, limit);
	}

    // Update is called once per frame
    public void Update () {
		float delta = Time.deltaTime;

		if (!grounded)
		{
			height += vSpeed * delta;
			if (State != AstronautState.Ejecting)
				vSpeed -= Gravity * delta;
			else
				vSpeed *= 0.99f;
		}

		float radius = GetGroundRadius();
		if (grounded = (height <= radius))
		{
            //Pousser la plateforme avec le dash une fois qu'on touche au sol.
			if (State == AstronautState.Dashing)
			{
                planet.PushWedge(this.theta);
                
                State = AstronautState.Idle;
            }

			height = radius;
			if (State == AstronautState.Jumping)
				State = AstronautState.Idle;

			if (State < AstronautState.Ejecting) vSpeed = 0f;
		}

		UpdatePosition();		
	}

	public void Move(float x, float y)
	{
		if (State >= AstronautState.Dashing )
			return;

        float playerX, playerY;
        PlanetUtilities.Spheric2Cartesian(theta - 108, height, out playerX, out playerY);

        Vector3 pos = new Vector3(playerX, playerY);

        Vector3 dirV = Vector3.Cross(pos, Vector3.forward).normalized;
        float proj = Vector3.Dot(new Vector3(x, y, 0), dirV);

        float move = proj;

		if (State < AstronautState.Jumping)
		{
			if (Mathf.Approximately(move, 0))
			{
				State = AstronautState.Idle;
			}
			else
			{
                walkRight = move > 0;
				State = AstronautState.Walking;
				walkTime = 0f;
			}
		}

		if (-0.2 < move && move < 0.2) return;

		float movement = PlanetUtilities.GetDisplacementAngle(Speed * -move, height) * Time.deltaTime;

		float newTheta = Repeat(theta + movement, 360);

		float newHeight = GetGroundRadius(newTheta);
		if (newHeight > height)
		{
			//Debug.Log("Blocked by wall");
			return; // Blocked by wall
		}

		theta = newTheta;
	}

	public void Jump()
	{
        if (State >= AstronautState.Dashing)
            return;

	    if (State == AstronautState.Jumping)
	    {
	        Dash();
            //State=AstronautState.Dashing;  //TODO relacher l'état Dashing
	        return;
	    }

        if (!grounded) return;

        _astronautAnimator.Jump();  // deja dans le property get/set

		vSpeed = JumpSpeed;
		grounded = false;
		State = AstronautState.Jumping;
	}

	public void Dash()
	{
	    if (Time.time < DashTime + lastDashTime)
            return;
        
        if (State >= AstronautState.Ejecting)
		    return;

        
	    lastDashTime = Time.time;
        //planet.PushWedge(this.theta);  //TODO devrait se faire juste avant d'être groundé
        State = AstronautState.Dashing;
        _astronautAnimator.Dash();
		//vSpeed = -DashSpeed;
	}

	public void Eject()
	{
		State = AstronautState.Ejecting;
		vSpeed = EjectSpeed;
	    _astronautAnimator.Eject();
		grounded = false;

		planet.playerDeath(this);
	}

    /// <summary>
    /// A character is stunned when hit by asteroid.
    /// </summary>
    public void Stun()
    {
        if (State < AstronautState.Ejecting)
        {
            State = AstronautState.Stun;
            StartCoroutine(StunTimeout());
            _astronautAnimator.Stun();
        }
    }

    IEnumerator StunTimeout()
    {
        for (float i = 0f; i <0.6f; i += Time.deltaTime)
        {
            yield return null;
        }
        if (State < AstronautState.Ejecting)
        {
            State = AstronautState.Idle;
            _astronautAnimator.Idle();
        }
    }

}
