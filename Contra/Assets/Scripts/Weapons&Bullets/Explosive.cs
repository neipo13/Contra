using UnityEngine;
using System.Collections;

public class Explosive : Projectile 
{
    public GameObject explosion;

    public override void Awake()
    {
        explosion.CreatePool(1);
        base.Awake();
    } 
    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "bulletDestroyer")
        {
            gameObject.Recycle();
        }
        else if (!(col.collider.tag == "ground" && _rigidbody.velocity.y > 0))
        {
            //spawn an explosion
            explosion.Spawn(_transform.position);
            //shake the screen
            Camera.main.GetComponent<CameraShake>().Shake(0.3f, 0.9f, 0, 0);
            gameObject.Recycle();
        }
    }
}
