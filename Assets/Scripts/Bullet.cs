using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    RouteManager LM;
    public CHaracter Shooter;
    public CHaracter Destination;
    public float BulletPower;
	// Use this for initialization
	void Start () {
        LM = GameObject.Find("LevelManager").GetComponent<RouteManager>();
        LM.SelectedChar = null;
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Destination.transform.position,Shooter.BulletSpeed  * Time.deltaTime);
    }
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destination.GetShot(BulletPower);
            Destroy(gameObject);
        }
       
    }

}
