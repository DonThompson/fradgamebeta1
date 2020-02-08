using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerBattle : MonoBehaviour
{
    public string sceneToLoad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            Destroy(GameObject.Find("Enemy"));
            SceneManager.LoadScene(sceneToLoad);
            //LoadBattleScreen();
        }
    }

    private void LoadBattleScreen()
    {
        GameController.control.inBattle = true;
        Destroy(GameObject.Find("Enemy"));
        GameController.control.playerDead = true;
        //Application.LoadLevel(sceneToLoad);       //tutorial but obsolete
        SceneManager.LoadScene(sceneToLoad);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
