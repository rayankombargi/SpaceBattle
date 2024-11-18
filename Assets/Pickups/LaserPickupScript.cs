using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserPickupScript : MonoBehaviour
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
        transform.name = "PUL";
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
            col.gameObject.GetComponent<MainShip>().UpdateAmmo(Random.Range(1,6));
            GameScipts.GetComponent<SpawnLaser>().DecreaseCount(Pickup);
            Destroy(Pickup);

        }
        
    }
    
    IEnumerator LastTime(int n)
    {
        yield return new WaitForSeconds(n);
        GameScipts.GetComponent<SpawnLaser>().DecreaseCount(Pickup);
        if (GameScipts.GetComponent<SpawnLaser>().count < GameScipts.GetComponent<SpawnLaser>().MaxCount)
            GameScipts.GetComponent<SpawnLaser>().Spawn(Pickup);
        Destroy(Pickup);
    }
}
