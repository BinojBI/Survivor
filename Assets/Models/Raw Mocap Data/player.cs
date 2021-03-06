﻿using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
    public float runSpeed;
    Rigidbody myRB;
    Animator myAnim;
    bool facingRight;

    //for jumping 
    bool grounded = false;
    Collider[] groundCollisions;
    float groundCheckRadious = .2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight; 

	// Use this for initialization
	void Start () {
        myRB = GetComponent<Rigidbody>();
        myAnim = GetComponent<Animator>();
        facingRight = true; 

	}
	
	// Update is called once per frame
	void Update () {

        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));

        myRB.velocity = new Vector3(move * runSpeed, myRB.velocity.y, 0);
        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();
	}

    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.z *= -1;
        transform.localScale = theScale;
    }
    void FixedUpdate() {

        if(grounded && Input.GetAxis("Jump")>0){
            grounded = false;
            myAnim.SetBool("grounded",grounded);
            myRB.AddForce(new Vector3(0,jumpHeight,0));
        }

groundCollisions = Physics.OverlapSphere(groundCheck.position,groundCheckRadious, groundLayer);
if(groundCollisions.Length>0) grounded=true;
else grounded = false;

myAnim.SetBool("grounded",grounded);
    }

}