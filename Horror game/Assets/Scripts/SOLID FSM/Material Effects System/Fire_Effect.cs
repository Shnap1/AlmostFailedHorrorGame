using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Effect : Effect
{
    public override void Interract(Effect effectToInterractWith)
    {
        //example with OnAcid()🧪 if THIS effect is an acid
        effectToInterractWith.OnFire(matParams);
        InterractOtherConditions(effectToInterractWith, matParams);
        DepleteResource(matParams.fuelPerSecond, matParams.currentAcidInside);
    }
}
