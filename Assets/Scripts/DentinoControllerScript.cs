using UnityEngine;
using System.Collections;

public class DentinoControllerScript : MonoBehaviour {

	// Variables
	// Physics
	public float runSpeed = 500f;		// Character's run speed
	private bool facingRight = true;	// Is facing right

	private bool grounded = false;		// On the ground
	public Transform groundCheck;		// Transform for the ground
	private float groundRadius = 0.2f;	// Radius for checking the ground
	public LayerMask whatIsGround;		// Defines what is ground
	public LayerMask whatIsHookable;	// Defines what is hookable
	public float jumpForce = 800f;		// Jump force
	private bool canJump = true;

	// Current velocity
	private Vector2 velocity, accel;
	private Vector2 joystickPos, oldJoystickPos;

	// Character Characteristics
	public float life = 100;
	public float maxLife = 100f;
	private int leftTouchId = -1;
	public bool attacking = false;
	public bool jumpForever = false;

	// Joystick
	public CNJoystick joystick;

	// Escova
	public bool hasEscova;

	// Dental floss
	public LineRenderer dentalFlossLineRenderer;
	private DistanceJoint2D distanceJoint2D;
	public bool isFlossHanging = false;
	public bool hasFloss;

	// Lock position
	public bool lockPosition = false;

	// Animator
	Animator anim_main, anim_eyes, anim_mouth, anim_brows, anim_escova, anim_psbubbles;

	// First attack
	public bool attackHandled = true;

	// Initialization
	void Start () {
		// Get animations
		anim_main = GetComponent<Animator>();
		anim_eyes = transform.GetChild(1).GetComponent<Animator>();
		anim_mouth = transform.GetChild(2).GetComponent<Animator>();
		anim_brows = transform.GetChild(3).GetComponent<Animator>();
		anim_escova = transform.GetChild(4).GetComponent<Animator>();
		anim_psbubbles = transform.GetChild(5).GetComponent<Animator>();
		//dentalFlossLineRenderer = transform.GetChild(6).GetComponent<LineRenderer>();
		distanceJoint2D = GetComponent<DistanceJoint2D>();

		leftTouchId = -1;

		// Reset velocity and joystick tracker
		velocity = Vector2.zero;

		joystickPos = Vector2.zero;
		oldJoystickPos = Vector2.zero;

		// Setup joystick events
		joystick.JoystickMovedEvent += HandleJoystickMovedEvent;
		joystick.FingerLiftedEvent += HandleFingerLiftedEvent;

		// Setup Dynamic Element Controller event
		GetComponent<DynamicElementController>().OnBack += HandleOnBack;// += HandleOnBacked;
	}

	// When player falls and gets taken back to original position
	bool HandleOnBack ()
	{
		return true;
	}

	// Joystick moved
	void HandleJoystickMovedEvent (Vector3 relativeVector)
	{
		oldJoystickPos = joystickPos;
		joystickPos = new Vector2 (relativeVector.x, relativeVector.y);
	}
	// Joystick finger left the screen
	void HandleFingerLiftedEvent ()
	{
		joystickPos = Vector2.zero;
	}
	
	// Fixed Update - constant step (no need for delta)
	void FixedUpdate () {

	}

	// Update - called every frame
	void Update() {
		velocity = Vector2.zero;
		if (!lockPosition)
			velocity += accel;
		accel = Vector2.zero;

		// jump forever
		if (jumpForever)
			jump (true);

		// Check ground
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim_main.SetBool ("Grounded", grounded);
		
		// Update the Vertical speed on the animator
		anim_main.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		
		// Get the input for horizontal movement
		if (!lockPosition) {
			velocity.x += Input.GetAxis ("Horizontal") * runSpeed;
			velocity.x += joystickPos.x * runSpeed;
		}
		
		// Update the Speed parameter for the Animation
		anim_main.SetFloat ("Speed", Mathf.Abs (velocity.x + rigidbody2D.velocity.x));
		
		// Move the rigid body
		if (!isFlossHanging && !lockPosition)
			rigidbody2D.velocity = new Vector2 (velocity.x * Time.deltaTime, rigidbody2D.velocity.y);

		if (isFlossHanging) {
			rigidbody2D.AddForce (new Vector2(Input.GetAxis ("Horizontal") * 6, Input.GetAxis ("Vertical") * 6));
			rigidbody2D.AddForce (new Vector2(joystickPos.x * 6, joystickPos.y * 6));
		}

		// Flip the sprite if not facing where the char is moving towards
		if (velocity.x > 0 && !facingRight)
			flip();
		else if (velocity.x < 0 && facingRight)
			flip ();

		// Keyboard
		if (Input.GetKeyDown (KeyCode.Z)) {
			jump ();
		}
		if (Input.GetKeyDown (KeyCode.X)) {
			attack ();
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			tossDentalFloss ();
		}

		// Mobile touch
		foreach (Touch t in Input.touches)
		{
			if (t.position.x > Screen.width / 2f)
			{
				if (t.position.y < Screen.height / 2f)
					attack ();
				else
					if (t.phase == TouchPhase.Began)
						jump ();
			}
		}
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey(KeyCode.Escape)) {
				//Application.LoadLevel ("MainMenu");

				return;
			}
		}
		// update floss line
		dentalFlossLineRenderer.SetPosition (0, transform.position + new Vector3(0,0,0.2f));
	}

	// Jump
	void jump(bool force = false) {
		if (!lockPosition || force) {
			// If hanging, jump out
			if (isFlossHanging) {
				distanceJoint2D.enabled = false;
				isFlossHanging = false;
				anim_main.SetBool ("isFlossHanging", false);
				dentalFlossLineRenderer.enabled = false;
				rigidbody2D.AddForce (new Vector2 (0, jumpForce * 0.6f));
				accel = new Vector2 (250f * (facingRight?1:-1), 0);
			} 
			// if not, normal jump
			else
			if (grounded) {
				if (canJump) {
					anim_main.SetBool ("Grounded", false);
					rigidbody2D.AddForce (new Vector2 (0, jumpForce));
					canJump = false;
					Invoke ("canJumpAgain", 0.33f);
				}
			} 
			// if not flossHanging but on the air, tossFloss
			else {
				tossDentalFloss ();
			}
		}
	}

	// Attack
	void attack() {
		if (!lockPosition && hasEscova) {
			anim_escova.SetTrigger ("Attack");
			anim_psbubbles.SetTrigger ("Attack");
			attacking = true;
			attackHandled = false;
		}
	}

	// Toss and hook dental floss
	void tossDentalFloss() {
		if ((!lockPosition) && (hasFloss)) {
			// calculate
			Vector3 flossPos = calculateFlossToss () + new Vector3(0,0.2f,0);
			// if error, return
			if (flossPos.z == 100)
				return;

			// draw line
			dentalFlossLineRenderer.enabled = true;
			dentalFlossLineRenderer.SetPosition (0, transform.position + new Vector3(0,0,0.2f));
			dentalFlossLineRenderer.SetPosition (1, flossPos);

			// joint
			distanceJoint2D.enabled = true;
			distanceJoint2D.connectedAnchor = new Vector2(flossPos.x, flossPos.y);
			distanceJoint2D.distance = 4f;//Vector2.Distance(new Vector2(flossPos.x, flossPos.y), transform.position) * 1.0f;

			isFlossHanging = true;
			anim_main.SetBool ("isFlossHanging", true);
			rigidbody2D.velocity = new Vector2(10 * transform.localScale.x,0);
		}
	}

	public static void RotateZ( ref Vector3 v, float angle )
	{
		float sin = Mathf.Sin( angle );
		float cos = Mathf.Cos( angle );
		
		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (cos * ty) + (sin * tx);
	}

	// Dental Floss toss. If found, return the position with Z = 0, if not, Z = 100
	Vector3 calculateFlossToss() {
		Vector3 ray = new Vector3 (0, 1, 0);
		//RotateZ ();
		RaycastHit2D rayHit = Physics2D.Raycast (transform.position, ray, 5f, whatIsHookable);
		if (rayHit.collider == null)
			return new Vector3 (0,0,100);


		return new Vector3 (rayHit.point.x, rayHit.point.y, 0);
	}

	// Called from an invoke after jump
	void canJumpAgain()
	{
		canJump = true;
	}

	// Callback from escova script
	public void attackStop() {
		attacking = false;
	}
	public void attackStart() {
		attacking = true;
	}

	void OnTriggerEnter2D(Collider2D coll) {
	}

	// Flip the character
	void flip () {
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	public void gotItem(int type) {
		if (type == 1) {
			transform.GetChild(4).gameObject.SetActive (true);
		}
		if (type == 2) {
			transform.GetChild(6).gameObject.SetActive (true);
			transform.GetChild(7).gameObject.SetActive (true);
			hasFloss = true;
		}
		if (type == 3) {
			hasEscova = true;
		}
	}
}
