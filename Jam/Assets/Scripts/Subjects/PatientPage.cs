using UnityEngine;

namespace Subjects
{
    public class PatientPage : MonoBehaviour
    {
        [SerializeField] public GameObject clothLayer;
        [SerializeField] public GameObject underClothLayer;
        [SerializeField] public GameObject organLayer;
        [SerializeField] public GameObject skeletonLayer;


        public void SetSpriteBySubject(Subject subject)
        {
            clothLayer.GetComponent<SpriteRenderer>().sprite = subject.imageOutfit;
            underClothLayer.GetComponent<SpriteRenderer>().sprite = subject.imageUnder;
            organLayer.GetComponent<SpriteRenderer>().sprite = subject.imageOrgans;
            skeletonLayer.GetComponent<SpriteRenderer>().sprite = subject.imageSkeleton;
        }
    }
}