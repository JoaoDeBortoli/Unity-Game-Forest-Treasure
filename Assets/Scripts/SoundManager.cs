using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sons;
    private AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();

        if (sons == null)
        {
            sons = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (sons != null && sons != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

}
