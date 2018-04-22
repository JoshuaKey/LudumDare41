using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem: MonoBehaviour {

    [SerializeField] private int level;
    [SerializeField] private float currEXP;
    [SerializeField] private float maxEXP;
    [SerializeField] private float expWorth;

    public int Level { get { return level; } }
    public float CurrExp { get { return currEXP; } }
    public float MaxExp { get { return maxEXP; } }
    public float ExpWorth { get { return expWorth; } }

    public delegate void ExpChange();
    public ExpChange OnExpChanged;
    public ExpChange OnLevel;

    public void Initialize(int l, float exp, float worth) {
        level = l;
        currEXP = 0;
        maxEXP = exp;
        expWorth = worth;
    }

    public void SetMaxXP(float max) {
        maxEXP = max;
    }

    public void GainExp(float xp) {
        currEXP += xp;
        while(currEXP >= maxEXP){
            level++;
            currEXP -= maxEXP;

            CallDelegate(OnLevel);
        }

        CallDelegate(OnExpChanged);
    }

    public void CallDelegate(ExpChange del) {
        if(del != null) {
            del();
        }
    }
}
