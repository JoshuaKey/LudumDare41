using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public GameObject target;
    public float speedMod = 3f;

	
	// Update is called once per frame
	//void LateUpdate () {
 //       Vector3 pos = Vector3.Lerp(this.transform.position, target.transform.position, Time.deltaTime * speedMod);
 //       pos.z = this.transform.position.z;
 //       this.transform.position = pos;
 //   }
    private void FixedUpdate() {
        Vector3 pos = Vector3.Lerp(this.transform.position, target.transform.position, Time.deltaTime * speedMod);
        pos.z = this.transform.position.z;
        this.transform.position = pos;
    }
}
