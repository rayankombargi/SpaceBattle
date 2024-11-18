using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class SpawnEnemies: MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] private float px;
    [SerializeField] private float py;
    [SerializeField] private float pz;
    [SerializeField] private float rx;
    [SerializeField] private float ry;
    [SerializeField] private float rz;

    public float WaitTime;
    [SerializeField] private int count = 0;
    public int maxCount;

    public void Spawn()
    {
        
        px = Random.Range(-500f, 500f);
        py = Random.Range(-150f, 150f);
        rz = Random.Range(-180f,180f);

        Instantiate(enemy, new Vector3(px,py,pz), Quaternion.Euler(rx,ry,rz));
        IncreaseCount();

    }


    void Start()
    {
        for (int i = 0; i < maxCount/2; i++)
        {
            Spawn();
        }
        StartCoroutine("InitiateSpawn");
    }

    void Update()
    {
            
    }


    IEnumerator InitiateSpawn()
    {
        while (true)
        {
            if (count == maxCount)
            {
                GameObject currentEnemy = GameObject.FindWithTag("Enemy");
                if (currentEnemy != null)
                {
                    Destroy(currentEnemy);
                    DecreaseCount();
                }
            }

            Spawn();


            yield return new WaitForSeconds(WaitTime);

        }
        
    }

    public void IncreaseCount()
    {
        count++;
        //Debug.Log(count);
    }
    public void DecreaseCount()
    {
        if (count > 0)
            count--;
        //Debug.Log(count);
    }
}
