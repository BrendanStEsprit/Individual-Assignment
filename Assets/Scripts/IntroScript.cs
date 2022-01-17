using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement TimerController;
    private Vector2 offset;
    public bool cutsceneisplaying;
    Animator anim;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip musicClipThree;
    public AudioClip musicClipFour;
    public AudioSource musicSource;
    bool SoundHasPlayed =false;
    bool MusicHasPlayed = false;
    public bool Cutsceneisplaying { get { return cutsceneisplaying; } }
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        cutsceneisplaying = true;
        anim = GetComponent<Animator>();
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }
    private void Update()
    {
        GameObject AnotherObject = GameObject.FindWithTag("Player");
        TimerController = AnotherObject.GetComponent<PlayerMovement>();
        if(TimerController.Timer<0)
        {
            anim.SetInteger("State", 1);
            if(!SoundHasPlayed)
            {
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                SoundHasPlayed = true;
            }
            

        }
        if(TimerController.score==3)
        {
            anim.SetInteger("State", 2);
            if(!SoundHasPlayed)
            {
                musicSource.clip = musicClipThree;
                musicSource.Play();
                SoundHasPlayed = true;
            }
            
            
        }
        if(!cutsceneisplaying)
        {
            if(!MusicHasPlayed)
            {
                musicSource.clip = musicClipFour;
                musicSource.Play();
                MusicHasPlayed = true;
            }
            
        }
    }
    // Update is called once per frame

    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
