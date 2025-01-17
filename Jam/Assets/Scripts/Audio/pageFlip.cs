using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class pageFlip : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _singlePageFlipSound;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySinglePageFlip()
    {
        _audioSource.PlayOneShot(_singlePageFlipSound);
    }
}
