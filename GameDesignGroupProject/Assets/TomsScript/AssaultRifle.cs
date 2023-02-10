using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssaultRifle : MonoBehaviour
{
    private Inputs controls;
    public PlayerWeapon PWeapon;
    private float ammoMag;
    private float ammoTotal;
    private float ammoUse;
    private float damage;
    private float firingDelay;
    private float reloadTime;
    private float range;

    private void Awake()
    {
        PWeapon = GetComponent<PlayerWeapon>();
        controls = new Inputs();
    }
    
    // Start calls the weapons stats
    void Start()
    {
        
        ammoMag = 30;
        ammoTotal = ammoMag * 6;
        ammoUse = 1;
        damage = 10;
        // Time it takes to fire another shot
        firingDelay = 0.05f;
        reloadTime = 3f;
        range = 40;
    }

    // Update is called once per frame
    void Update()
    {
        // When this weapons swap key is pressed, set all the variables in PlayerWeapon to equal the weapon stats
        // This is so that PlayerWeapon can handle ammo, reloading, shooting etc.
        if (controls.Player.SwapWeapon1.IsPressed() && PWeapon.currentWeapon != "Assault Rifle")
        {
            PWeapon.PlayerWeaponSwap("Assault Rifle");
            PWeapon.weaponAmmoMag = ammoMag;
            PWeapon.weaponAmmoCurrent = ammoMag;
            PWeapon.weaponAmmoTotal = ammoTotal;
            PWeapon.weaponAmmoUse = ammoUse;
            PWeapon.weaponDamage = damage;
            PWeapon.weaponFiringDelay = firingDelay;
            PWeapon.weaponReloadTime = reloadTime;
            PWeapon.weaponRange = range;
        }

        if (PWeapon.currentWeapon == "Assault Rifle")
        {
            ammoTotal = PWeapon.weaponAmmoTotal;
        }

    }
}
