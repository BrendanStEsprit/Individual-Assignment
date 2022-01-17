using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxController : MonoBehaviour
{
   Animator Animation;
    public AudioClip musicClipOne;
    public AudioSource musicSource;
    private PlayerMovement PlayerController;
    public int score { get { return Score; } }
     int Score;
    public bool activeHitbox;
    CapsuleCollider2D m_Collider;

    //Start is called before the first frame update
    void Start()
    {
        Animation = GetComponent<Animator>();
        m_Collider = GetComponent<CapsuleCollider2D>();
      
    }

    // Update is called once per frame
    void Update()
    {
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        PlayerController = PlayerObject.GetComponent<PlayerMovement>();

        if (PlayerController != null)
        {
            if (PlayerController.GroundInt == 1)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    Animation.SetTrigger("Attacking");





                }
            }

        }  
        if(activeHitbox)
        { m_Collider.enabled = true; }
        if(!activeHitbox)
        {
            m_Collider.enabled=false;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Target")
        {
            
            
                Score += 1;
                Destroy(other.gameObject);
            musicSource.clip = musicClipOne;
            musicSource.Play();


        }
    }
}
