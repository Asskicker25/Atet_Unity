using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource deathSound;
    public AudioSource slidingSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayDeathSound()
    {
        deathSound.Play();

    }
    public void PlaySlidingSound()
    {
        slidingSound.Play();

    }

}
