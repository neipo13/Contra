using UnityEngine;
using System.Collections;
using Prime31.ZestKit;

[RequireComponent(typeof(CharacterController2D))]
public class MobilePlayerControl : BouncyCharacter
{
    public float moveSpeed = 8.0f;
    public float gravity = 15.0f;
    public float targetJumpHeight = 2.0f;

    public bool canDoubleJump = false;
    protected bool doubleJumpUsed;

    protected WeaponManager weaponManager;
    protected CharacterController2D controller;
    protected Vector2 velocity;

    protected Transform _spriteTransform;
    public GameObject CrossHair;
    public bool shooting;

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<CharacterController2D>();
        weaponManager = GetComponent<WeaponManager>();
        doubleJumpUsed = false;
        //get sprite/animator data from child
        _animator = GetComponentInChildren<Animator>();
        _spriteTransform = _animator.gameObject.GetComponent<Transform>();
        startingScale = _spriteTransform.localScale;
        Invoke("StartShooting", 2f);
    }
    
    void StartShooting()
    {
        shooting = true;
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
                if (!controller.wasGroundedLastFrame)
                {
                    //Debug.Log("Was not grounded last frame");
                    //bounce
                    _spriteTransform.localScale = new Vector3(startingScale.x * 1.25f, startingScale.y * .75f, startingScale.z);
                    _spriteTransform.ZKlocalScaleTo(startingScale, 0.2f).start();
                }
            }

            //fire at crosshair
            if(shooting)
                weaponManager.currentWeapon.Shoot();

#if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began && touch.position.x < (Screen.width/3))
                    {
                        if (controller.isGrounded)
                        {
                            velocity.y = Mathf.Sqrt(2f * targetJumpHeight * gravity);
                            _spriteTransform.localScale = new Vector3(startingScale.x * 0.75f, startingScale.y * 1.25f, startingScale.z);
                            _spriteTransform.ZKlocalScaleTo(startingScale, 0.3f).start();
                        }
                        else if (canDoubleJump && !doubleJumpUsed)
                        {
                            doubleJumpUsed = true;
                            velocity.y = Mathf.Sqrt(1.5f * targetJumpHeight * gravity);
                            _spriteTransform.localScale = new Vector3(startingScale.x * 0.75f, startingScale.y * 1.25f, startingScale.z);
                            _spriteTransform.ZKlocalScaleTo(startingScale, 0.3f).start();
                        }
                    }
                    else if(touch.phase != TouchPhase.Canceled && touch.phase != TouchPhase.Ended && touch.position.x >= (Screen.width/2))
                    {
                        //fire at touch position
                    }
                }
                
            }
#endif

#if UNITY_EDITOR
            //jump
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (controller.isGrounded)
                {
                    velocity.y = Mathf.Sqrt(2f * targetJumpHeight * gravity);
                    _spriteTransform.localScale = new Vector3(startingScale.x * 0.75f, startingScale.y * 1.25f, startingScale.z);
                    _spriteTransform.ZKlocalScaleTo(startingScale, 0.3f).start();
                }
                else if (canDoubleJump && !doubleJumpUsed)
                {
                    doubleJumpUsed = true;
                    velocity.y = Mathf.Sqrt(1.5f * targetJumpHeight * gravity);
                    _spriteTransform.localScale = new Vector3(startingScale.x * 0.75f, startingScale.y * 1.25f, startingScale.z);
                    _spriteTransform.ZKlocalScaleTo(startingScale, 0.3f).start();
                }
            }
#endif

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
        if (!paused)
            controller.move(velocity * Time.deltaTime);
    }

    void SpriteFlip()
    {
        _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
    }
}
