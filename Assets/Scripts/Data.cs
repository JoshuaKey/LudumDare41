using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data {

    static public readonly int PLAYER_LAYER = 8;
    static public readonly int ENEMY_LAYER = 9;
    static public readonly int LADDER_LAYER = 10;
    static public readonly int GROUND_LAYER = 1;
    static public readonly int SPAWN_LAYER = 11;

    static public int enemiesKilled = 0;
    static public int greenKilled = 0;
    static public int blueKilled = 0;
    static public int redKilled = 0;
    static public int purpleKilled = 0;
    static public int whiteKilled = 0;
    static public int blackKilled = 0;
    static public int goldKilled = 0;

    static public int playerLevel = 0;
    static public int playerDeath = 0;

    static public bool hasKey = false;
}
