using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawns enemy TIE fighters
public class EnemySpawner : MonoBehaviour {

    public float maxZSpawn;
    public float minZSpawn;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float launchRangeMinTime;
    public float launchRangeMaxTime;
    public GameObject enemyPrefab;

    private float _nextLaunchTime;
    private float _ySpawn = 0.4f;
    private GameController gameController;
    private Quaternion _launchRotation;

	// Use this for initialization
	void Start () {
        setNextLaunch();
        gameController = FindObjectOfType<GameController>();
	}

    //Sets timer for next launch
    void setNextLaunch()
    {
        float launchInterval = Random.Range(launchRangeMinTime, launchRangeMaxTime);
        _nextLaunchTime = Time.time + launchInterval;
    }
	
	// Update is called once per frame
	void Update () {
        //If there are already 5 enemies, do not spawn another
        if (gameController.maxEnemies == 5)
        {
            setNextLaunch();
        }
        //Spawns enemy
		if (Time.time > _nextLaunchTime && !gameController.isGameOver)
        {
            Vector3 launchPosition = new Vector3(Random.Range(minX,maxX), Random.Range(minY,maxY), Random.Range(minZSpawn, maxZSpawn));
            Instantiate(enemyPrefab, launchPosition, _launchRotation);
            gameController.maxEnemies++;
            setNextLaunch();
        }
	}
}
