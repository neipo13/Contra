using UnityEngine;
using System.Collections;

public class BouncyProjectile : Projectile 
{
    public override void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag != "ground")
            gameObject.Recycle();
    }
}
