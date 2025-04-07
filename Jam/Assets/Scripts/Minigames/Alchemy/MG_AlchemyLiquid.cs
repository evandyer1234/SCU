using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_AlchemyLiquid : MG_AlchemyBase
{
    public float initialLevel = 2.5f;
    public float drainRate = 0.5f;
    public float stopDelay = 0.5f;

    // Range from 0 to 1, how far up the goal is
    private float tt1Goal;
    private float tt2Goal;
    // How far the liquid level can be from the goal and still count
    public float goalRange = 0.1f;

    public GameObject testtube1;
    public GameObject testtube2;

    private Transform tt1_transform;
    private Transform tt2_transform;

    private Transform tt1_level_transform;
    private Transform tt2_level_transform;

    private Transform tt1_goal_transform;
    private Transform tt2_goal_transform;

    // Both can be false but both cannot be true
    private bool tt1_pouring = false;
    private bool tt2_pouring = false;

    private bool runTimer = false;
    private float timerValue;

    private const float TT1_POS_X = 1.39f;
    private const float TT1_POS_Y = 3f;
    private const float TT2_POS_X = 3.42f;
    private const float TT2_POS_Y = 3f;

    private const float TT1_POURING_POS_X = 2.19f;
    private const float TT1_POURING_POS_Y = 3.76f;
    private const float TT2_POURING_POS_X = 2.66f;
    private const float TT2_POURING_POS_Y = 3.76f;

    private const float TT_POURING_ANGLE = 56f;

    // Set the level for test tube 1, from a value of 0 to 1
    void SetTT1Level(float level)
    {
        tt1_level_transform.localScale = new Vector3(tt1_level_transform.localScale.x, Mathf.Lerp(0f, 3f, level), tt1_level_transform.localScale.z);
        tt1_level_transform.localPosition = new Vector3(tt1_level_transform.localPosition.x, Mathf.Lerp(-1.5f, 0f, level), tt1_level_transform.localPosition.z);
    }

    void SetTT2Level(float level)
    {
        tt2_level_transform.localScale = new Vector3(tt2_level_transform.localScale.x, Mathf.Lerp(0f, 3f, level), tt2_level_transform.localScale.z);
        tt2_level_transform.localPosition = new Vector3(tt2_level_transform.localPosition.x, Mathf.Lerp(-1.5f, 0f, level), tt2_level_transform.localPosition.z);
    }

    void SetTT1Goal(float goal, float range)
    {
        tt1_goal_transform.localScale = new Vector3(tt1_goal_transform.localScale.x, range, 1.0f);
        tt1_goal_transform.localPosition = new Vector3(tt1_goal_transform.localPosition.x, Mathf.Lerp(-1.5f, 1.5f, goal), tt1_goal_transform.localPosition.z);
    }

    void SetTT2Goal(float goal, float range)
    {
        tt2_goal_transform.localScale = new Vector3(tt2_goal_transform.localScale.x, range, 1.0f);
        tt2_goal_transform.localPosition = new Vector3(tt2_goal_transform.localPosition.x, Mathf.Lerp(-1.5f, 1.5f, goal), tt2_goal_transform.localPosition.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Reset Test Tubes (tt1=left test tube, tt2=right test tube) to their starting positions
        // tt1: Pos:    X: 1.39 Y: 3    Z: -1.93522
        // tt2: Pos:    X: 3.42 Y: 3    Z: -1.93522
        tt1Goal = Random.Range(0f, 1f);
        tt2Goal = 1.0f - tt1Goal;

        tt1_transform = testtube1.transform;
        tt2_transform = testtube2.transform;

        tt1_level_transform = tt1_transform.GetChild(0).transform;
        tt2_level_transform = tt2_transform.GetChild(0).transform;

        tt1_goal_transform = tt1_transform.GetChild(1).transform;
        tt2_goal_transform = tt2_transform.GetChild(1).transform;

        tt1_transform.localPosition = new Vector3(TT1_POS_X, TT1_POS_Y, tt1_transform.localPosition.z);
        tt2_transform.localPosition = new Vector3(TT2_POS_X, TT2_POS_Y, tt2_transform.localPosition.z);

        tt1_transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        tt2_transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        SetTT1Level(initialLevel);
        SetTT2Level(1f - initialLevel);

        SetTT1Goal(tt1Goal, goalRange);
        SetTT2Goal(tt2Goal, goalRange);

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
                    Debug.Log("You win!");
                    gameFinished = true;
                    enabled = false;
                }

                runTimer = false;
                timerValue = stopDelay;
                tt1_pouring = false;
                tt1_transform.localPosition = new Vector3(TT1_POS_X, TT1_POS_Y, tt1_transform.localPosition.z);
                tt1_transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                tt2_pouring = false;
                tt2_transform.localPosition = new Vector3(TT2_POS_X, TT2_POS_Y, tt2_transform.localPosition.z);
                tt2_transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            } 
        }

        if (Input.GetMouseButton(0) && !tt2_pouring) tt1_pouring = true;
        if (Input.GetMouseButton(1) && !tt1_pouring) tt2_pouring = true;

        if (tt1_pouring)
        {
            tt1_transform.localPosition = new Vector3(TT1_POURING_POS_X, TT1_POURING_POS_Y, tt1_transform.localPosition.z);
            tt1_transform.rotation = Quaternion.Euler(0f, 0f, -TT_POURING_ANGLE);

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
            tt2_transform.localPosition = new Vector3(TT2_POURING_POS_X, TT2_POURING_POS_Y, tt1_transform.localPosition.z);
            tt2_transform.rotation = Quaternion.Euler(0f, 0f, TT_POURING_ANGLE);

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
