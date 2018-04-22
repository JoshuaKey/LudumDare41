using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField] private float currHP;
    [SerializeField] private float maxHP;

    public float MaxHP { get { return maxHP; } }
    public float CurrHP { get { return currHP; } }

    public delegate void HealthChange();
    public HealthChange OnHeathChanged;
    public HealthChange OnDeath;

    private LevelSystem levelSys;
    public LevelSystem LevelSys { get { return levelSys; } }

    private void Start() {
        levelSys = GetComponent<LevelSystem>();
        Initialize(maxHP);
    }

    public void Initialize(float max) {
        maxHP = max;
        currHP = maxHP;
        CallDelegate(OnHeathChanged);
    }

    public void Heal(float health) {
        if (currHP != maxHP) {
            currHP += health;
            if (currHP > maxHP) {
                currHP = maxHP;
            }

            CallDelegate(OnHeathChanged);
        }
    }

    public void TakeDamage(float health) {
        currHP -= health;

        CallDelegate(OnHeathChanged);

        // On Death
        if (IsDead()) {
            CallDelegate(OnDeath);
        }
    }

    public bool IsDead() {
        return currHP <= 0;
    }

    public void CallDelegate(HealthChange del) {
        if(del != null) {
            del();
        }
    }
}
