using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField] private PlayerController player;

    public float physicsWaitTime;
    public float rectArea = 10f;

    private float nextPhysicsTime;
    private List<SpawnPoint> spawnPoints;

    private void Start() {
        spawnPoints = new List<SpawnPoint>();

        rectArea /= 2f;
    }

    private void Update() {
        // Find nearby Spawn Points
        // Check for spawns
        // Spawn accordingly...

        if (Time.time > nextPhysicsTime) {

            var posA = player.transform.position;
            posA += Vector3.one * rectArea;
            var posB = player.transform.position;
            posB -= Vector3.one * rectArea;
   
            var hit = Physics2D.OverlapAreaAll(posA, posB, 1 << Data.SPAWN_LAYER);

            List<SpawnPoint> oldSpawns = new List<SpawnPoint>();
            for(int i = 0; i < spawnPoints.Count; i++) {
                oldSpawns.Add(spawnPoints[i]);
            }

            spawnPoints.Clear();
            for (int i = 0; i < hit.Length; i++) {
                SpawnPoint point = hit[i].transform.GetComponent<SpawnPoint>();
                if(point != null) {
                    oldSpawns.Remove(point);
                    spawnPoints.Add(point);
                }
            }

            // These spawns are no longer in the area.
            for(int i= 0; i < oldSpawns.Count; i++) {
                if (!oldSpawns[i].IsEngaged()) {
                    oldSpawns[i].KillAll();
                } else {
                    spawnPoints.Add(oldSpawns[i]);
                }
                
            }

            nextPhysicsTime = physicsWaitTime + Time.time;
        }

        for (int i = 0; i < spawnPoints.Count; i++) {
            SpawnPoint point = spawnPoints[i];
            if (point.CheckForSpawn()) {
                point.SpawnEnemy();
            }
        }
    }

	
}
