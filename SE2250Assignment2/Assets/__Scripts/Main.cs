﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;

    [Header("Set in Inspector")] 
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyDefaultPadding = 1.5f;

    private BoundsCheck bndCheck;

    void Awake()
    {
        S = this;
        //Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();
        //invoke SpawnEnemy() once (on 2 seconds, based on default values)
        Invoke( "SpawnEnemy", 1f/enemySpawnPerSecond );
    }

    public void SpawnEnemy()
    {
        //Pick a random Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ ndx ] );

        //position the enemy above the screen with a random x pos
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius );
        }

        //set the initial pos for the spawned enemy
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        //invoke SpawnEnemy() again
        Invoke( "SpawnEnemy", 1f/enemySpawnPerSecond );
    }
}
