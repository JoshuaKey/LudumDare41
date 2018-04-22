using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryUI : MonoBehaviour {

    public Vector3 startPos;

    public Vector3 endPos;

    private void OnEnable() {
        StartCoroutine(Flow());
    }

    public IEnumerator Flow() {
        this.transform.localPosition = startPos;

        float start = Time.time;
        float length = 5.0f;
        while (Time.time < start + length) {
            float diff = Time.time - start;
            float t = diff / length;

            this.transform.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        this.transform.localPosition = endPos;
    }
}
