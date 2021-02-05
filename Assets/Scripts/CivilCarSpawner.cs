using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilCarSpawner : MonoBehaviour
{
    public float carSpawnDelay = 2f;
    public GameObject civilCar;


    private float spawnDelay;
    private float[] lanesArray;

    private void Start()
    {
        lanesArray = new float[4];
        lanesArray[0] = -6.6f;
        lanesArray[1] = -2.22f;
        lanesArray[2] = 2.54f;
        lanesArray[3] = 6.6f;
        spawnDelay = carSpawnDelay;
    }

    private void Update()
    {
        spawnDelay -= Time.deltaTime;
        if (spawnDelay <= 0)
        {
            spawnCar();
            spawnDelay = carSpawnDelay;
        }
    }

    void spawnCar()
    {
        int lane = Random.Range(0, 4);
        if (lane == 2 || lane == 3)
        {
            Instantiate(civilCar, new Vector3(lanesArray[lane], 7.1f, 0), Quaternion.identity);
        }
        if (lane == 0 || lane == 1)
        {
           
            GameObject car= (GameObject) Instantiate(civilCar, new Vector3(lanesArray[lane], 7.1f, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
            car.GetComponent<CivilCarBehavior>().direction = 1;
            car.GetComponent<CivilCarBehavior>().civilCarSpeed = 12f;
        }


    }
}
