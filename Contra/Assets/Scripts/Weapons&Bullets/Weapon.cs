﻿using UnityEngine;
using System.Collections;

public class Weapon : PausableMonoBehavior 
{
    public GameObject projectileType;                       //prefab of desired projectile type
    public Vector2 muzzleLocation;                          //how far from center of gun is the location you want bullets to spawn
    protected CameraShake camShake;                         //cached version of the main camera's camera shaker, for visual recoil

    public float visualKick = 0.3f;                         //intensity of gun sprite's visual kick
    public float screenShakeKick = 0.3f;                    //intensity of gun's screenshake
    public float screenShakeDegredation = 0.5f;            //rate at which screen returns to normal, higher = slower to stop
    public float tangibleKick = 0.3f;                       //amount of force to push player back
    
    public bool fullauto = true;                            //can the player hold down shoot button or do they need to tap
    public float fireRate = 0.1f;                           //minimum time between firing, lower = faster fire rate
    protected float nextFireTime;                           //time the next shot can occur

    public int clipSize = 32;                               //max bullets in a clip
    protected int currentInClip;                            //bullets currently in the clip
    public float reloadSpeed = 0.5f;                        //amount of time to refill a clip, lower = reload faster
    protected bool reloading = false;                       //flag for reloading

    [Range(-1f, 1f)]
    public float bulletVelocityY = 0.0f;                    //

    public void Start()
    {
        currentInClip = clipSize;
        nextFireTime = 0.0f;
        projectileType.CreatePool(clipSize + 1);
        camShake = Camera.main.GetComponent<CameraShake>();
    }

    public void Shoot()
    {
        //check if you can fire yet (recently fired/reloading/outOfBullets)
        if(!reloading && Time.time > nextFireTime)
        {
            if(currentInClip > 0)
            {
                //create a bullet
                GameObject bullet = projectileType.Spawn(_transform.position + (Vector3)muzzleLocation);
                bullet.GetComponent<Projectile>().SetDirection(Mathf.Sign(_transform.parent.localScale.x), bulletVelocityY);
                //shake the screen
                camShake.Shake(screenShakeKick, screenShakeDegredation, (int)Mathf.Sign(_transform.parent.localScale.x), 0);
                //set next fire time
                nextFireTime = Time.time + fireRate;
                //reduce clip size
                currentInClip--;
                if(currentInClip <= 0)
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

    public void Reload()
    {
        //check if currently reloading
        //if not begin reload
        //on reload end, fill clip
        if(!reloading)
        {
            reloading = true;
            StartCoroutine("ReloadTimer");
        }
    }

    IEnumerator ReloadTimer()
    {
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadSpeed);
        currentInClip = clipSize;
        reloading = false;
        Debug.Log("done");
    }
}
