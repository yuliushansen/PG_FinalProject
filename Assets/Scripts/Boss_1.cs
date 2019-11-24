using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss_1 : MonoBehaviour
{

    public int health = 100;
    public Slider HealthBar;
    private Animator anim;
    public float delay = 0f;
    public GameObject Portal;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = health;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
    }

    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        HealthBar.value = _currentHealth;


        if (_currentHealth <= 0)
        {
            anim.SetTrigger("isDeath");
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
            Portal.SetActive(true);
        }
    }
}
