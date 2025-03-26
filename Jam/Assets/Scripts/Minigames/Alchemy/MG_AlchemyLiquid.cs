using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_AlchemyLiquid : MonoBehaviour
{
    public float initialLevel = 2.5f;
    public float drainRate = 0.5f;
    public float stopDelay = 0.5f;

    // Range from 0 to 1, how far up the goal is
    public float tt1Goal = 0.4f;
    private float tt2Goal;
    // How far the liquid level can be from the goal and still count
    public float goalRange = 0.1f;

    public GameObject testtube1;
    public GameObject testtube2;

    private Transform tt1_transform;
    private Transform tt2_transform;

    private Transform tt1_level_transform;
    private Transform tt2_level_transform;

    // Both can be false but both cannot be true
    private bool tt1_pouring = false;
    private bool tt2_pouring = false;

    private bool runTimer = false;
    private float timerValue;

    // Start is called before the first frame update
    void Start()
    {
        tt2Goal = 1.0f - tt1Goal;

        tt1_transform = testtube1.transform;
        tt2_transform = testtube2.transform;

        tt1_level_transform = tt1_transform.GetChild(0).transform;
        tt2_level_transform = tt2_transform.GetChild(0).transform;

        tt1_transform.localPosition = new Vector3(0.5f, 0f, tt1_transform.localPosition.z);
        tt2_transform.localPosition = new Vector3(4.5f, 0f, tt2_transform.localPosition.z);

        tt1_transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        tt2_transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        tt1_level_transform.localScale = new Vector3(tt1_level_transform.localScale.x, initialLevel, tt1_level_transform.localScale.z);
        tt1_level_transform.localPosition = new Vector3(0f, (initialLevel - 3f) / 2f, 0f);

        tt2_level_transform.localScale = new Vector3(tt2_level_transform.localScale.x, 3f - initialLevel, tt2_level_transform.localScale.z);
        tt2_level_transform.localPosition = new Vector3(0f, -initialLevel / 2f, 0f);

        tt1_transform.GetChild(1).transform.localScale = new Vector3(tt1_transform.GetChild(1).transform.localScale.x, goalRange, 1.0f);
        tt1_transform.GetChild(1).transform.localPosition = new Vector3(0.0f, Mathf.Lerp(-1.5f, 1.5f, tt1Goal), -0.34f);
        Debug.Log(Mathf.Lerp(1.0f, -1.5f, tt1Goal));

        tt2_transform.GetChild(1).transform.localScale = new Vector3(tt2_transform.GetChild(1).transform.localScale.x, goalRange, 1.0f);
        tt2_transform.GetChild(1).transform.localPosition = new Vector3(0.0f, Mathf.Lerp(-1.5f, 1.5f, tt2Goal), -0.34f);

        timerValue = stopDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;

        if (runTimer)
        {
            timerValue -= Time.deltaTime;
            Debug.Log(timerValue);
            //runTimer = timerValue >= 0.0f;
            if (timerValue <= 0.0f)
            {
                if (Mathf.Lerp(-1.5f, 1.5f, tt1Goal) - goalRange <= tt1_level_transform.localScale.y - 1.5f && tt1_level_transform.localScale.y - 1.5f <= Mathf.Lerp(-1.5f, 1.5f, tt1Goal) + goalRange)
                {
                    Debug.Log((tt1_level_transform.localScale.y - 1.5f) + ": " + (Mathf.Lerp(-1.5f, 1.5f, tt1Goal)) + ", " + (Mathf.Lerp(-1.5f, 1.5f, tt1Goal)));
                    enabled = false;
                }

                runTimer = false;
                timerValue = stopDelay;
                tt1_pouring = false;
                tt1_transform.localPosition = new Vector3(0.5f, 0f, tt1_transform.localPosition.z);
                tt1_transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                tt2_pouring = false;
                tt2_transform.localPosition = new Vector3(4.5f, 0f, tt2_transform.localPosition.z);
                tt2_transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }

        if (Input.GetMouseButton(0) && !tt2_pouring) tt1_pouring = true;
        if (Input.GetMouseButton(1) && !tt1_pouring) tt2_pouring = true;

        if (tt1_pouring)
        {
            tt1_transform.localPosition = new Vector3(1.65f, 1.86f, tt1_transform.localPosition.z);
            tt1_transform.rotation = Quaternion.Euler(0f, 0f, -56f);

            float prevLevel = tt1_level_transform.localScale.y;

            tt1_level_transform.localScale -= new Vector3(0f, drainRate, 0f) * Time.deltaTime;
            tt1_level_transform.localPosition -= new Vector3(0f, drainRate / 2f, 0f) * Time.deltaTime;

            tt1_level_transform.localScale = new Vector3(tt1_level_transform.localScale.x, Mathf.Clamp(tt1_level_transform.localScale.y, 0f, 3f), tt1_level_transform.localScale.z);
            tt1_level_transform.localPosition = new Vector3(tt1_level_transform.localPosition.x, Mathf.Clamp(tt1_level_transform.localPosition.y, -1.5f, 0f), tt1_level_transform.localPosition.z);

            float diff = prevLevel - tt1_level_transform.localScale.y;

            tt2_level_transform.localScale += new Vector3(0f, diff, 0f);
            tt2_level_transform.localPosition += new Vector3(0f, diff / 2f, 0f);
        } else if (tt2_pouring)
        {
            tt2_transform.localPosition = new Vector3(3.33f, 1.86f, tt1_transform.localPosition.z);
            tt2_transform.rotation = Quaternion.Euler(0f, 0f, 56f);

            float prevLevel = tt2_level_transform.localScale.y;

            tt2_level_transform.localScale -= new Vector3(0f, drainRate, 0f) * Time.deltaTime;
            tt2_level_transform.localPosition -= new Vector3(0f, drainRate / 2f, 0f) * Time.deltaTime;

            tt2_level_transform.localScale = new Vector3(tt2_level_transform.localScale.x, Mathf.Clamp(tt2_level_transform.localScale.y, 0f, 3f), tt2_level_transform.localScale.z);
            tt2_level_transform.localPosition = new Vector3(tt2_level_transform.localPosition.x, Mathf.Clamp(tt2_level_transform.localPosition.y, -1.5f, 0f), tt2_level_transform.localPosition.z);

            float diff = prevLevel - tt2_level_transform.localScale.y;

            tt1_level_transform.localScale += new Vector3(0f, diff, 0f);
            tt1_level_transform.localPosition += new Vector3(0f, diff / 2f, 0f);
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) runTimer = true;
    }
}
