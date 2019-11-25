using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeController : MonoBehaviour
{
    public float scrollSpeed = 5.0f;
    public GameObject[] challenges;
    public GameObject[] portal;
    public float frequency = 0.5f;
    float counter = 0.0f;
    public Transform challengesSpawnPoint;
    bool isGameOver = false;
    // float counterSpeed = 0.0f;
    public int targetTime = 600;
    bool isPortalCreated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomChallenge();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isGameOver) {
            return;
        }
        //Generate Challenge
        if(counter <= 0.0f){
            
            if (targetTime <= 0.0f)
            {
                if (!isPortalCreated)
                {
                    GeneratePortal();
                }
            }
            else
            {
                GenerateRandomChallenge();
            }
        }
        else
        {
            counter -= Time.deltaTime * frequency;
            
            // if(counterSpeed == 5.0f){
            //     scrollSpeed +=0.01f;
            // }
            // counterSpeed++;
        }
        //Scrolling
        GameObject currentChild;
        for (int i = 0; i < transform.childCount; i++){
            currentChild = transform.GetChild(i).gameObject;
            ScrollChallenge(currentChild);
            if (currentChild.transform.position.x <= -15.0f)
            {
                Destroy(currentChild);
            }
        }
        
    }
    public void GameOver() {
        isGameOver = true;
        transform.GetComponent<GameController>().GameOver();
    }

    void ScrollChallenge (GameObject currentChallenge) {
        currentChallenge.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
    }

    void GenerateRandomChallenge () {
        GameObject newChallenge = Instantiate(challenges[Random.Range(0,challenges.Length)], challengesSpawnPoint.position, Quaternion.identity) as GameObject;
        newChallenge.transform.parent = transform;
        counter = 1.0f;
        targetTime--;
        isPortalCreated = false;
    }
    void GeneratePortal () {
        GameObject newportal = Instantiate(portal[Random.Range(0,portal.Length)], challengesSpawnPoint.position, Quaternion.identity);
        newportal.transform.parent = transform;
        isPortalCreated = true;
    }

}
