using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{   public int maxHealth = 100;
    public int currentHealth;
     public HealthSystem playerHealthsystem;
    void Start()
    {
        currentHealth = maxHealth;
        playerHealthsystem.SetHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            TakeDamage(20);
        }
    }
    public void TakeDamage(int damage){
       currentHealth -= damage;
       playerHealthsystem.SetHealth(currentHealth);
    }
}
