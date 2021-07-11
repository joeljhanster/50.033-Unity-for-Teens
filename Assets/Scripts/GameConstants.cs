using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // for Scoring system
    int currentScore;
    int currentPlayerHealth;

    // for Reset values
    Vector3 gombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0);    // hardcoded location
    
    // .. other reset values

    // Mario basic starting values
    public int playerMaxSpeed = 10;
    public int playerMaxJumpSpeed = 100;
    public int playerDefaultSpeed = 250;

    // for Consume.cs
    public int consumeTimeStep = 10;
    public int consumeLargestScale = 4;

    // for Debris.cs
    public int breakTimeStep = 30;
    public int breakDebrisForce = 10;
    public int breakDebrisTorque = 10;

    // for SpawnDebris.cs
    public int spawnNumberOfDebris = 10;

    // for Coin.cs
    public int rotatorRotateSpeed = 350;
    public int coinTimeStep = 15;

    // for testing
    public int testValue;

    // for EnemyController.cs
    public float maxOffset = 5.0f;
    public float enemyPatroltime = 2.0f;
    public float groundSurface = -1.0f;

    // for SpawnManager.cs
    public int numGombaEnemy = 2;
    public int numGreenEnemy = 2;
    public Vector3 enemySpawnPointStart = new Vector3(0.0f, -0.5f, 0.0f);
}
