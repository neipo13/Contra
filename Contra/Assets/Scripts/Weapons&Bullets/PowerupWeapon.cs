using UnityEngine;
using System.Collections;

public class PowerupWeapon : Weapon 
{
    public override void Reload()
    {
        //when you run out of bullets, switch back to the default weapon
        GameObject.Find("Player").GetComponent<WeaponManager>().SwapToWeapon('A');
    }
}
