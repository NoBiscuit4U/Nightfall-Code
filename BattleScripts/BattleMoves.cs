using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleMoves{
    
    public string moveName;
    public int movePower;
    public int moveCost;
    public AttackEffect theEffect;
    public bool doesEffectFire,doesEffectIce,doesEffectPoison,doesEffectDecay,doesEffectHealth,doesEffectArmrCrnch,doesEffectWeaken;

    
}
