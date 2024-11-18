using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update\

    private bool cooldown;
    private Vector3 D;
    private Vector3 B;
    void Awake()
    {
        //Start();
        //Update();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (cooldown == false)
        {
            D = NewCords1();
            B = NewCords2();

            cooldown = true;
        }


        transform.position = Vector3.MoveTowards(transform.position,D, 1 * Time.deltaTime);

        if (transform.position == D)
            D = B;
        
        if (transform.position == B)
            cooldown = false;

    }
    
    Vector3 NewCords1()
    {
        Vector3 D = new Vector3(Random.Range(0,50-transform.position.x), Random.Range(0,-30+transform.position.y),transform.position.z);
        return D;
    }
    Vector3 NewCords2()
    {
        Vector3 B = new Vector3(-Random.Range(0,50-transform.position.x), Random.Range(0,30-transform.position.y),transform.position.z);
        return B;
    }

}
