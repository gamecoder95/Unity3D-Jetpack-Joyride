using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {
	public float jetpackForce = 75.0f;
	private Rigidbody2D myRigidBody2D;

	// Use this for initialization
	void Start () {
		myRigidBody2D = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		bool jetpackActive = Input.GetButton ("Fire1");

		if (jetpackActive) 
		{
			myRigidBody2D.AddForce (new Vector2 (0, jetpackForce));
		}
			
	}
}
