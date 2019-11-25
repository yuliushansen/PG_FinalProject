using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip fireSound, jumpSound,playerDeath;
    static AudioSource aSource;
    void Start()
    {
        fireSound = Resources.Load<AudioClip>("fire");
        jumpSound = Resources.Load<AudioClip>("jump");
        playerDeath = Resources.Load<AudioClip>("playerDeath");
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void PlaySound(string clip){
        switch(clip){
            case "fire":
                aSource.PlayOneShot(fireSound);
                break;
            case "jump":
                aSource.PlayOneShot(jumpSound);
                break;
            case "death":
                aSource.PlayOneShot(playerDeath);
                break;
        }
    }
}
