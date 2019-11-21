using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss_1 : MonoBehaviour
{

    public int health = 100;
    public Slider HealthBar;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = health;
    }


    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        HealthBar.value = _currentHealth;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
