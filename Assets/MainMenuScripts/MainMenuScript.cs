using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public Canvas MainCanvas;
    public Canvas ProfileCanvas;
    public Canvas SettingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        //Time.timeScale = 0f;
        SceneManager.LoadScene("InGame");
    }
    public void ProfileButton()
    {
        MainCanvas.gameObject.SetActive(false);
        ProfileCanvas.gameObject.SetActive(true);
    }
    public void SettingsButton()
    {
        MainCanvas.gameObject.SetActive(false);
        SettingsCanvas.gameObject.SetActive(true);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
