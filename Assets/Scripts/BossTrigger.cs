using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    public GameObject gate;
    public PlayerController player;
    public VictoryUI ui;

    public Boss bossPrefab;

    private void Awake() {
        bossPrefab = Instantiate(bossPrefab, this.transform.position, Quaternion.identity);
        bossPrefab.gameObject.SetActive(false);
        bossPrefab.transform.parent = this.transform.parent;
        bossPrefab.level = 20;
        bossPrefab.victoryUI = ui;
        bossPrefab.gate = gate;
    }

    // Use this for initialization
    void Start () {
        
	}

	private void OnDeath() {
        gate.SetActive(false);
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        gate.SetActive(true);
        this.gameObject.SetActive(false);
        player.health.OnDeath += OnDeath;

        bossPrefab.gameObject.SetActive(true);
        bossPrefab.transform.position = this.transform.position;
        bossPrefab.Reset();
    }
}   
