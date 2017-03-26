using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls player lasers once they are fired
public class LaserController : MonoBehaviour {

    public GameObject explosion;
    private GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
	}
	
    
    //Destroys laser and initiates TIE destroy function on impact
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //Destroy's laser
            Destroy(gameObject);
            //Adds point to the score
            gameController.GotOne();
            //Destroy's enemy
            other.GetComponent<EnemyTIE>().destroy();
        }else if (other.tag == "restartB") //Restarts game
        {
            gameController.NewGame();
        }
    }

}
