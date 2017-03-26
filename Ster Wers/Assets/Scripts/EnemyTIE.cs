using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is the TIE fighter class
//It controls when they spawn, how they roam, and when they attack the player
public class EnemyTIE : MonoBehaviour {

    public float speedMin;
    public float speedMax;
    private float speed;

    public float attackTimeMin;
    public float attackTimeMax;
    private float nextAttackTime;
    public float accuracy;
    private bool attackMode;
    private bool canShoot;
    private int burstCount;
    public GameObject flyby;
    public AudioSource shotSound;
    private float nextShot;
    public GameObject laser;
    public GameObject explosion;
    public Transform leftBarrel;
    public Transform rightBarrel;
    private Vector3 target;
    private Vector3 startPos;
    private float startTime;


    private GameController gameController;


	// Use this for initialization
	void Start () {
        speed = Random.Range(speedMin, speedMax);
        gameController = FindObjectOfType<GameController>();
        Roam();
        setNextAttack();
        burstCount = 0;
        //Physics.IgnoreCollision(laser.GetComponent<Collider>(), GetComponent<Collider>());
    }
	
    //This controls how the enemies move around the environment
    public void Roam()
    {
        canShoot = false;
        attackMode = false;
        startPos = transform.position;
        startTime = Time.time;
        target = Vector3.zero;
        target = new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
    }

    //Controls the destruction of the gameObject when hit by a player's laser
    public void destroy()
    {
        gameController.maxEnemies--;
        GameObject expl = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
        Destroy(expl, 4);
    }

    //Sets attack timer
    public void setNextAttack()
    {
        float attackInterval = Random.Range(attackTimeMin, attackTimeMax);
        nextAttackTime = Time.time + attackInterval;
    }

    //Makes TIE fly directly at player in attack mode
    void attack()
    {
        canShoot = true;
        startPos = target;
        target = flyby.transform.position;
        startTime = Time.time;
        setNextShot();
    }

    //Controls how the TIE fires lasers at the player
    void shoot()
    {
        Vector3 lb = leftBarrel.transform.position;
        Vector3 rb = rightBarrel.transform.position;
        Quaternion rotation = leftBarrel.rotation;
        rotation *=  Quaternion.Euler(0, 0, -45);
        Instantiate(laser, lb, rotation);
        //Instantiate(laser, rb, rotation);
        
        shotSound.Play();
    }

    //Sets timer for shooting
    public void setNextShot()
    {
        if(burstCount == 6)
        {
            nextShot = Time.time + 5f;
            burstCount = 0;
        }else
        {
            nextShot = Time.time + 0.2f;
        }
        
    }

    // Update is called once per frame
    void Update () {

        
        if (Time.time > nextAttackTime && !gameController.isGameOver)
        {
            attackMode = true;
        }

        //Keeps TIE within boundaries of the environment
        if (transform.position.x >= 90 || transform.position.x <= -90 || transform.position == target && attackMode == false)
        {
            Roam();
        }
        else if (transform.position.y >= 90 || transform.position.y <= -90 || transform.position == target && attackMode == false)
        {
            Roam();
        }
        else if (transform.position.z >= 90 || transform.position.z <= -90 || transform.position == target && attackMode == false)
        {
            Roam();
        }
        //Makes TIE fly away from player once finishing strafing run
        else if(transform.position == target && attackMode == true)
        {
            attack();
        }

        //Controls the bursts of the TIE fighter
        if (canShoot == true)
        {
            if (nextShot <= Time.time) { 
                shoot();
                burstCount++;
                setNextShot();
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        transform.LookAt(target);
        
	}

    //If TIE fighter gets too close to player
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "shield" && attackMode == true)
        {
            attackMode = false;
            Roam();
            setNextAttack();
        }else if(other.tag == "shield" && attackMode == false)
        {
            Roam();
        }else if (other.tag == "greenlaser")
        {
            
        }

    }


}
