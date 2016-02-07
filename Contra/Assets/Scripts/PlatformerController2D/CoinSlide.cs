using UnityEngine;
using System.Collections;

public class CoinSlide : PausableMonoBehavior 
{
    CircleCollider2D col;
    public float slideSpeed = 1.0f;
    CharacterController2D controller;
    Vector2 vel;
    public float gravity = 15.0f;
    public float lifeTime = 5.0f;
    protected float remainingLife;


    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        controller = GetComponent<CharacterController2D>();
        remainingLife = lifeTime;
    }

	// Update is called once per frame
	void Update () 
    {
        slideSpeed = StaticGameVariables.gameSpeed * 1.5f;
        remainingLife -= Time.deltaTime;
        if (remainingLife <= 0.0f)
        {
            gameObject.Recycle();
        }
        if(_transform.position.y < -10)
        {
            gameObject.Recycle();
        }
        vel = controller.velocity;

	   if(controller.isGrounded)
       {
           vel.x = -slideSpeed;
       }
       vel.y -= gravity * Time.deltaTime;
	}

    void FixedUpdate()
    {
        if (!paused)
            controller.move(vel * Time.deltaTime);
    }
}
