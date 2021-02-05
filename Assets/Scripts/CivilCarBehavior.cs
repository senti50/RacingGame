using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilCarBehavior : MonoBehaviour
{

    public float crashDemage=20f;
    public GameObject explosion;
    [HideInInspector]
    public int pointsPerCar;
    public float civilCarSpeed = 5f;
    public int direction = -1;


    private Vector3 civilCarPosition;



    private void Update()
    {
        this.gameObject.transform.Translate(new Vector3(0, direction, 0) * civilCarSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PointsMenager.points -= pointsPerCar;
            collision.gameObject.GetComponent<PlayerCarMovment>().durability -= crashDemage;
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Shield")
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "EndOfTheRoad")
        {
            PointsMenager.points += pointsPerCar;
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerCarMovment>().durability -= crashDemage / 4;
        }
    }
}
