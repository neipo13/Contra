using UnityEngine;
using System.Collections;

public class Projectile : PausableMonoBehavior 
{
    public float moveSpeed = 5.0f;              //how fast it moves
    protected Vector2 moveDirection;            //direction to move in x and y
    public float maxLifetimeInSeconds = 10.0f;  //maximum number of seconds this object can live

    private float originalXScale;

    public override void Awake()
    {
        base.Awake();
        originalXScale = _transform.localScale.x;
    }

    /// <summary>
    /// Accepts a direction for a bullet to travel and sets it's velocity
    /// </summary>
    /// <param name="dirX">Must be either -1 of 1</param>
    /// <param name="dirY">Must be between -1 and 1</param>
    public void SetDirection(float dirX, float dirY = 0f)
    {
        moveDirection = new Vector2(dirX, dirY);
        if (dirX < 0)
        {
            _transform.localScale = new Vector3(originalXScale * -1, _transform.localScale.y, _transform.localScale.z);
        }
        else
        {
            _transform.localScale = new Vector3(originalXScale, _transform.localScale.y, _transform.localScale.z);
        }
        _rigidbody.velocity = moveDirection.normalized * moveSpeed;
        Invoke("LivedTooLong", maxLifetimeInSeconds);
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
            gameObject.Recycle();
    }

    public virtual void LivedTooLong()
    {
        gameObject.Recycle();
    }

}
