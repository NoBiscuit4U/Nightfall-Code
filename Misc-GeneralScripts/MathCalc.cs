using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class MathCalc
{
    //gets percent of left float
    public  static float ToPercent(float flo, float flo2)
    {
        float percent = flo2/100;
        
        float percentFromFloat = flo*percent;

        percentFromFloat = Mathf.RoundToInt(percentFromFloat);

        Debug.Log(flo2 + "% " + "of " + flo + " is " + percentFromFloat);
        return percentFromFloat;
    }

}
