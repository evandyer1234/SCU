using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubjectManager : MonoBehaviour
{
    public static SubjectManager instance;
    
    [SerializeField] private List<Subject> subjects;
    [SerializeField] private Subject currentSubject;

    [SerializeField] private SpriteRenderer xRay, body, outline;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateSprites();
    }

    private void UpdateSprites()
    {
        xRay.sprite = currentSubject.imageSkeleton;
        body.sprite = currentSubject.imageMain;
        outline.sprite = currentSubject.imageOutLine;
    }
    
    
}
