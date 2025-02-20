using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_AlchemyBellows : MonoBehaviour
{
    public GameObject fireSprite;
    public GameObject beakerSprite;

    SpriteRenderer beakerSpriteRenderer;

    public float fireShrinkRate = .5f;
    public float fireGrowRate = 0.1f;

    public float colorShiftRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        beakerSpriteRenderer = beakerSprite.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            if (Input.GetKeyDown("space"))
            {
                fireSprite.transform.localScale += new Vector3(fireGrowRate, fireGrowRate);
            }
            fireSprite.transform.localScale = new Vector3(
                Mathf.Clamp(fireSprite.transform.localScale.x - (fireShrinkRate * Time.deltaTime), 0f, 1f),
                Mathf.Clamp(fireSprite.transform.localScale.y - (fireShrinkRate * Time.deltaTime), 0f, 1f)
                );

            if (beakerSpriteRenderer)
            {
                // How close the scale of the fire sprite is to 0.5
                float t = -Mathf.Abs(2f * fireSprite.transform.localScale.x - 1f) + 1f;
                float H, S, V;

                Color.RGBToHSV(beakerSpriteRenderer.color, out H, out S, out V);
                H = Mathf.Clamp(H + colorShiftRate * t * Time.deltaTime, 0f, 0.3f);

                beakerSpriteRenderer.color = Color.HSVToRGB(H, S, V);
            }
        }
    }
}
