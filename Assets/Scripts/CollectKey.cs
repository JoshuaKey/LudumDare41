using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        Data.hasKey = true;
        Destroy(this.gameObject);
    }
}
