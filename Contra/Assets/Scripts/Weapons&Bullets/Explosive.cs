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
        //spawn an explosion
        explosion.Spawn(_transform.position);
        //shake the screen
        Camera.main.GetComponent<CameraShake>().Shake(0.3f, 0.9f, 0, 0);
        base.OnCollisionEnter2D(col);
    }
}
