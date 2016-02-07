using UnityEngine;
using System.Collections;

public class PickupCollision : MonoBehaviour 
{
    GameObject gameMaster;
    public GameObject heavyMachineGun;
    PowerupManager powerupManager;

    void Start()
    {
        gameMaster = GameObject.Find("GameMaster");
        powerupManager = gameMaster.GetComponent<PowerupManager>();
    }
	void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(gameObject.tag);
        if(col.tag == "Player")
        {
            if(gameObject.tag == "coin")
            {
                //Debug.Log("Coin Message Sent");
                gameMaster.SendMessage("CoinCollected");
            }
            else if(gameObject.tag == "heavyPowerup")
            {
                powerupManager.PlayEffect(gameObject.tag);
                //x=0.5 y=-0.2
                GameObject player = GameObject.Find("Player");
                WeaponManager weapons = player.GetComponent<WeaponManager>();
                GameObject weapon = heavyMachineGun.Spawn(player.transform, new Vector3(0.5f, -0.2f, 0f));
                weapons.weaponB = weapon.GetComponent<Weapon>();
                weapons.spriteB = weapon.GetComponent<SpriteRenderer>();
                weapons.SwapToWeapon('B');
            }
            gameObject.Recycle();
        }
    }
}
