using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DataScript : MonoBehaviour
{
    public TextMeshProUGUI ScoreVal;
    public float Score;
    public Image HPBar;
    public float HP;
    public Image TurboBar;
    public float turboStamina;
    public TextMeshProUGUI LaserAmmoVal;
    public int laserAmmo;
    public MainShip MainShipScript;
    private GameObject Ship;
    public InGameScript InGameScript;
    private GameObject Game;


    void Awake()
    {
        //Start();
        //Update();
    }
    void Start()
    {

        StartCoroutine("FindShip");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Ship != null)
        {
            HP = MainShipScript.health;
            turboStamina = MainShipScript.turboStamina;
            laserAmmo = MainShipScript.laserAmmo;
            Score = InGameScript.Score;
            ScoreVal.text = Score.ToString();
            HPBar.fillAmount = HP / 100;
            TurboBar.fillAmount = turboStamina / 100;
            LaserAmmoVal.text = laserAmmo.ToString();
        }


    }

    IEnumerator FindShip()
    {
        do
        {
            Ship = GameObject.Find("SpaceShip");
            yield return null;
        } while (Ship == null);

        MainShipScript = Ship.GetComponent<MainShip>();

        //StopCoroutine("FindSHip");
    }
}
