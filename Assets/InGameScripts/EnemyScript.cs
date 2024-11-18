using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class EnemyScript : SpaceShip
{
    // Start is called before the first frame update
    
    public GameObject Enemy;
    public Image HPBar;
    private float maxProx;
    private float distance;
    public GameObject Player;

    public GameObject laser;
    public GameObject Ll;
    public GameObject Lr;
    private bool right;
    private bool AttackWait;


    public GameObject Fire1;
    public GameObject Fire2;
    private float speed;
    public bool isRotating;

    public SpawnEnemies SEScript;

    void Awake()
    {
        Start();
        Update();
    }
    void Start()
    {
        transform.name = "Enemy";
        Player = GameObject.Find("SpaceShip");
        maxProx = 30f;
        defaultSpeed = 10f;
        speed = 0;
        health = 100;

        Ll = transform.Find("Ll").gameObject;
        Lr = transform.Find("Lr").gameObject;
        right = true;

        Fire1 = transform.Find("BaseFire1").gameObject;
        Fire2 = transform.Find("BaseFire2").gameObject;

        SEScript = GameObject.Find("GameScripts").GetComponent<SpawnEnemies>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();
        if (Player)
        {
            distance = Vector2.Distance(transform.position, Player.transform.position);
            if (distance < maxProx)
            {   
                Vector3 direction = Player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0,0,angle-90));
                
                if (AttackWait == false)
                {
                    StartCoroutine("Attack");
                }

                if (distance > 15)
                {
                    SetFire(true);
                    Accelerate(defaultSpeed);
                    transform.position += transform.up * speed * Time.deltaTime;
                }
                else
                {
                    SetFire(false);
                    Decelerate();

                }

                if (distance >= maxProx)
                {
                    SetFire(false);
                    Decelerate();
                    
                }
            }
            else
            {
                SetFire(true);
                Roam();
                StopCoroutine("MoveRotate");
            }
        }
    }

    void Roam()
    {
        Accelerate(defaultSpeed/2);
        transform.position += transform.up * speed * Time.deltaTime;

        if (isRotating == false)
        {
            isRotating = true;
            int rotationTime = Random.Range(0,8);
            int direction = Random.Range(0,3);
            StartCoroutine(MoveRotate(rotationTime,direction));
        }
    }

    IEnumerator MoveRotate(int s, int d)
    {
        float n = 0;
        if (d == 0)
        {
            yield return new WaitForSeconds(s);
        }
        else if (d == 1)
        {
            while (n < s)
            {
                transform.Rotate(0,0,Random.Range(50,100)*Time.deltaTime);
                n += Time.deltaTime;
                yield return null;
            }
        }
        else if (d == 2)
        {
            while (n < s)
            {
                transform.Rotate(0,0,-(Random.Range(50,100)*Time.deltaTime));
                n += Time.deltaTime;
                yield return null;
            }
        }

        StopCoroutine("MoveRotate");
    }

    IEnumerator Attack()
    {
        AttackWait = true;
        yield return new WaitForSeconds(1f);
        if (right == true)
        {
            Instantiate(laser, Ll.transform.position,Ll.transform.rotation);
            right = false;
        }
        else
        {
            Instantiate(laser, Lr.transform.position,Lr.transform.rotation);
            right = true;
        }
        AttackWait = false;
        StopCoroutine("Attack");
    }

    void Accelerate(float n)
    {
        if (speed < n)
        {
            speed += 10f * Time.deltaTime;
        }
    }

    void Decelerate()
    {
        if (speed > 0)
        {
            speed -= 10f * Time.deltaTime;
            transform.position += (transform.up) * speed * Time.deltaTime;
        }
    }

    void SetFire(bool b)
    {
        if (Fire1.activeSelf != b && Fire2.activeSelf != b)
        {
            Fire1.SetActive(b);
            Fire2.SetActive(b);

        }
    }

    void CheckHP()
    {
        if (health < 100)
        {
            HPBar.gameObject.transform.parent.parent.parent.gameObject.SetActive(true);
        }
        else
            HPBar.gameObject.transform.parent.parent.parent.gameObject.SetActive(false);

        HPBar.gameObject.transform.parent.parent.rotation = Quaternion.Euler(0,0,0);
        HPBar.gameObject.transform.parent.parent.position = new Vector2(transform.position.x, transform.position.y + 5);
        HPBar.fillAmount = health / 100;
        if (health <= 0)
        {
            SEScript.DecreaseCount();
            Destroy(Enemy);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bounds")
        {
            TakeDamage(health);
        }
    }

}
