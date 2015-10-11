using UnityEngine;
using System.Collections;

public class Weapon : PausableMonoBehavior 
{
    public GameObject projectileType;                       //prefab of desired projectile type
    public Vector2 muzzleLocation;                          //how far from center of gun is the location you want bullets to spawn
    protected CameraShake camShake;                         //cached version of the main camera's camera shaker, for visual recoil

    public float visualKick = 0.3f;                         //intensity of gun sprite's visual kick
    public float screenShakeKick = 0.3f;                    //intensity of gun's screenshake
    public float screenShakeDegredation = 0.5f;             //rate at which screen returns to normal, higher = slower to stop
    public float tangibleKick = 0.3f;                       //amount of force to push player back
    public float bulletInaccuracy = 0.05f;                  //min/max velocity difference from standard the bullet can be aimed
    
    public bool fullauto = true;                            //can the player hold down shoot button or do they need to tap
    public float fireRate = 0.1f;                           //minimum time between firing, lower = faster fire rate
    protected float nextFireTime;                           //time the next shot can occur

    public int clipSize = 32;                               //max bullets in a clip
    protected int currentInClip;                            //bullets currently in the clip
    public float reloadSpeed = 0.5f;                        //amount of time to refill a clip, lower = reload faster
    protected bool reloading = false;                       //flag for reloading

    protected float cameraOrthoSize = 5;                    //gets the camera size for orthographic camera
    protected Transform crosshairTrans;                        //the transform of the crosshair

    [Range(-1f, 1f)]
    public float bulletVelocityY = 0.0f;                    //

    public virtual void Start()
    {
        currentInClip = clipSize;
        nextFireTime = 0.0f;
        projectileType.CreatePool(clipSize + 1);
        camShake = Camera.main.GetComponent<CameraShake>();
        crosshairTrans = GameObject.Find("crosshair").GetComponent<Transform>();
    }

    public virtual void Update()
    {
        Vector3 mouse_pos = crosshairTrans.position;
        mouse_pos.z = cameraOrthoSize; //The distance between the camera and object
        //transformPosWorld = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - _transform.position.x;
        mouse_pos.y = mouse_pos.y - _transform.position.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        //limit the angle so that the gun does not spin around completely
        if(angle < 55 && angle > -55)
            _transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public virtual void Shoot()
    {
        //check if you can fire yet (recently fired/reloading/outOfBullets)
        if(!reloading && Time.time > nextFireTime)
        {
            if(currentInClip > 0)
            {
                //create a bullet
                GameObject bullet = projectileType.Spawn(_transform.position + (Vector3)muzzleLocation);
                float x = crosshairTrans.position.x - _transform.position.x + Random.Range(-bulletInaccuracy, bulletInaccuracy);
                float y = crosshairTrans.position.y - _transform.position.y + Random.Range(-bulletInaccuracy, bulletInaccuracy);
                bullet.GetComponent<Projectile>().SetDirection(x, y);
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
        //Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadSpeed);
        currentInClip = clipSize;
        reloading = false;
        //Debug.Log("done");
    }
}
