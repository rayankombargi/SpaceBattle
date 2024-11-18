using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject Ship;
    public Canvas DataCanvas;


    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        Instantiate(Ship, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
        DataCanvas.gameObject.SetActive(true);
    }

}
