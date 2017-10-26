using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruondTile : MonoBehaviour {
   public List<Tile> _tile = new List<Tile>();
    public List<Tile> Add = new List<Tile>();
	// Use this for initialization
	void Start () {
        _tile.AddRange( GetComponentsInChildren<Tile>());
	}
	
	
	
   
}
