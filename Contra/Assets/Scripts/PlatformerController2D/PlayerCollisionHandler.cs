using UnityEngine;
using System.Collections;

public class PlayerCollisionHandler : MonoBehaviour 
{
    protected PlayerControl _playerControl;
    protected Transform _transform;

    public virtual void Awake()
    {
        _transform = GetComponent<Transform>();
        _playerControl = _transform.parent.gameObject.GetComponent<PlayerControl>();
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        //reset localposition to keep rigidbody2D in correct place
        _transform.localPosition = Vector3.zero;
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
