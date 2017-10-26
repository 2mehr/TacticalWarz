using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public CHaracter SelectedChar;
    public CHaracter ranged;
    public PlayerSide Turn = new  PlayerSide();
    public PlayerSide CurrentPlayerSide = new PlayerSide();
    public List<CHaracter> Players = new List<CHaracter>();
    public float TimerChangeTurn;
    public Text Timer;
    float DefultTimer;
    public Text TurnPlayer;
    void Start () {
        DefultTimer = TimerChangeTurn;
        TurnPlayer.text = Turn.ToString();
        Turn = PlayerSide.Player1;
        Players.AddRange ( GameObject.FindObjectsOfType<CHaracter>());
        foreach (var item in Players)
        {
            if (item.Side == CurrentPlayerSide)
            {
                item.transform.gameObject.layer = LayerMask.NameToLayer("Player");

            }
            else
            {
                item.transform.gameObject.layer = LayerMask.NameToLayer("Enemy");

            }

            item.OnDeath += Item_OnDeath;
        }
	}
    private void Update()
    {
        TimerChangeTurn -= Time.deltaTime;
        string s = string.Format("{0:0} Timer", TimerChangeTurn);
        Timer.text =s ;
      
        if (TimerChangeTurn<=0)
        {
            EndTurn();
        }
    }

    private void Item_OnDeath(CHaracter dead)
    {
        Players.Remove(dead);
    }
  
    public void EndTurn()
    {

      
        if (Turn== PlayerSide.Player1)
        {
            Turn = PlayerSide.Player2;
            Timer.GetComponent<Text>().color = Color.blue;
           // SelectedChar.GetComponent<FieldOfView>().viewRadius = 0;
           
        }
        else
        {
            Turn = PlayerSide.Player1;
            Timer.GetComponent<Text>().color = Color.white;
           // SelectedChar.GetComponent<FieldOfView>().viewRadius = 0;
        }
        CurrentPlayerSide = Turn;
        TurnPlayer.text = Turn.ToString();
        SelectedChar.ShootTarget = null;
        TimerChangeTurn = DefultTimer;
    }

    public void Shoot()
    {

        ranged.Attack();
    }
    
   
}
