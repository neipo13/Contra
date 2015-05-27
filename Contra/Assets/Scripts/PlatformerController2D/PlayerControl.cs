using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CharacterController2D))]
public class PlayerControl : PausableMonoBehavior 
{
    public float moveSpeed = 8.0f;
    public float gravity = 15.0f;
    public float targetJumpHeight = 2.0f;

    public bool canDoubleJump = false;
    protected bool doubleJumpUsed;

    protected WeaponManager weaponManager;
    protected CharacterController2D controller;
    protected Vector2 velocity;

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController2D>();
        weaponManager = GetComponent<WeaponManager>();
        doubleJumpUsed = false;
    }

    void Update()
    {
        if (!paused)
        {
            velocity = controller.velocity;

            if (controller.isGrounded)
            {
                velocity.y = 0.0f;
                doubleJumpUsed = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                velocity.x = -moveSpeed;
                if (_transform.localScale.x > 0)
                {
                    SpriteFlip();
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                velocity.x = moveSpeed;
                if (_transform.localScale.x < 0)
                {
                    SpriteFlip();
                }
            }
            else
            {
                velocity.x = 0;
            }

            //shoot
            if ((weaponManager.currentWeapon.fullauto && Input.GetKey(KeyCode.J)) || Input.GetKeyDown(KeyCode.J))
            {
                weaponManager.currentWeapon.Shoot();
            }

            if(Input.GetKeyDown(KeyCode.W))
            {
                weaponManager.SwapWeapons();
            }

            //jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (controller.isGrounded)
                {
                    velocity.y = Mathf.Sqrt(2f * targetJumpHeight * gravity);
                }
                else if (canDoubleJump && !doubleJumpUsed)
                {
                    doubleJumpUsed = true;
                    velocity.y = Mathf.Sqrt(1.5f * targetJumpHeight * gravity);
                }
            }

            velocity.y -= gravity * Time.deltaTime;

            if (Mathf.Abs(velocity.x) > 0 && controller.isGrounded)
            {
                _animator.SetBool("walking", true);
            }
            else
            {
                _animator.SetBool("walking", false);
            }
        }
    }

    void FixedUpdate()
    {
        controller.move(velocity * Time.deltaTime);
    }

    void SpriteFlip()
    {
        _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
    }
}
