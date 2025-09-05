using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Effect : Effect
{
    public override void Interract(Effect effectToInterractWith)
    {
        effectToInterractWith.OnWood(matParams);

        //Wood doesnt is not liquid so no DepleteResource
        // InterractOtherConditions(effectToInterractWith, matParams);
        // DepleteResource(matParams.fuelPerSecond, matParams.currentAcidInside);
    }

    public override void OnFire(MatParams OtherEffectMatParams)
    {
        MatParams OEMP = OtherEffectMatParams;
        if (OEMP.currentTemp >= matParams.burningPoint_C && matParams.currentSize < (OEMP.currentSize * 10)) //if fire is hot enough
        {
            if (matParams.currentWaterInside >= 0)
            {
                float waterToEvap = OEMP.currentSize * OEMP.currentTemp;//todo WRONG
                DepleteResource(waterToEvap, matParams.currentWaterInside);
                matParams.currentWaterInside -= 1;
            }
        }


        matParams.currentHealth -= OtherEffectMatParams.curResourceTemperature;
    }
    public override void CalculateInnerState()
    {
        if (matParams.currentHealth <= 0) Debug.Log("Dead");
    }
}
