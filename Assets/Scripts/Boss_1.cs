using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss_1 : MonoBehaviour
{

    public int health = 500;
    public Slider HealthBar;
    private Animator anim;
    public float delay = 0f;
    public GameObject Portal;
    private float _currentHealth;
    public float speed;
    public float distance;
    private bool movingRight = true;
    public Transform groundDetection;

    private void Start()
    {
        _currentHealth = health;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if(movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
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
