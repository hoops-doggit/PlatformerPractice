using UnityEngine;
using System.Collections;
using Prime31;

public class PlayerController : MonoBehaviour 
{

	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	public Animator animator;
	public AudioClip[] clips;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	//private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
	private AudioSource audioSource;


	void Awake()
	{
		//_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		//Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		//Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		if (_controller.isGrounded) {
			_velocity.y = 0;
			animator.SetBool ("Grounded", true);
		}
		else
			animator.SetBool ("Grounded", false);

		if( Input.GetKey( KeyCode.RightArrow ) )
		{
			if (_controller.isGrounded) {
				animator.SetBool ("PlayerWalk", true);
				if(!audioSource.isPlaying)
					audioSource.PlayOneShot(clips [1], 0.13f);
			}
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
			{
				//_animator.Play( Animator.StringToHash( "Run" ) );
			}
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			if (_controller.isGrounded) {
				animator.SetBool ("PlayerWalk", true);
				if(!audioSource.isPlaying)
					audioSource.PlayOneShot(clips [1], 0.13f);
			}
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
			{
				//_animator.Play( Animator.StringToHash( "Run" ) );
			}
		}
		else
		{
			animator.SetBool ("PlayerWalk", false);
			normalizedHorizontalSpeed = 0;

			if( _controller.isGrounded )
			{
				//_animator.Play( Animator.StringToHash( "Idle" ) );
			}
		}


		// we can only jump whilst grounded
		if( _controller.isGrounded && Input.GetKeyDown( KeyCode.UpArrow ) )
		{
			PlayerJump();
		}


		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets us jump down through one way platforms
		if( _controller.isGrounded && Input.GetKey( KeyCode.DownArrow ) )
		{
			_velocity.y *= 3f;
			_controller.ignoreOneWayPlatformsThisFrame = true;
		}

		_controller.move( _velocity * Time.deltaTime );

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
	}

    public void PlayerJump(float heightPercent = 1f)
    {
        animator.SetTrigger("PlayerJump");
        _velocity.y = Mathf.Sqrt(2f*jumpHeight* heightPercent * -gravity);
        audioSource.PlayOneShot(clips[0], 0.35f);
    }

    public void PlayHitSound(){
		if(!audioSource.isPlaying)
			audioSource.PlayOneShot (clips[3], 0.3f);
	}

	public void PlayButtonSound(){
		if(!audioSource.isPlaying)
			audioSource.PlayOneShot (clips[4], 0.3f);
	}

}
