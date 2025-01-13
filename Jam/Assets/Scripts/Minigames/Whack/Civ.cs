using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civ : whackbase
{
    //plays miss function to pubish the player for hitting it 
    public override void Hit()
    {
        base.Miss();
        mw.Miss();
    }

    public override void Miss()
    {
        base.Miss();
    }
}
