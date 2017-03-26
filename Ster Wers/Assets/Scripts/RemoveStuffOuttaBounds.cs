using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the boundary of the environment
public class RemoveStuffOuttaBounds : MonoBehaviour {

    GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    //Destroys any player lasers that hit boundary
    //Destroys TIE fighter if they hit boundary (they shouldn't roam that far)
    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyTIE>().destroy();
        }else
        {
            Destroy(other.gameObject);
        }
       
    }
	
}
