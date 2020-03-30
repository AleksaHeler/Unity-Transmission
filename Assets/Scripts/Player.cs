using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player Settings")]
    public int maxHealth = 100;
    public HealthBar healthBar;

    [HideInInspector]
    public int health;

    void Start() {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        
    }

    public void TakeDamage(int amount) {
        health -= amount;
        healthBar.SetHealth(health);
        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        Debug.Log("Game Over!");
        Time.timeScale = 0;
    }
}
