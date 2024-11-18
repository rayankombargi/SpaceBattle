using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
    public GameObject Pickup;
    public GameObject GameScipts;
    void Awake()
    {
        Start();
        Update();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameScipts = GameObject.Find("GameScripts");
        transform.name = "PUH";
        //StartCoroutine(LastTime(30));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,100f * Time.deltaTime,0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.gameObject.GetComponent<MainShip>().health < 100)
            {
                col.gameObject.GetComponent<MainShip>().GainHealth(20);
                GameScipts.GetComponent<SpawnHealth>().DecreaseCount(Pickup);
                Destroy(Pickup);
            }
        }

    }
    
    IEnumerator LastTime(int n)
    {
        yield return new WaitForSeconds(n);
        GameScipts.GetComponent<SpawnHealth>().DecreaseCount(Pickup);
        if (GameScipts.GetComponent<SpawnHealth>().count < GameScipts.GetComponent<SpawnHealth>().MaxCount)
            GameScipts.GetComponent<SpawnHealth>().Spawn(Pickup);
        Destroy(Pickup);
    }
}
