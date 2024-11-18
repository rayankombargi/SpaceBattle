using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLaserScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject laser;
    public float speed;
    void Start()
    {
        transform.name = "EnemyLaser";
        speed = 100f;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    IEnumerator LaserLast()
    {
        yield return new WaitForSeconds(4f);
        Destroy(laser);

    }

    void Awake()
    {
        Start();
        Update();
        StartCoroutine(LaserLast());
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<SpaceShip>().TakeDamage(10);
            Destroy(laser);
        }
    }

}
