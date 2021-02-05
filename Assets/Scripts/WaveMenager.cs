using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMenager : MonoBehaviour
{
    [Header("Wave 1 (Civil Cars)")]
    public GameObject civilCar;
    public float civilCarSpawnDelay;
    public int civilCarsAmount;
    [Header("Wave 2 (Bandit Cars)")]
    public GameObject banditCar;
    public int bombsAmount;
    public int banditCarVerticalSpeed;
    public int banditVarHorizontalSpeed;
    public float bombDelay;
    private GameObject spawnedBanditCar;
    private bool isSpawned;
    private bool is2ndSpawned;
    [Header("Wave 3 (Police Cars)")]
    public GameObject policeCar;
    public int policeCarAmount;
    [HideInInspector]
    static public bool isLeft;
    [HideInInspector]
    static public bool isRight;
    public float shootingSeriesDelay;
    public float singleShotDelay;
    public float policeCarVerticalSpeed;
    public int bulletsInSeries;
    private GameObject spawnedPoliceCar;

    [Header("Points")]
    public int pointsPerCivilCar;
    public int pointsPerBanditCar;
    public int pointsPerBomb;
    public int pointsPerPoliceCar;
    public GameObject EndGameScreen;

    private float[] lanesArray;
    private float spawnDelay;
    public float time;
    void Start()
    {
        lanesArray = new float[4];
        lanesArray[0] = -6.6f;
        lanesArray[1] = -2.22f;
        lanesArray[2] = 2.54f;
        lanesArray[3] = 6.6f;
        spawnDelay = civilCarSpawnDelay;
    }

    void Update()
    {
        spawnDelay -= Time.deltaTime;
        if (spawnDelay <= 0 && civilCarsAmount > 0)
        {
            spawnCar();
            spawnDelay = civilCarSpawnDelay;
        }
        else if (civilCarsAmount <= 0 && is2ndSpawned == false)
        {
            if (isSpawned == false)
            {
                spawnBanditCar();
            }
            else if (isSpawned == true && spawnedBanditCar.GetComponent<BanditCarBehavior>().bombsAmount < 10 && is2ndSpawned == false)
            {
                spawnBanditCar();
            }
        }
        else if (civilCarsAmount <= 0 && policeCarAmount > 0 && spawnedBanditCar == null)
        {
            spawnPoliceCar();
        }else if (policeCarAmount==0 && isLeft==false && isRight==false)

        {


            time = Time.timeScale=0;
           
          
            
            EndGameScreen.SetActive(true);
        }
    }

    void spawnPoliceCar()
    {
        Transform playerCarPosition;
        if (GameObject.FindWithTag("Player"))
        {
            playerCarPosition = GameObject.FindWithTag("Player").transform;
        }else if (GameObject.FindWithTag("Shield"))
        {
            playerCarPosition = GameObject.FindWithTag("Shield").transform;
        }else if (GameObject.FindWithTag("Untouchable"))
        {
            playerCarPosition = GameObject.FindWithTag("Untouchable").transform;
        }
        else
        {
            playerCarPosition = null;
        }
        if (playerCarPosition.position.x <= -1.21f && isRight == false)
        {
            spawnedPoliceCar = (GameObject)Instantiate(policeCar, new Vector3(6.5f, -7f, 0), Quaternion.identity);
            spawnedPoliceCar.GetComponent<PoliceCarBehavior>().isLeft = false;
            isRight = true;
            policeCarAmount--;
        }
        else if (playerCarPosition.position.x > -1.21f && isLeft == false)
        {
            spawnedPoliceCar = (GameObject)Instantiate(policeCar, new Vector3(-6.5f, -7f, 0), Quaternion.identity);
            spawnedPoliceCar.GetComponent<PoliceCarBehavior>().isLeft = true;
            isLeft = true;
            policeCarAmount--;
        }
        spawnedPoliceCar.GetComponent<PoliceCarBehavior>().shootingSeriesDelay = shootingSeriesDelay;
        spawnedPoliceCar.GetComponent<PoliceCarBehavior>().singleShotDelay = singleShotDelay;
        spawnedPoliceCar.GetComponent<PoliceCarBehavior>().bulletsInSeries = bulletsInSeries;
        spawnedPoliceCar.GetComponent<PoliceCarBehavior>().policeCarVerticalSpeed = policeCarVerticalSpeed;
        spawnedPoliceCar.GetComponent<PoliceCarBehavior>().pointsPerCar = pointsPerPoliceCar;
    }

    void spawnBanditCar()
    {
        if (isSpawned == false)
        {
            spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(Random.Range(-8f, 8f), 7f, 0), Quaternion.identity);
            spawnedBanditCar.GetComponent<BanditCarBehavior>().bombDelay = bombDelay;
            isSpawned = true;
        }
        else if (isSpawned == true && is2ndSpawned == false)
        {
            if (spawnedBanditCar.transform.position.x < -1f)
            {
                spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(3f, 7f, 0), Quaternion.identity);
                is2ndSpawned = true;
            }
            else if (spawnedBanditCar.transform.position.x >= -1f)
            {
                spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(-3f, 7f, 0), Quaternion.identity);
                is2ndSpawned = true;
            }
            spawnedBanditCar.GetComponent<BanditCarBehavior>().bombDelay = bombDelay/1.5f;
        }
        spawnedBanditCar.GetComponent<BanditCarBehavior>().bombsAmount = bombsAmount;
        spawnedBanditCar.GetComponent<BanditCarBehavior>().banditCarVerticalSpeed = banditCarVerticalSpeed;
        spawnedBanditCar.GetComponent<BanditCarBehavior>().banditVarHorizontalSpeed = banditVarHorizontalSpeed;
        
        spawnedBanditCar.GetComponent<BanditCarBehavior>().pointsPerCar = pointsPerPoliceCar;
        spawnedBanditCar.GetComponent<BanditCarBehavior>().bomb.GetComponent<Bomb>().pointsPerBomb = pointsPerBomb;
    }

    void spawnCar()
    {
        int lane = Random.Range(0, 4);
        if (lane == 0 || lane == 1)
        {
            GameObject car = (GameObject)Instantiate(civilCar, new Vector3(lanesArray[lane], 6f, 0), Quaternion.Euler(new Vector3(0, 0, 180)));
            car.GetComponent<CivilCarBehavior>().direction = 1;
            car.GetComponent<CivilCarBehavior>().civilCarSpeed = 12f;
            car.GetComponent<CivilCarBehavior>().pointsPerCar = pointsPerCivilCar;
        }
        if (lane == 2 || lane == 3)
        {
            GameObject car = (GameObject)Instantiate(civilCar, new Vector3(lanesArray[lane], 6f, 0), Quaternion.identity);
            car.GetComponent<CivilCarBehavior>().pointsPerCar = pointsPerCivilCar;
        }
        civilCarsAmount--;
    }

}
