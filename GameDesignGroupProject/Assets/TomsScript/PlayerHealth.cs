using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth: MonoBehaviour
{
    public float playerCurrentHealth;
    public float playerMaxHealth = 100f;
    public float invLength = 1f;
    private float invCounter;
    private float regenTimer;
    private float fastregenTimer;
    public float regenRate = 2f;
    public float fastRegenRate = 5f;
    //public Vector3 checkpointCoords;


    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        // Ticks down invulnerability frames
        if (invCounter > 0)
        {
            invCounter -= Time.deltaTime;
        }

        // Ticks down time until player starts to regenerate health
        if (regenTimer > 0)
        {
            regenTimer -= Time.deltaTime;
        }

        if (fastregenTimer > 0)
        {
            fastregenTimer -= Time.deltaTime;
        }

        // Regenerates the players health outside of "Combat"
        if (regenTimer <= 0 && playerCurrentHealth < playerMaxHealth)
        {
            playerCurrentHealth += (regenRate * Time.deltaTime);

            if (fastregenTimer <= 0 && playerCurrentHealth < playerMaxHealth)
            {
                playerCurrentHealth += (fastRegenRate * Time.deltaTime);
            }

            if(playerCurrentHealth > playerMaxHealth)
            {
                playerCurrentHealth = playerMaxHealth;
            }
        }

        // On player death, sends them back to a checkpoints coordinates
        if (playerCurrentHealth <= 0)
        {
            Debug.Log("Player has died, sending back to checkpoint!");
            //Send player to checkpoint coords
            playerCurrentHealth = playerMaxHealth;
        }
    }
    // When player collides with a gameobject tagged Enemy, damages player
    // Created to work as an example, however we should use this so that enemies run the DamagePlayer() function instead of the player running it. Thomas.
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            if (invCounter <= 0)
            {
                DamagePlayer(10);
            }

        }

    }

    // Causes the player to take damage
    // Gives them invunerability frames
    // Also sets a timer for when the player can regenerate health again
    public void DamagePlayer(float damage)
    {
        playerCurrentHealth -= damage;
        invCounter = invLength;
        regenTimer = 5;
        fastregenTimer = 8;
    }
}

    

