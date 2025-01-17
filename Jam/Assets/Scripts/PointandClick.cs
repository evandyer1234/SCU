using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PointandClick : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] genericClickSounds = new AudioClip[8];

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                //Transform objectHit = hit.transform;
                Debug.Log("Hit " + hit.collider.gameObject.name);
                ClickEvent ce = hit.collider.gameObject.GetComponent<ClickEvent>();
                if (ce != null)
                {
                    ce.Clicked();
                }
            }
            else
            {
                _audioSource.PlayOneShot(genericClickSounds[Random.Range(0, genericClickSounds.Length)]);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {

        }
    }
}
