using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarMovment : MonoBehaviour
{
    public float carHorizontalSpedd = 5f;
    private Vector3 carPosition;


    public float maxDuarbility = 100f;
   
    public float durability;

    private void Start()
    {
        
        carPosition = this.gameObject.transform.position;
        durability = maxDuarbility;
    }


    private void Update()
    {
        carPosition.x += Input.GetAxis("Horizontal") * carHorizontalSpedd * Time.deltaTime;
        carPosition.x = Mathf.Clamp(carPosition.x, -8.35f, 8.35f);
        this.gameObject.transform.position = carPosition;
    }
}
