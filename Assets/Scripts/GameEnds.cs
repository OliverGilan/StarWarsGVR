using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls when player gets hit by enemy laser
public class GameEnds : MonoBehaviour {

    public GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = this.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyLaser")
        {
            gameController.gotHit();
        }
        Destroy(other);
    }
}
