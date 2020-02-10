using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState {
    START, PLAYERTURN, ENEMYTURN, WON, LOST
}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogText;

    Unit playerUnit;
    Unit enemyUnit;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogText.text = "A wild " + enemyUnit.unitName + " approaches!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        dialogText.text = "Choose an action:";
    }


    IEnumerator PlayerAttack()
    {
        //Do damage
        int damageDone = UnityEngine.Random.Range(1, playerUnit.damage);
        bool isDead = enemyUnit.TakeDamage(damageDone);

        //Update UI
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogText.text = string.Format("Attack landed for {0} damage!", damageDone);

        //Pause UI
        yield return new WaitForSeconds(2f);

        //Handle next state
        if(isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
    }

    IEnumerator EnemyTurn()
    {
        dialogText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);


        //Do damage
        int damageDone = UnityEngine.Random.Range(1, enemyUnit.damage);
        bool isDead = playerUnit.TakeDamage(damageDone);

        //Update UI
        playerHUD.SetHP(playerUnit.currentHP);
        dialogText.text = string.Format("You took {0} damage!", damageDone);

        //Pause UI
        yield return new WaitForSeconds(1f);

        //Handle next state
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

     void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST)
        {
            dialogText.text = "DEFEATED!";
        }

        StartCoroutine(TransitionOut());
    }

    IEnumerator TransitionOut()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("SampleScene");
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerHeal()
    {
        //Heal self
        int healHP = UnityEngine.Random.Range(1, playerUnit.magicPower);
        playerUnit.HealDamage(healHP);

        //Update UI
        playerHUD.SetHP(playerUnit.currentHP);
        dialogText.text = string.Format("You healed {0} damage!", healHP);

        //Pause UI
        yield return new WaitForSeconds(2f);

        //Handle next state
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

        yield return new WaitForSeconds(2f);
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }
}
