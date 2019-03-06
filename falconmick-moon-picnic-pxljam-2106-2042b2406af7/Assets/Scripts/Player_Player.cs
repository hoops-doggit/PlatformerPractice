using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player_Controller2D)) ]
public class Player_Player : MonoBehaviour {

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;


    float moveSpeed = 6;
    float gravity = -20;
    float jumpVelocity = 8; 
    Vector3 velocity;

    


    Player_Controller2D controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<Player_Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}
	
	// Update is called once per frame
	void Update () {

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        velocity.x = input.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
		
	}
}
