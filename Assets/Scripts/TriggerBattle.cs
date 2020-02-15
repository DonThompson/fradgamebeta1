using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerBattle : MonoBehaviour
{

    public string sceneToLoad;
    private GameObject thisEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            GameController.instance.BeginEnemyBattle(thisEnemy);

            //Destroy(GameObject.Find("Enemy"));
            //SceneManager.LoadScene(sceneToLoad);
            //LoadBattleScreen();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        thisEnemy = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
