using Helpers;
using UnityEngine;

public class leech : MonoBehaviour
{
    [SerializeField] MG_Drain _Drain;
   
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    //when the leeche enters a trigger, it adds a leech to the leech counter in the main script
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(NamingConstants.TAG_PLAYER))
        {
            _Drain.leeches++;
        }
    }
    //remeoves a leech when exiting the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(NamingConstants.TAG_PLAYER))
        {
            _Drain.leeches--;
        }

    }

    public void ChangeSprite(Sprite sprite)
    {
        if (sr != null)
        {
            sr.sprite = sprite;
        }
    }


}
