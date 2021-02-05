using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditCarBehavior: MonoBehaviour
{
    public GameObject bomb;
    public int bombsAmount;
    public int banditCarVerticalSpeed;
    public int banditVarHorizontalSpeed;
    public float bombDelay;
    
    [HideInInspector]
    public int pointsPerCar;

    private float Delay;
    private GameObject playerCar;
    private Vector3 banditCarPos;

    void Start()
    {
        playerCar = GameObject.FindWithTag("Player");
        
        Delay = bombDelay;
    }
    private void FixedUpdate()
    {
        if (playerCar == null)
        {
            playerCar = GameObject.FindWithTag("Player");
        }
        else
        {
            banditCarPos = Vector3.Lerp(transform.position, playerCar.transform.position, Time.fixedDeltaTime * banditVarHorizontalSpeed);
            Mathf.Clamp(banditCarPos.x, -7.9f, 7.9f);
            transform.position = new Vector3(banditCarPos.x, transform.position.y, 0);
        }
    }
    void Update()
    {
        
        
            if (gameObject.transform.position.y > 3.1f && bombsAmount > 0)
            {
                this.gameObject.transform.Translate(new Vector3(0, -1, 0) * banditCarVerticalSpeed * Time.deltaTime);

            }
            else if (bombsAmount <= 0)
            {
                this.gameObject.transform.Translate(new Vector3(0, 1, 0) * banditCarVerticalSpeed * Time.deltaTime);
                if (gameObject.transform.position.y > 7.5f)
                {
                    PointsMenager.points += pointsPerCar;
                    Destroy(this.gameObject);
                }
            }
            else
            {
                

                Delay -= Time.deltaTime;
                if (Delay <= 0 && bombsAmount > 5)
                {
                    Delay = bombDelay;
                    bombsAmount--;
                    Instantiate(bomb, transform.position, Quaternion.identity);
                }
                else if (Delay <= 0 && bombsAmount <= 5 && bombsAmount > 0)
                {
                    Delay = bombDelay / 2;
                    bombsAmount--;
                    Instantiate(bomb, transform.position, Quaternion.identity);
                }
            }
            
        }
    }

