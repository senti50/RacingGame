using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDurabilityMenager : MonoBehaviour
{
    public GameObject playerCarPrefab;
    public GameObject spawnPoint;
    public TextMesh durabilityText;
    public GameObject EndGameScreen;
    public int lifes;
    private GameObject playerCar;
    [HideInInspector]
    public int maxLifes;
    public GameObject[] hearts;


    void Start()
    {
        durabilityText.GetComponent<MeshRenderer>().sortingLayerName = "Durability";
        maxLifes = lifes;
        playerCar = (GameObject)Instantiate(playerCarPrefab, spawnPoint.transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (playerCar.GetComponent<PlayerCarMovment>().durability <= 0)
        {
            Destroy(playerCar);
            lifes--;
            Destroy(hearts[lifes]);
            if (lifes > 0)
            {
                StartCoroutine("SpawnaCar");
            }
            else if (lifes <= 0)
            {
                Time.timeScale = 0;
                EndGameScreen.SetActive(true);
            }
        }
        else if (playerCar.GetComponent<PlayerCarMovment>().durability > playerCar.GetComponent<PlayerCarMovment>().maxDuarbility)
        {
            playerCar.GetComponent<PlayerCarMovment>().durability = playerCar.GetComponent<PlayerCarMovment>().maxDuarbility;
        }

        durabilityText.text = "Durability: " + playerCar.GetComponent<PlayerCarMovment>().durability + "/" + playerCar.GetComponent<PlayerCarMovment>().maxDuarbility;

    }

    IEnumerator SpawnaCar()
    {
        playerCar = (GameObject)Instantiate(playerCarPrefab, spawnPoint.transform.position, Quaternion.identity);
        playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        playerCar.GetComponent<BoxCollider2D>().isTrigger = true;
        playerCar.tag = "Untouchable";
        yield return new WaitForSeconds(3);
        playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        playerCar.GetComponent<BoxCollider2D>().isTrigger = false;
        playerCar.tag = "Player";
    }

}
