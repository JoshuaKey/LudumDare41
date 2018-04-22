using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    bool rotatingZPos;
    float maxZ = 20;

    private void Start() {
        rotatingZPos = Random.Range(0, 2) == 1;

    }

    // Update is called once per frame
    void Update () {
        //float timeRange = Time.time - timeStart;
        //float t = timeRange / timeLength;

        //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, currRot, 10 * Time.deltaTime);
        //if(this.transform.rotation == currRot) {
        //    SetRotation();
        //}

        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, currRot, t);
        //if(t >= 1) {
        //    SetRotation();
        //}
        //// y = 50
        ////x = 30
        //// z = 20
        var angles = this.transform.eulerAngles;

        angles.z += 10 * Time.deltaTime * (rotatingZPos ? 1 : -1);
        if(angles.z > maxZ && angles.z < 360 - maxZ) {

            if (rotatingZPos) {
                angles.z = maxZ - .1f;
            } else {
                angles.z = 360 - maxZ + .1f;
            }
            rotatingZPos = !rotatingZPos;
        }
        //if (rotatingZPos && angles.z > maxZ  && angles.x < 360 - maxZ) {
        //    rotatingZPos = false;
        //    angles.z = maxZ;
        //}
        //if(!rotatingZPos && angles.z > maxZ && angles.x < 360 - maxZ) {
        //    rotatingZPos = true;
        //    angles.z = 360 - maxZ;
        //}

        //angles.y += Random.Range(0, maxAmo) * Time.deltaTime * (rotatingYPos ? 1 : -1);
        //if (angles.y > maxY && angles.y < 360 - maxY) {
        //    rotatingYPos = !rotatingYPos;
        //}

        //angles.z -= Random.Range(0, maxAmo) * Time.deltaTime * (rotatingZPos ? 1 : -1);
        //if (angles.z > maxZ && angles.z < 360 - maxZ) {
        //    rotatingZPos = !rotatingZPos;
        //}

        //Quaternion.Slerp(this.transform.rotation, )

        ////if (rotatingZPos) {
        ////    angles.z += Random.Range(0, maxAmo) * Time.deltaTime;
        ////    if (angles.z > maxZ) {
        ////        rotatingZPos = false;
        ////    }
        ////} else {
        ////    angles.z -= Random.Range(0, maxAmo) * Time.deltaTime;
        ////    if (angles.z < 360 - maxZ) {
        ////        rotatingZPos = true;
        ////    }
        ////}

        this.transform.eulerAngles = angles;
    }
}
