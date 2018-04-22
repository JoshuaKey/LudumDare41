using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

    public GameObject gate;
    public PlayerController player;

    public Boss bossPrefab;

    private void Awake() {
        bossPrefab = Instantiate(bossPrefab, this.transform.position, Quaternion.identity);
        bossPrefab.gameObject.SetActive(false);
        bossPrefab.transform.parent = this.transform;
        bossPrefab.level = 20;
    }

    // Use this for initialization
    void Start () {
        player.health.OnDeath += OnDeath;
	}

	private void OnDeath() {
        gate.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        gate.SetActive(true);

        bossPrefab.gameObject.SetActive(true);
        bossPrefab.transform.position = this.transform.position;
        bossPrefab.Reset();
    }
}
