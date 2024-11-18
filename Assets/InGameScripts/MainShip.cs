using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MainShip : SpaceShip
{
    // Start is called before the first frame update
    public GameObject Ship;

    public GameObject Lleft;
    public GameObject Lright;
    public bool right;
    public GameObject Laser;
    public LaserScript laserScript;
    public int laserAmmo;


    public GameObject Fire1;
    public GameObject Fire2;

    public float speed;
    public float turboStamina;
    public bool cooldown;

    public GameObject MainCam;
    public Camera Cam;
    float x;
    float y;
   
    public Rigidbody2D RB;

    void Awake()
    {
        Start();
        Update();
    }
    void Start()
    {    
        Ship.name = "SpaceShip";
        Lleft = transform.Find("Ll").gameObject;
        Lright = transform.Find("Lr").gameObject;
        right = true;
        laserScript = FindObjectOfType<LaserScript>();
        laserAmmo = 25;

        Fire1 = transform.Find("BaseFire1").gameObject;
        Fire1.SetActive(false);
        Fire2 = transform.Find("BaseFire2").gameObject;
        Fire2.SetActive(false);

        defaultSpeed = 15f;
        speed = 0;
        turboStamina = 100f;
        cooldown = false;

        health = 100f;

        MainCam = GameObject.Find("MainCamera");
        Cam = MainCam.GetComponent<Camera>();
        x = Ship.transform.position.x;
        y = Ship.transform.position.y;

        RB = transform.GetComponent<Rigidbody2D>();
    }   

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameScripts").GetComponent<InGameScript>().isPaused == false)
        {
            UpdateCamera();
            Movements();
            Attack();
            CheckHP();
        }
    }

    void UpdateCamera()
    {
        if (Ship.transform.position.x < 500 && Ship.transform.position.x > -500)
            x = Ship.transform.position.x;

        if (Ship.transform.position.y < 150 && Ship.transform.position.y > -150)
            y = Ship.transform.position.y;

        MainCam.transform.position = new Vector3(x, y, -40);
       
    }

    public void stopTurbo()
    {
        StartCoroutine("ZoomOutTurbo");

        if (speed != defaultSpeed)
            StartCoroutine("DecelerateTurbo");

        if (Fire1.transform.Find("Fire002").gameObject.transform.localScale.z > 1 && Fire1.transform.Find("Fire002").gameObject.transform.localScale.z > 1)
        {
            Fire1.transform.Find("Fire002").gameObject.transform.localScale = new Vector3(Fire1.transform.Find("Fire002").gameObject.transform.localScale.x,Fire1.transform.Find("Fire002").gameObject.transform.localScale.y,1);
            Fire2.transform.Find("Fire002").gameObject.transform.localScale = new Vector3(Fire2.transform.Find("Fire002").gameObject.transform.localScale.x,Fire2.transform.Find("Fire002").gameObject.transform.localScale.y,1);
        }
    }

    public void startTurbo()
    {
        if (turboStamina > 0 && cooldown == false)
        {
            if (Cam.orthographic)
            {
                if (Cam.orthographicSize < 25)
                {
                    Cam.orthographicSize += 5 * Time.deltaTime;
                }
            }

            Accelerate(defaultSpeed * 2);

            if (Fire1.transform.Find("Fire002").gameObject.transform.localScale.z < 5 && Fire2.transform.Find("Fire002").gameObject.transform.localScale.z < 5)
            {
                Fire1.transform.Find("Fire002").gameObject.transform.localScale += new Vector3(0,0,0.2f); 
                Fire2.transform.Find("Fire002").gameObject.transform.localScale += new Vector3(0,0,0.2f);
            }

            turboStamina -= 6f * Time.deltaTime;
            
            if (turboStamina < 0)
                turboStamina = 0;
            if (turboStamina <= 0)
            {
                cooldown = true;
                stopTurbo();
            }
        }
    }

    void Movements()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            
            if (Fire1.activeSelf == false && Fire2.activeSelf == false)
            {
                Fire1.SetActive(true);
                Fire2.SetActive(true);
            }

            if (Cam.orthographic)
            {
                if (Cam.orthographicSize < 15)
                {
                    Cam.orthographicSize += 5 * Time.deltaTime;
                }
            }

            Accelerate(defaultSpeed);

            transform.position += (transform.up) * speed * Time.deltaTime;
            //RB.MovePosition(transform.position + (transform.up * speed * Time.deltaTime));
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {

            if (Fire1.activeSelf == true && Fire2.activeSelf == true)
            {
                Fire1.SetActive(false);
                Fire2.SetActive(false);
            }
            stopTurbo();
            StartCoroutine("ZoomOut");
            StartCoroutine("Decelerate");

        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0,0,100f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0,0,-100f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            startTurbo();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            stopTurbo();
        }

        if (turboStamina >= 100 && cooldown == true)
            cooldown = false;

        if (turboStamina < 100 && ((!(Input.GetKey(KeyCode.LeftShift)) && cooldown == false) || (cooldown == true)))
            turboStamina += 12f * Time.deltaTime;
        if (turboStamina > 100)
            turboStamina = 100;
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Laser != null && laserAmmo > 0)
            {
                if (right == true)
                {
                    Instantiate(Laser,Lright.transform.position,Lright.transform.rotation);
                    right = false;
                }
                else
                {
                    Instantiate(Laser,Lleft.transform.position,Lleft.transform.rotation);
                    right = true;
                }
                UpdateAmmo(-1);
            }
        }        
    }
    void CheckHP()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f;
            MainCam.transform.Find("DataCanvas").gameObject.SetActive(false);
            MainCam.transform.Find("GameOverCanvas").gameObject.SetActive(true);
            Destroy(Ship);
        }
    }

    public void UpdateAmmo(int n)
    {
        laserAmmo += n;
    }

    void Accelerate(float n)
    {
        if (speed < n)
        {
            speed += 10f * Time.deltaTime;
        }
    }


    IEnumerator Decelerate()
    {
        while (speed > 0 && !(Input.GetKey(KeyCode.W)))
        {
            speed -= 10f * Time.deltaTime;
            transform.position += (transform.up) * speed * Time.deltaTime;
            //RB.MovePosition(transform.position + (transform.up * speed * Time.deltaTime));

            yield return null;
        }

        StopCoroutine("Decelerate");
    }
    IEnumerator DecelerateTurbo()
    {
        while (speed > defaultSpeed && (!(Input.GetKey(KeyCode.LeftShift)) || ((Input.GetKey(KeyCode.LeftShift)) && cooldown == true)))
        {
            speed -= 10f * Time.deltaTime;
            yield return null;
        }

        StopCoroutine("DecelerateTurbo");
    }


    IEnumerator ZoomOut()
    {
        if (Cam.orthographic)
        {
            while (Cam.orthographicSize > 10 && !(Input.GetKey(KeyCode.LeftShift)))
            {
                Cam.orthographicSize -= 5 * Time.deltaTime;
                yield return null;
            }
        }
        StopCoroutine("ZoomOut");
    }
    IEnumerator ZoomOutTurbo()
    {
        if (Cam.orthographic)
        {
            while (Cam.orthographicSize > 15 && (!(Input.GetKey(KeyCode.LeftShift)) || ((Input.GetKey(KeyCode.LeftShift)) && cooldown == true)))
            {
                Cam.orthographicSize -= 5 * Time.deltaTime;
                yield return null;
            }
        }
        StopCoroutine("ZoomOutTurbo");
    }
}
