﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player_BossScene
 : MonoBehaviour
{
    public Joystick joystick;
    public int health = 100;
    public Slider HealthBar;

    private float _currentHealth;
    //bool isEnemy;
    public Transform firePoint;
    public GameObject bulletPrefab;
    // untuk mengatur kecepatan saat Player bergerak
    [SerializeField] private float speed = 5.0f;
    // untuk komponen Rigidbody2D
    private Rigidbody2D rigidBody;
    // Untuk menyimpan nilai yang mengkondisikan
    //Player saat bergerak ke kanan atau ke kiri
    private float moveInput;
    // Untuk mengkondisikan benar saat Player menghadap ke kanan
    private bool facingRight;

    // memberikan nilai seberapa tinggi Player dapat melompat
    [SerializeField] private float jumpForce = 10.0f;
    // menandakan benar jika Player menyentuh pinjakan atau ground
    [SerializeField] private bool isGrounded;
    // memastikan bahwa posisi kaki Player berada di bawah,
    //seperti telapak kaki gitu loh sobat
    [SerializeField] private Transform feetPos;
    // ini digunakan untuk mengatur seberapa besar radius kaki Player sobat
    //"Kurang lebih seperti itu :)"
    [SerializeField] private float circleRadius;
    // Ini digunakan untuk memastikan object
    //yang bertindak / kita jadikan sebagai ground
    [SerializeField] private LayerMask whatIsGround;

    //variabel ini kita panggil
    //untuk menjalankan animasi idle, run, dan jump
    private Animator anim;
    float verticalMove;

    private void Start()
    {
        //inisialisasi komponen Rigidbody2D yang ada pada Player
        rigidBody = GetComponent<Rigidbody2D>();
        //kita set di awal BENAR karena Player menghadap ke kanan
        facingRight = true;
        //Inisialisasi komponen Animator yang ada pada Player
        anim = GetComponent<Animator>();
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
        //Destroy(gameObject);
        anim.SetBool("isDie", true);
        anim.SetBool("isHurt", false);
        //sound
        SoundMScript.PlaySound("death");
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Enemy")
        {
            //isEnemy = true;
            TakeDamage(5);
            anim.SetBool("isHurt", true);
            
        }
        else if (other.collider.tag == "Portal")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            anim.SetBool("isHurt", false);
        }

    }

    private void Update()
    {
        //Dengan memanggil class Physics2D dan fungsi OverlapCircle
        //yang memiliki 3 parameter ini menandakan bahwa
        //isGrounded akan bernilai benar jika ketiga parameter tersebut terpenuhi
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);

        //Fungsi untuk Player saat melompat
        CharacterJump();
        //Attack();
        
        //if (isEnemy)
        //{
        //    TakeDamage(5);
        //}
    }

    private void FixedUpdate()
    {
        // Fungsi yang memanage inputan
        //saat Player bergerak ke Kanan atau ke Kiri
        CharacterMovement();
        // Fungsi yang mengatur
        //transisi animasi Player
        //saat idle, run atau jump
        CharacterAnimation();

    }

    private void CharacterMovement()
    {
        //Input.GetAxis adalah sebuah fungsi
        //yang telah di sediakan oleh Unity
        //Untuk melihat keyboard inputannya sobat
        //buka di menu edit terus pilih Project Setting dan pilih Input
        moveInput = joystick.Horizontal;

        if (moveInput > 0 && facingRight == false)
        {

            Flip();
        }
        else if (moveInput < 0 && facingRight == true)
        {
            //Fungsi yang berguna agar Player
            //dapat menghadap ke kanan atau ke kiri
            Flip();
        }
        // nilai pada sumbu X akan bertambah sesuai dg speed * moveInput
        rigidBody.velocity = new Vector2(speed * moveInput, rigidBody.velocity.y);
    }

    void CharacterJump()
    {
        verticalMove = joystick.Vertical;
        //if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        if (isGrounded == true && verticalMove >= .5f)
        {
            //Cara memanggil animasi dengan
            //parameter yang bertipe Trigger
            anim.SetTrigger("isJump");
            //sound jump
            SoundMScript.PlaySound("jump");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void Attack()
    {

        //if (Input.GetKeyDown(KeyCode.B))
        //{
            //Cara memanggil animasi dengan
            //parameter yang bertipe Trigger
            anim.SetTrigger("isAttack");
            //suara tembakan
            SoundMScript.PlaySound("fire");
            Shoot();
        //}
    }

    void CharacterAnimation()
    {
        if (moveInput != 0 && isGrounded == true)
        {
            //cara memanggil animasi dengan
            //parameter yang bertipe BOOL
            anim.SetBool("isRun", true);

        }
        else if (moveInput == 0 && isGrounded == true)
        {
            anim.SetBool("isRun", false);
        }
    }

    private void Flip()
    {
        //facingRight bernilai tidak sama dengan facingRight
        facingRight = !facingRight;
        //membuat variabel dengan tipe Vector3
        //yang isinya = transform.localScale
        //(Scling pada sumbu x=1, y=1,z=1)
        //Vector3 scaler = transform.localScale;
        //lalu pada sumbu x di kalikan
        //dengan minus sehingga sumbu x
        //nantinya akan memiliki nilai minus
        //scaler.x *= -1;
        //dan terakhir sumbu x pada Player di berikan
        //nilai minus sehingga ketika Player menghadap
        //ke kiri sumbu x pada Player akan bernilai -1
        //transform.localScale = scaler;
        transform.Rotate(0f, 180f, 0f);

        //NOTE : Sumbu x ini sumbu x yang ada
        //pada Scale yang ada pada komponen Transform
    }
}