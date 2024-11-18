using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject laser;
    public float speed;

    public InGameScript GameScript;
    void Start()
    {
        speed = 100f;
        GameScript = GameObject.Find("GameScripts").GetComponent<InGameScript>();
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
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<SpaceShip>().TakeDamage(50);
            Destroy(laser);
            if (col.gameObject.GetComponent<EnemyScript>().health <= 0)
            {
                GameScript.UpdateScore(50);
            }
            //Debug.Log(count);
        }
    }

}
