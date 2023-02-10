using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyCurrentHealth;
    private float enemyMaxHealth;
    private float invLength = 0.5f;
    private float invCounter;
    // Start is called before the first frame update
    void Start()
    {
        enemyMaxHealth = 20;
        enemyCurrentHealth = enemyMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Ticks down invulnerability frames
        if (invCounter > 0)
        {
            invCounter -= Time.deltaTime;
        }

        if (enemyCurrentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy Has died!");
        }
    }

    public void DamageEnemy(float damage)
    {
        enemyCurrentHealth -= damage;
        invCounter = invLength;
    }

    //Placeholder for testing
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (invCounter <= 0)
            {
                DamageEnemy(10);
            }

        }

    }
}
