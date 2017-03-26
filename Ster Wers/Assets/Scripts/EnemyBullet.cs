using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is for the green lasers that are fired from enemy TIE fighters

public class EnemyBullet : MonoBehaviour {

    private GameController gameController;
    public AudioSource enemyFireSound;
    public GameObject player;
    public GameObject explosion;

    private Vector3 spawn;
    private float startTime;
    private float speed = .3f;
    private Vector3 target;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
        spawn = transform.position;
        startTime = Time.time;
        target = player.transform.position;
        //enemyFireSound.Play();
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = Vector3.Lerp(spawn, target, (Time.time - startTime) / speed);
        transform.position = Vector3.MoveTowards(transform.position, target, (Time.time - startTime)/ speed);
        if(transform.position == target)
        {
            //Creates probability of bullet harming player
            int hit = Random.Range(0, 15);
            if(hit == 1)
            {
                Vector3 player = new Vector3(3.5f, .6f, -4.02f);
                gameController.gotHit();
                GameObject expl = Instantiate(explosion, player, transform.rotation) as GameObject;
                Destroy(explosion, 3);
            }
            Destroy(gameObject);
        }
	}

    
}
