using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MG_Drain : MiniGameBase
{
    //starting value of the blood toxicity 
    float bloodtoxicity = 0;

    
    [SerializeField, Tooltip("Rate at which the leeches drain blood")]
    float drainrate = 1f;

    [SerializeField, Tooltip("value the player must get below for the game to clear")]
    float safebloodlevel;

    [SerializeField, Tooltip("Text to display the current toxicity level")]
    TextMeshPro displaylevel;

    [SerializeField, Tooltip("amount of time the display takes to load")]
    float loadtime = .5f;


    [SerializeField, Tooltip("Collider for the button so the player cannot spam for a result")] Collider buttoncollider;

    [SerializeField, Tooltip("Object to be turned on to let the player know whn they have reached a safe value")] 
    GameObject SafeInd;

    [SerializeField, Tooltip("Max value the blood can start at")] 
    float maxstartvalue;
    [SerializeField, Tooltip("Min value the blood can start at")]
    float minstartvalue;

    [Tooltip("number of leeches currently on the patient")]
    public int leeches = 0;

    
    [SerializeField, Tooltip(" minimum value of the syringe slider (can help make the syringe look good")]
    float minsyringe;

    [SerializeField, Tooltip("speed the syringe goes up")] float pumpspeed;
    [SerializeField] Slider syringe;
    [SerializeField, Tooltip("sprite renderer for the toxic spirt thats overlayed on the blood sprite")]
    SpriteRenderer toxicsp;

    bool syringego = false;
    public override void Start()
    {
        base.Start();
        //set start value for blood
        bloodtoxicity = Random.Range(minstartvalue, maxstartvalue);
        SafeInd.SetActive(false);

    }

    public void FixedUpdate()
    {
        //drains blood based on the drain rate and the number of leeches
        bloodtoxicity -= Time.fixedDeltaTime * drainrate * leeches;

        //if the counter gets below zero, the player "loses" and has a significant time penalty, but the game is cleared
        if (bloodtoxicity <= 0)
        {
            miniGameManager.subtractTime(penalty);
            OnSuccess();
        }

        if (syringego)
        {
            syringe.value += pumpspeed * Time.fixedDeltaTime;
        }
    }

    //starts the loading courutine fir the display, unityevents cant trigger IEnumerators, so thats why this function is so barebones
    public void StartLoad()
    {
        StartCoroutine(LoadDisplay());
    }

    //waits a set amount of time before loading the information for the player
    public IEnumerator LoadDisplay()
    {
        buttoncollider.enabled = false;

        displaylevel.text = "Loading";

        yield return new WaitForSeconds(loadtime);

        updatetox();

    }
    //loads the info for the player and checks for a game success
    //outdated, now acts as finish button
    public void updatetox()
    {
        buttoncollider.enabled = true;
        displaylevel.text = bloodtoxicity.ToString("F0");

        if (bloodtoxicity < safebloodlevel )
        {
            SafeInd.SetActive(true);
        }

        if (leeches == 0 && bloodtoxicity < safebloodlevel)
        {
            OnSuccess();
        }
    }

    //updates syringe with current blood
    public void PullSyringe()
    {
        
        syringego = true;
        syringe.value = minsyringe;
        if (bloodtoxicity < safebloodlevel )
        {
            toxicsp.gameObject.SetActive(false);

            if (leeches == 0 && bloodtoxicity < safebloodlevel)
            {
                OnSuccess();
            }
        }
    }

   
    
}
