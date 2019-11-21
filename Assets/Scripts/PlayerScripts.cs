using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerScripts : MonoBehaviour
{
    Animator animator;
    ChallengeController myChallenge;
    public float jumpPower = 10.0f;
    float posX = 0.0f;
    Rigidbody2D myRigidbody;
    bool isGround = false;
    bool isGameOver = false;
    GameController myGameController;
    //  bool isRun = true;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = transform.GetComponent<Rigidbody2D>();
        posX = transform.position.x;
        animator = GetComponent<Animator>();
        myChallenge = GameObject.FindObjectOfType<ChallengeController>();
        myGameController = GameObject.FindObjectOfType<GameController>();
    }

    //void FixedUpdate()
    //{
        
    //}
    void GameOver() {
        isGameOver = true;
        myChallenge.GameOver();
        animator.SetBool("isDeath",true);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !isGameOver)
        {
            myRigidbody.AddForce(Vector3.up * (jumpPower * myRigidbody.mass * myRigidbody.gravityScale * 20.0f));
            animator.SetBool("isRun", false);
            animator.SetBool("isJump", true);
        }
        //Hit 
        if (transform.position.x < posX)
        {
            GameOver();
        }
    }

    void OnCollisionEnter2D (Collision2D other) {
        if(other.collider.tag == "Ground") {
            isGround = true;
            animator.SetBool("isRun",true);
            animator.SetBool("isJump", false);
        }
        else if(other.collider.tag == "Enemy"){
            GameOver();
        }
        else if(other.collider.tag == "Portal"){
            SceneManager.LoadScene(1);
        }
          
    }

    void OnCollisionStay2D (Collision2D other) {
        if(other.collider.tag == "Ground") {
            isGround = true;
        }
    }

    void OnCollisionExit2D (Collision2D other) {
        if(other.collider.tag == "Ground") {
            isGround = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag =="Score"){
            myGameController.IncrementScore();
            Destroy(other.gameObject);
        }
    }
}
