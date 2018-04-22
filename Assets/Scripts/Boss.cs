using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    //public enum SlimeType {
    //    Green,
    //    Red,
    //    Blue,
    //    Purple,
    //    Black,
    //    White,
    //    Gold,
    //}

    //private Health m_health;
    //private LevelSystem m_levelSystem;
    //private SpriteRenderer m_renderer;

    //public SlimeType m_slimeType;

    //public Health health { get { return m_health; } }
    //public LevelSystem levelSystem { get { return m_levelSystem; } }

    public int level;
    public SlimeType[] types;

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
        }
        
    }

    public void Reset() {
        this.Initialize(types[0], level);
        percentIndex = 0;
    }


}
