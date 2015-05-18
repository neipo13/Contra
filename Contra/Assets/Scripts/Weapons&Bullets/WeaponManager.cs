using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour 
{
    public Weapon weaponA;
    public SpriteRenderer spriteA;
    public Weapon weaponB;
    public SpriteRenderer spriteB;
    public Weapon currentWeapon;

    public void Start()
    {
        currentWeapon = weaponA;
        weaponA.enabled = spriteA.enabled = true;
        weaponB.enabled = spriteB.enabled = false;
    }

    public void SwapWeapons()
    {
        if(currentWeapon == weaponA)
        {
            weaponA.enabled = spriteA.enabled = false;
            weaponB.enabled = spriteB.enabled = true;
            currentWeapon = weaponB;
        }
        else if(currentWeapon == weaponB)
        {
            weaponB.enabled = spriteB.enabled = false;
            weaponA.enabled = spriteA.enabled = true;
            currentWeapon = weaponA;
        }
    }
}
