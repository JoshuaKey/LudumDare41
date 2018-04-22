using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGate : MonoBehaviour {
    [SerializeField] private GameObject[] associations;

    // A gate is an obstacle that will open up after some condition has been met
    // It must register or test that condition until it comes true. 
    // when it comes true, it should disable itself.

    private void Update() {
        if(Data.hasKey) {
            Remove();
        }
    }

    public void Remove() {
        // Annimations and crap...
        for (int i = 0; i < associations.Length; i++) {
            associations[i].SetActive(false);
        }
        AudioManager.Instance.PlaySound(AudioManager.Instance.openSound);
        this.gameObject.SetActive(false);
        Destroy(this);
    }
}
