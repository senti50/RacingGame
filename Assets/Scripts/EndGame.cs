using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public Text gainedPointsText;
    public Text extraLifesBonusText;
    public Text altogetherPointsText;
    
    public int everyExtraLifeBonus;
    

    private GameObject GameManager;
    private GameObject PlayerCar;

    void Start()
    {

        gainedPointsText.text = PointsMenager.points.ToString();
        GameManager = GameObject.Find("GameMenager");
        extraLifesBonusText.text = (GameManager.GetComponent<CarDurabilityMenager>().lifes * everyExtraLifeBonus).ToString();
       // Debug.Log("dodatkowe punkty za zycie"+extraLifesBonusText.text);
        

        altogetherPointsText.text = (int.Parse(gainedPointsText.text) + int.Parse(extraLifesBonusText.text)).ToString();


    }

    public void RetryButton()
    {
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //GameManager.GetComponent<WaveMenager>().time = Time.timeScale=1;
        Time.timeScale = 1;
    }

    public void MenuExitButton()
    {
        SceneManager.LoadScene(1);
    }
}
