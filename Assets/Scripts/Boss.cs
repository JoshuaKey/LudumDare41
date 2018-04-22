using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    public int level;
    public SlimeType[] types;
    public VictoryUI victoryUI;
    public GameObject gate;

    private int percentIndex = 0;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
        health.OnDeath += OnDeath;
	}

    private void Start() {
        this.Initialize(types[0], level);
    }

    public void OnDeath() {
        percentIndex++;
        if(percentIndex < types.Length) {
            this.Initialize(types[percentIndex], level);
        } else {
            // Victory
            this.gameObject.SetActive(false);
            gate.SetActive(false);
            victoryUI.gameObject.SetActive(true);
        }
        
    }

    public void Reset() {
        this.Initialize(types[0], level);
        percentIndex = 0;
    }


}
