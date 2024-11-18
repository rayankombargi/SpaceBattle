using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseScript : MonoBehaviour
{

    public Canvas DataCanvas;
    public Canvas PauseCanvas;
    public Canvas SettingsCanvas;
    public GameObject GameScripts;
    public InGameScript IGScript;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        IGScript = GameScripts.GetComponent<InGameScript>();
        isPaused = IGScript.isPaused;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickResume()
    {
        if (isPaused == true)
        {
           IGScript.UpdatePause(false);
            Time.timeScale = 1;
            DataCanvas.gameObject.SetActive(true);
            PauseCanvas.gameObject.SetActive(false);
        }
    }   
    public void ClickReplay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ClickSettings()
    {
        PauseCanvas.gameObject.SetActive(false);
        SettingsCanvas.gameObject.SetActive(true);
    }

    public void ClickMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
