using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RouteManager : MonoBehaviour {

	public List<Tile> SelectedRoute = new List<Tile> ();
	public CHaracter SelectedChar;
  //  public CHaracter ranged;
    public PlayerSide Turn = new PlayerSide();
    public PlayerSide CurrentPlayerSide = new PlayerSide();
    public List<CHaracter> Players = new List<CHaracter>();
    public float TimerChangeTurn;
    public Text Timer;
    float DefultTimer;
    public Text TurnPlayer;
    Tile CurrentTile;
	List<Tile> AllTiles = new List<Tile> ();

	// Use this for initialization
	void Start () 
	{
		AllTiles.AddRange (GameObject.FindObjectsOfType<Tile> ());


        DefultTimer = TimerChangeTurn;
        TurnPlayer.text = Turn.ToString();
        Turn = PlayerSide.Player1;
        Players.AddRange(GameObject.FindObjectsOfType<CHaracter>());
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
    private void Item_OnDeath(CHaracter dead)
    {
        Players.Remove(dead);
    }

    public void EndTurn()
    {


        if (Turn == PlayerSide.Player1)
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
        SelectedChar = null;
        TimerChangeTurn = DefultTimer;
        foreach (var item in Players)
        {
            item.WalkPoint = 5;
        }
    }
    public void Shoot()
    {

        SelectedChar.Attack();
    }

    bool CorrectSelect;

	// Update is called once per frame
	void Update () 
	{


        TimerChangeTurn -= Time.deltaTime;
        string s = string.Format("{0:0} Timer", TimerChangeTurn);
        Timer.text = s;

        if (TimerChangeTurn <= 0)
        {
            EndTurn();
        }



        if (Input.GetMouseButtonDown (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 50)) 
			{
				if (hit.transform.GetComponent<Tile> () == SelectedChar.CurrentPos) 
				{
                    Debug.DrawLine(SelectedChar.transform.position, hit.point, Color.red);
					CorrectSelect = true;
				}
			}
		}
		if (Input.GetMouseButton (0) && CorrectSelect) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 50)) 
			{
				if (hit.transform.GetComponent<Tile> () && hit.transform.GetComponent<Tile> () != CurrentTile && 
					(CurrentTile == null || hit.transform.GetComponent<Tile>().Neighbors.Contains(CurrentTile)) &&
					(hit.transform.GetComponent<Tile>().CharOn == null || hit.transform.GetComponent<Tile>().CharOn == this.SelectedChar)) 
				{
					if (!hit.transform.GetComponent<Tile> ().IsOn) 
					{
						CurrentTile = hit.transform.GetComponent<Tile> ();
						CurrentTile.IsOn = true;
						SelectedRoute.Add (CurrentTile);
					} 
					else if(SelectedRoute.IndexOf(CurrentTile) - 1 < 0 || hit.transform.GetComponent<Tile>() == SelectedRoute[SelectedRoute.IndexOf(CurrentTile) - 1])
					{
						SelectedRoute.Remove (CurrentTile);
						CurrentTile.IsOn = false;
						CurrentTile = hit.transform.GetComponent<Tile> ();
					}
				}
			}
		} 
		else if (Input.GetMouseButtonUp (0)) 
		{
			foreach (Tile t in SelectedRoute) 
			{
				t.IsOn = false;
				t.transform.GetComponent<Renderer>().material.color = Color.white;
			}	

			SelectedChar.Route.AddRange (SelectedRoute.ToArray ());
				
			SelectedRoute.Clear();

			CurrentTile = null;

			CorrectSelect = false;
		}

		foreach(Tile t in AllTiles)
		{
			if (SelectedRoute.Contains (t)) 
			{
				if(SelectedRoute.IndexOf(t) >= SelectedChar.WalkPoint)
				{
					t.GetComponent<Renderer> ().material.color = Color.red;
				}
				else
					t.GetComponent<Renderer> ().material.color = Color.yellow;
			} 
			else
				t.GetComponent<Renderer> ().material.color = Color.white;
		}
	}
}
