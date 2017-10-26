using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Tile : MonoBehaviour {

	public List<Tile> Neighbors = new List<Tile> ();

	public bool IsOn;
	public CHaracter CharOn;

	// Use this for initialization
	void Start () 
	{
		Ray ray = new Ray (transform.position, transform.forward);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 10)) 
		{
			if(hit.transform.GetComponent<Tile>())
				Neighbors.Add(hit.transform.GetComponent<Tile>());
		}

		ray = new Ray (transform.position, -transform.forward);

		if (Physics.Raycast (ray, out hit, 10)) 
		{
			if(hit.transform.GetComponent<Tile>())
				Neighbors.Add(hit.transform.GetComponent<Tile>());
		}

		ray = new Ray (transform.position, transform.right);

		if (Physics.Raycast (ray, out hit, 10)) 
		{
			if(hit.transform.GetComponent<Tile>())
				Neighbors.Add(hit.transform.GetComponent<Tile>());
		}

		ray = new Ray (transform.position, -transform.right);

		if (Physics.Raycast (ray, out hit, 10)) 
		{
			if(hit.transform.GetComponent<Tile>())
				Neighbors.Add(hit.transform.GetComponent<Tile>());
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.GetComponent<CHaracter> ()) 
		{
			this.CharOn = other.transform.GetComponent<CHaracter> ();
			other.GetComponent<CHaracter> ().CurrentPos = this;

			foreach (Tile t in Neighbors) 
			{
				if (t.CharOn == this.CharOn) 
				{
					t.CharOn = null;
				}
			}
		}
	}
}
