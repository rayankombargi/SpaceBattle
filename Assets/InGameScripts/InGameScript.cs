using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScript : MonoBehaviour
{

    public float Score;
    public bool isPaused;

    public Canvas DataCanvas;
    public Canvas PauseCanvas;

    private GameObject Ship;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        isPaused = false;
        Ship = GameObject.Find("SpaceShip");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false && Ship != null)
            {
                isPaused = true;
                Time.timeScale = 0;
                DataCanvas.gameObject.SetActive(false);
                PauseCanvas.gameObject.SetActive(true);
            }
            else if (Ship != null)
            {
                isPaused = false;
                Time.timeScale = 1;
                DataCanvas.gameObject.SetActive(true);
                PauseCanvas.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateScore(float n)
    {
        Score += n;
    }

    public void UpdatePause(bool b)
    {
        if (isPaused = !b)
        {
            isPaused = b;
        }
    }

}
