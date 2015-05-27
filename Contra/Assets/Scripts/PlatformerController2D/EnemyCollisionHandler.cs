using UnityEngine;
using System.Collections;

public class EnemyCollisionHandler : MonoBehaviour 
{
    //reference to the class that handles motion for enemy characters
    protected EnemyController _enemyController;
    protected Transform _transform;

    public virtual void Awake()
    {
        _transform = GetComponent<Transform>();
        _enemyController = _transform.parent.gameObject.GetComponent<EnemyController>();
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        //reset the localPosition so it stays with the parent
        _transform.localPosition = Vector3.zero;
        Debug.Log("Hit " + col.collider.name);
        //add anything you want to happen on collisionenter here
        if(col.collider.tag == "playerBullet")
        {
            col.gameObject.Recycle();
        }
        _enemyController.SpriteFlip();
    }

    public virtual void OnCollisionStay2D(Collision2D col)
    {
        //reset localposition to keep rigidbody2D in correct place
        _transform.localPosition = Vector3.zero;
    }

    public virtual void OnCollisionExit2D(Collision2D col)
    {
        //reset localposition to keep rigidbody2D in correct place
        _transform.localPosition = Vector3.zero;
    }
}
