using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UINavigation : MonoBehaviour
{
    GameObject[] elems;
    GameObject[] Buttons;

    [SerializeField] private int index = 0;

    private int buttonSize;
    // Start is called before the first frame update
    void Start()
    {
        elems = new GameObject[transform.childCount];

        for (int i = 0; i < elems.Length; i++)
        {
            elems[i] = transform.GetChild(i).gameObject;

            if (elems[i].transform.GetComponent<Button>() != null)
            {
                buttonSize++;
            }
        }

        Buttons = new GameObject[buttonSize];
        int j = 0;
        for (int i = 0; i < elems.Length; i++)
        {
            if (elems[i].transform.GetComponent<Button>() != null)
            {
                Buttons[j] = elems[i];
                j++;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (index < Buttons.Length-1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (index > 0)
            {
                index--;
            }
            else
            {
                index = Buttons.Length-1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            Buttons[index].GetComponent<Button>().onClick.Invoke();
        }

        if (Input.anyKeyDown)
            Buttons[index].GetComponent<Button>().Select();
    }
}
