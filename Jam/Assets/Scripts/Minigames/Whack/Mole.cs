using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Mole : whackbase
{
    //plays when hit
    public override void Hit()
    {
        mw.Smack();
        base.Hit();
    }
    //plays when target missed
    public override void Miss()
    {
        base.Miss();
        mw.Miss();
    }
}
