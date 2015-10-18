using UnityEngine;
using System.Collections;

public class CoinSlide : PausableMonoBehavior 
{
    CircleCollider2D col;
    public float slideSpeed = 1.0f;
    CharacterController2D controller;
    Vector2 vel;
    public float gravity = 15.0f;

    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        controller = GetComponent<CharacterController2D>();
    }

	// Update is called once per frame
	void Update () 
    {
        if(_transform.position.y < -10)
        {
            gameObject.Recycle();
        }
        vel = controller.velocity;

	   if(controller.isGrounded)
       {
           vel.x = -StaticGameVariables.gameSpeed * slideSpeed;
       }
       if(col.IsTouchingLayers(LayerMask.NameToLayer("player")))
       {
           gameObject.Recycle();
       }
       vel.y -= gravity * Time.deltaTime;
	}

    void FixedUpdate()
    {
        if (!paused)
            controller.move(vel * Time.deltaTime);
    }
}
