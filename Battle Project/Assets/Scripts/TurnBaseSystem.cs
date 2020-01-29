using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST};
public class TurnBaseSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleState state;

    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        Debug.Log("Battaglia iniziata!");
        // Delay inzio turno
        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        Debug.Log("Attacco ...");
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        yield return new WaitForSeconds(2f);
        if(isDead)
        {
            // Enemy death
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            // Enemy Turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Il nemico ti attacca");
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            // Player Turn
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    private void EndBattle()
    {
        if(state == BattleState.WON)
        {
            Debug.Log("Hai vinto la battaglia");
        }
        else
        {
            Debug.Log("Hai perso la battaglia!");
        }
    }

    private void PlayerTurn()
    {
        Debug.Log("Turno del giocatore");
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
    
}
