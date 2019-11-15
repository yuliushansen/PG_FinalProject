using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text scoreText;
    int score = 0;
    public Text bestText;
    public Text CurrentText;
    public GameObject newAlert;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver() {
        Invoke("ShowOverPanel", 2.0f);
    }
    
    public void Restart() {
        Application.LoadLevel(Application.loadedLevelName);
    }
    
    void ShowOverPanel() {
        scoreText.gameObject.SetActive(false);

        if(score > PlayerPrefs.GetInt("Best",0)){
            newAlert.SetActive(true);
            PlayerPrefs.SetInt("Best",score);
        }
        bestText.text = "Best Score : "+ PlayerPrefs.GetInt("Best",0).ToString();
        CurrentText.text = "Current Score : " + score.ToString(); 
        gameOverPanel.SetActive(true);
    }

    public void IncrementScore() {
        score++;
        scoreText.text = score.ToString();
    }
}
