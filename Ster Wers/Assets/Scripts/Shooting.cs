using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controls the player's shooting mechanics
public class Shooting : MonoBehaviour {

    public Rigidbody laserPrefab;
    public Transform rightBarrel;
    public Transform leftBarrel;
    public AudioSource fireSound;

    private GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GvrViewer.Instance.VRModeEnabled && GvrViewer.Instance.Triggered && !gameController.isGameOver)
        {
            Rigidbody rightLaserInstance;
            rightLaserInstance = Instantiate(laserPrefab, rightBarrel.position, rightBarrel.rotation) as Rigidbody;
            rightLaserInstance.AddForce(rightBarrel.up * 3000);
            Rigidbody leftLaserInstance;
            leftLaserInstance = Instantiate(laserPrefab, leftBarrel.position, leftBarrel.rotation) as Rigidbody;
            leftLaserInstance.AddForce(leftBarrel.up * 3000);
            fireSound.Play();
        }
        //For debugging on pc
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rightLaserInstance;
            rightLaserInstance = Instantiate(laserPrefab, rightBarrel.position, rightBarrel.rotation) as Rigidbody;
            rightLaserInstance.AddForce(rightBarrel.up * 3000);
            Rigidbody leftLaserInstance;
            leftLaserInstance = Instantiate(laserPrefab, leftBarrel.position, leftBarrel.rotation) as Rigidbody;
            leftLaserInstance.AddForce(leftBarrel.up * 3000);
            fireSound.Play();
        }
    }
}
