using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
	public float jetpackForce = 75.0f;
	public float forwardMovementSpeed = 3.0f;
	private Rigidbody2D myRigidBody2D;
	public Transform groundCheckTransform;
	private bool grounded;
	public LayerMask groundCheckLayerMask;
	Animator animator;
	public ParticleSystem jetpack;

	// Use this for initialization
	void Start () {
		myRigidBody2D = this.GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AdjustJetpack (bool jetpackActive)
	{
		// Enable the jetpack emission
		ParticleSystem.EmissionModule em = jetpack.emission;
		em.enabled = true;

		// Set the emission rate
		ParticleSystem.MinMaxCurve rate = em.rate;
		rate.constantMin = jetpackActive ? 300.0f : 75.0f;
		rate.constantMax = jetpackActive ? 300.0f : 75.0f;
		em.rate = rate;


	}

	void UpdateGroundedStatus()
	{
		grounded = Physics2D.OverlapCircle (groundCheckTransform.position, 0.1f, groundCheckLayerMask);
		animator.SetBool ("grounded", grounded);
	}

	void FixedUpdate()
	{
		bool jetpackActive = Input.GetButton ("Fire1");

		if (jetpackActive) 
		{
			myRigidBody2D.AddForce (new Vector2 (0, jetpackForce));
		}

		Vector2 newVelocity = myRigidBody2D.velocity;
		newVelocity.x = forwardMovementSpeed;
		myRigidBody2D.velocity = newVelocity;
		UpdateGroundedStatus ();
		AdjustJetpack (jetpackActive);
	}
}
