using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldEnemyManager : MonoBehaviour
{
    public static WorldEnemyManager instance;
    public GameObject enemyPrefab;

    //Tracked enemies
    private List<GameObject> worldEnemies = new List<GameObject>();

    //Persistance
    private List<Vector3> savedEnemyPositions = new List<Vector3>();

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SpawnEnemy(Vector3 position)
    {
        //Weird.  We can't create a transform, but we can create an empty object and use its transform
        GameObject emptyGO = new GameObject();
        emptyGO.transform.position = position;

        //Create a new gameobject
        GameObject go = Instantiate(enemyPrefab, emptyGO.transform);
        if(go == null)
        {
            Debug.Log("Instantiate failed?");
        }
        worldEnemies.Add(go);

        Debug.Log($"created enemy at ({position.x}, {position.y})!");
        Debug.Log($"Increased enemies by one.  Now {GetEnemyCount()} remaining.");
    }

    public void RemoveEnemy(GameObject enemy)
    {
        worldEnemies.Remove(enemy);
    }

    public int GetEnemyCount()
    {
        return worldEnemies.Count;
    }

    internal void PersistEnemies()
    {
        foreach(var enemy in worldEnemies)
        {
            Vector3 v = enemy.transform.position;
            savedEnemyPositions.Add(v);
        }
        worldEnemies.Clear();
    }

    internal void RespawnEnemies()
    {
        Debug.Log($"Recreating {savedEnemyPositions.Count} positions.");

        foreach(var pos in savedEnemyPositions)
        {
            SpawnEnemy(pos);
        }
        savedEnemyPositions.Clear();
    }
}
