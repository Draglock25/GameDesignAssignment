using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    private Inputs controls;
    public EnemyHealth enemy;
    public float weaponAmmoMag;
    public float weaponAmmoTotal;
    public float weaponAmmoUse;
    public float weaponAmmoCurrent;
    public float weaponDamage;
    public float weaponFiringDelay;
    private float firingDelayTimer;
    public float weaponReloadTime;
    private float reloadTimer;
    public string currentWeapon;
    public float weaponRange;
    void Start()
    {
        controls = new Inputs();
        currentWeapon = "Fists";
    }

    // Update is called once per frame
    void Update()
    {

        // This checks if the weapon can pull from the weapons total ammo when reloading
        if (controls.Player.Reload.IsPressed())
        {
            if (weaponAmmoTotal >= weaponAmmoMag)
            {
                weaponAmmoTotal -= (weaponAmmoMag - weaponAmmoCurrent);
                weaponAmmoCurrent = weaponAmmoMag;
            }
            else if(weaponAmmoTotal > 0)
            {
                weaponAmmoCurrent = weaponAmmoTotal;
                weaponAmmoTotal = 0;
            } else
            {
                Debug.Log("Ran out of ammo!");
            }
            reloadTimer = weaponReloadTime;

        }
        // Weapon Shoots
        // Atm no way to do other firing modes other than automatic, although I have an idea how to implement that. Thomas.
        if(firingDelayTimer <= 0 && reloadTimer <= 0)
        {
            if (controls.Player.Interact.IsPressed())
            {
                if (weaponAmmoCurrent > 0)
                {
                    PlayerShoot();
                }
                else
                {
                    Debug.Log("Player attempting to shoot, out of ammo!");
                }
            }
        }

        //Holsters weapon and brings out "Fists"
        //Can remove damage if melee is not needed. Thomas.
        if (controls.Player.Holster.IsPressed() && currentWeapon != "Fists")
        {
            PlayerWeaponSwap("Fists");
            weaponAmmoMag = 0;
            weaponAmmoCurrent = 0;
            weaponAmmoTotal = 0;
            weaponAmmoUse = 0;
            weaponDamage = 5;
            weaponFiringDelay = 2;
            weaponReloadTime = 0;
            weaponRange = 1;
        }
    }
    // This allows a weapon id system, this can be changed to anything else, I've chosen a naming system
    public void PlayerWeaponSwap(string weapon)
    {
        currentWeapon = weapon;
        Debug.Log("Swapping weapon to " + currentWeapon + "!");
        reloadTimer = 0;
    }

    private void FixedUpdate()
    {
        if(firingDelayTimer > 0)
        {
            firingDelayTimer -= Time.deltaTime;
        }

        if(reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
        }
    }

    public void PlayerShoot()
    {
        weaponAmmoCurrent -= weaponAmmoUse;
        firingDelayTimer = weaponFiringDelay;
        if(weaponAmmoCurrent < 0)
        {
            weaponAmmoCurrent = 0;
        }
        //Place holder for raycasting shooting
        Debug.Log("Player shot!");

    }
}
