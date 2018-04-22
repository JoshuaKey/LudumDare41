using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [Header("Pool")]
    [SerializeField] private Enemy prefab;
    [SerializeField] private int poolMax;

    [Header("Spawn")]
    [SerializeField] Enemy.SlimeType spawnType;
    [SerializeField] float avgAmo;
    [SerializeField] float avgTime;
    [SerializeField] int level;

    private Enemy[] enemyPool;
    private int poolIter = 0;

    private float nextSpawnTime;

    private void Start() {
        enemyPool = new Enemy[poolMax];

        for (int i = 0; i < poolMax; i++) {
            enemyPool[i] = Instantiate(prefab, this.transform.position, Quaternion.identity);
            enemyPool[i].gameObject.SetActive(false);
            enemyPool[i].transform.parent = this.transform;

            Health health = enemyPool[i].health;
            health.OnDeath += OnDeath;
        }
    }

    public virtual bool CheckForSpawn() {
        return Time.time > nextSpawnTime;
    }

    private void OnDeath() {


        for (int i = 0; i < poolIter; i++) {
            if (enemyPool[i].health.IsDead()) {

                //print(enemyPool[i].name + " is dead");
                Stats(enemyPool[i].m_slimeType);
                enemyPool[i].gameObject.SetActive(false);

                // Swap
                Enemy temp = enemyPool[poolIter - 1];
                enemyPool[poolIter - 1] = enemyPool[i];
                enemyPool[i] = temp;
                break;
            }
        }
        // time = 10 sec
        // amo = 2
        // t = 1
        // t2 = .75
        float t = (poolIter / avgAmo) / 2f + .5f; // Range of .5 -> 1
        float t2 = ((poolIter-1) / avgAmo) / 2f + .5f; // Range of .5 -> 1
        //float t = Interpolation.QuadraticIn(poolIter / avgAmo);
        //float t2 = Interpolation.QuadraticIn((poolIter-1) / avgAmo);
        float removeTime = avgTime * (t - t2);
        nextSpawnTime -= removeTime;

        poolIter--;
    }

    public void SpawnEnemy() {
        // Add variance in spawning position
        if (poolIter >= poolMax) { return; }
        //print(enemyPool[poolIter] + " " + poolIter);
        enemyPool[poolIter].gameObject.SetActive(true);

        enemyPool[poolIter].Initialize(spawnType, level);

        enemyPool[poolIter].transform.position = this.transform.position;

        poolIter++;
        float t = (poolIter / avgAmo) / 2f + .5f; // Range of .5 -> 1
        //t = Interpolation.QuadraticOut(t);
        nextSpawnTime = Time.time + avgTime * t;

        // A Spawn point Avg time refers to the time it takees to spawn after the avg amount has been reached.
        // Before the avg amount. The spawn time should never be less than 50%
        // If 0, -> avgTime / 2
        // if 1, -> avgTime
        // if 2 -> avgTime
    }

    public void KillAll() {
        for (int i = 0; i < poolIter; i++) {
            enemyPool[i].gameObject.SetActive(false); 
        }
        poolIter = 0;
    }

    public bool IsEngaged() {
        for (int i = 0; i < poolIter; i++) {
            if (enemyPool[i].isChasing) {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .2f);
    }

    static private void Stats(Enemy.SlimeType type) {
        Data.enemiesKilled++;
        switch (type) {
            case Enemy.SlimeType.Green:
                Data.greenKilled++;
                break;
            case Enemy.SlimeType.Red:
                Data.redKilled++;
                break;
            case Enemy.SlimeType.Blue:
                Data.blueKilled++;
                break;
            case Enemy.SlimeType.Purple:
                Data.purpleKilled++;
                break;
            case Enemy.SlimeType.Black:
                Data.blackKilled++;
                break;
            case Enemy.SlimeType.White:
                Data.whiteKilled++;
                break;
            case Enemy.SlimeType.Gold:
                Data.goldKilled++;
                break;
        }
    }
}
