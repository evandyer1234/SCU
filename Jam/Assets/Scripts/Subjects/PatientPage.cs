using UnityEngine;

namespace Subjects
{
    public class PatientPage : MonoBehaviour
    {
        [SerializeField] private GameObject clothLayer;
        [SerializeField] private GameObject underClothLayer;
        [SerializeField] private GameObject organLayer;
        [SerializeField] private GameObject skeletonLayer;


        public void SetSpriteBySubject(Subject subject)
        {
            clothLayer.GetComponent<SpriteRenderer>().sprite = subject.imageOutfit;
            underClothLayer.GetComponent<SpriteRenderer>().sprite = subject.imageUnder;
            organLayer.GetComponent<SpriteRenderer>().sprite = subject.imageOrgans;
            skeletonLayer.GetComponent<SpriteRenderer>().sprite = subject.imageSkeleton;
        }
    }
}