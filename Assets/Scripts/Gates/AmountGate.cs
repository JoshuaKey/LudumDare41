using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmountGate : MonoBehaviour {

    [SerializeField] private int enemiesKilled = 5;
    [SerializeField] private bool hasType = false;
    [SerializeField] private Enemy.SlimeType type;

    [SerializeField] private GameObject[] associations;

    // A gate is an obstacle that will open up after some condition has been met
    // It must register or test that condition until it comes true. 
    // when it comes true, it should disable itself.

    private void Update() {
        if (!hasType) {
            if (Data.enemiesKilled >= enemiesKilled) {
                Remove();
            }
        } else {
            int killed = 0;
            switch (type) {
                case Enemy.SlimeType.Green:
                    killed = Data.greenKilled;
                    break;
                case Enemy.SlimeType.Red:
                    killed = Data.redKilled;
                    break;
                case Enemy.SlimeType.Blue:
                    killed = Data.blueKilled;
                    break;
                case Enemy.SlimeType.Purple:
                    killed = Data.purpleKilled;
                    break;
                case Enemy.SlimeType.Black:
                    killed = Data.blackKilled;
                    break;
                case Enemy.SlimeType.White:
                    killed = Data.whiteKilled;
                    break;
                case Enemy.SlimeType.Gold:
                    killed = Data.goldKilled;
                    break;
            }
            if (killed >= enemiesKilled) {
                Remove();
            }
        }

    }

    public void Remove() {
        // Annimations and crap...
        this.gameObject.SetActive(false);
        for(int i = 0; i < associations.Length; i++) {
            associations[i].SetActive(false);
        }
    }
}
