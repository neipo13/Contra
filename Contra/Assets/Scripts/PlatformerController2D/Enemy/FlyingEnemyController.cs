using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController2D))]
public class FlyingEnemyController : PausableMonoBehavior
{
    public float moveSpeed = 6.0f;                          //enemy's movement speed
    public float gravity = 15.0f;                           //how gravity effects the character

    public float raycastDist = 1.0f;                        //distance from center to raycast down for cliff/ledge detection
    private Vector2 raycastVec;                             //for easy use
    private Vector2 raycastOrigin;
    private float turnTime = 0.2f;                          //amount of time to ignore ledge detection to allow character to move away from cliff
    private float turnTimer;

    protected CharacterController2D controller;
    protected Transform playerTrans;
    protected Vector2 velocity;

    [Range(-1, 1)]
    public int direction = 1;                 //positive means facing right, negative left

    public bool walksOffCliffs = false;                     //determines if the enemy should behave differently when reaching a cliff or ledge

    public enum EnemyStates { Patrol, Alert, Chase, Shoot, Die }
    protected StateMachine<EnemyStates> _sm;

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController2D>();
        controller.velocity = new Vector2(moveSpeed * direction, 0f);
        raycastVec = new Vector2(raycastDist, 0);
        turnTimer = -1f;
        playerTrans = GameObject.Find("Player").GetComponent<Transform>();
        //if the sprite is starting facing left, flip it
        if (direction == -1)
        {
            _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
        }
        CreateStateMachine();

    }
    public virtual void CreateStateMachine()
    {
        _sm = new StateMachine<EnemyStates>();
        _sm.Entity = gameObject;
        _sm.Script = this;
        _sm.Added();
        _sm.ChangeState(EnemyStates.Patrol);
    }
    public virtual void Update()
    {
        //if game is not paused
        if (!paused)
        {
            _sm.Update();
        }
    }

    public virtual void FixedUpdate()
    {
        if (!paused)
            controller.move(velocity * Time.deltaTime);
    }

    public void SpriteFlip()
    {
        direction *= -1;
        _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
    }

    public virtual void CliffFound()
    {
        turnTimer = turnTime;
        SpriteFlip();
    }

    public void UpdatePatrol()
    {
        //set velocity
        velocity = new Vector2(moveSpeed * direction, controller.velocity.y);
        if(Vector2.Distance(_transform.position, playerTrans.position) < 5)
        {
            _sm.ChangeState(EnemyStates.Chase);
        }
        //velocity = controller.velocity;
        //if character does not walk off cliffs, 
        if (!walksOffCliffs && turnTimer < 0)
        {
            raycastOrigin = new Vector2(_transform.position.x, controller.boxCollider.bounds.min.y);
            //check if raycast on either side is no longer over ground,
            RaycastHit2D hitR = Physics2D.Raycast(raycastOrigin + raycastVec, -Vector2.up);
            RaycastHit2D hitL = Physics2D.Raycast(raycastOrigin - raycastVec, -Vector2.up);
            //Draw the rays for debugging
            controller.DrawRay(raycastOrigin + raycastVec, -Vector2.up, Color.cyan);
            controller.DrawRay(raycastOrigin - raycastVec, -Vector2.up, Color.cyan);
            //if it is not over ground, call appropriate function
            if (hitR.collider == null || hitR.collider.tag != "ground"
                || hitL.collider == null || hitL.collider.tag != "ground")
            {
                CliffFound();
            }
        }
        else if (turnTimer > 0)
        {
            turnTimer -= Time.deltaTime;
        }
        //otherwise continue movement
        velocity.y -= gravity * Time.deltaTime;
    }

    public void UpdateChase()
    {
        velocity = new Vector2(playerTrans.position.x - _transform.position.x, playerTrans.position.y - _transform.position.y);
        velocity = velocity.normalized * moveSpeed;
    }
}
