using UnityEngine;
using System.Collections;

public class SpreadWeapon : Weapon 
{
    public int bulletsPerShot = 3;                      //number of bullets to create
    public int ammoConsumedPerShot = 1;                 //how much ammo should each shot consume
    public float ySpreadMax = 0.5f;                     //max/min Y velocity

    public override void Shoot()
    {
        //check if you can fire yet (recently fired/reloading/outOfBullets)
        if (!reloading && Time.time > nextFireTime)
        {
            if (currentInClip > 0)
            {
                float yPos = bulletVelocityY + ySpreadMax;
                for(int i = 0; i < bulletsPerShot; i++)
                {
                    //create a bullet
                    GameObject bullet = projectileType.Spawn(_transform.position + (Vector3)muzzleLocation);
                    bullet.GetComponent<Projectile>().SetDirection(
                        Mathf.Sign(_transform.parent.localScale.x),
                        (yPos - (2 * ySpreadMax * i / bulletsPerShot)) + Random.Range(-bulletInaccuracy, bulletInaccuracy));
                }
                //shake the screen
                camShake.Shake(screenShakeKick, screenShakeDegredation, (int)Mathf.Sign(_transform.parent.localScale.x), 0);
                //set next fire time
                nextFireTime = Time.time + fireRate;
                //reduce clip size
                currentInClip -= ammoConsumedPerShot;
                if (currentInClip <= 0)
                {
                    Reload();
                }
            }
            else
            {
                Reload();
            }
        }
    }
}
