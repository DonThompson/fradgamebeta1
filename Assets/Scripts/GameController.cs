using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    

    public bool playerDead;
    public bool inBattle;

    public GameObject enemyPrefab;

    
    private float nextEnemySpawnTime = 1;
    public int enemySpawnRate;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    internal void BeginEnemyBattle(GameObject enemy)
    {
        //Destroy the enemy game object we collided with
        Destroy(enemy);

        //Remove him from our enemies array
        WorldEnemyManager.instance.RemoveEnemy(enemy);

        Debug.Log($"Beginning battle, removed enemy from the world.  {WorldEnemyManager.instance.GetEnemyCount()} remain...");

        //Save everything in the current world
        WorldEnemyManager.instance.PersistEnemies();

        inBattle = true;
        SceneManager.LoadScene("TurnBasedBattle");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "SampleScene" && playerDead)
        {
            inBattle = false;

            Destroy(GameObject.Find("Following Enemy"));
        }
        else if(scene.name == "SampleScene" && inBattle)
        {
            //Reload after battle
            ReturnFromBattle();
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TrySpawnEnemy();
    }

    private void TrySpawnEnemy()
    {
        if(!inBattle && Time.time >= nextEnemySpawnTime)
        {
            //Update the time we'll spawn the next one
            nextEnemySpawnTime = Time.time + enemySpawnRate;

            //Randomize position of the enemy
            float pos = UnityEngine.Random.Range(-3, -3);
            Vector3 position = new Vector3(pos, pos, 0);

            //Spawn it!
            WorldEnemyManager.instance.SpawnEnemy(position);
        }
    }

    internal void ReturnFromBattle()
    {
        Debug.Log("Returned from battle!  Recreating enemies");

        //Respawn all enemies that we had persisted
        WorldEnemyManager.instance.RespawnEnemies();

        //Reset the time we'll next spawn.  Otherwise we'll immediately spam a bunch to catch up.
        nextEnemySpawnTime = Time.time + enemySpawnRate;
        inBattle = false;
    }
}
