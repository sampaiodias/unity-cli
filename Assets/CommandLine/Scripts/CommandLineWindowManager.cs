using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLineWindowManager : MonoBehaviour {

    public GameObject inputField;
    public CommandLineCore commandLineCore;
    public GameObject buttonOpenWindow;
	
	void Update () {
        if (!inputField.activeSelf && Input.GetKeyDown(commandLineCore.openWindowHotkey))
        {
            OpenCLIUWindow();
        }
    }

    public void OpenCLIUWindow()
    {
        inputField.SetActive(true);
        buttonOpenWindow.SetActive(false);
    }

    public void CloseCLIUWindow()
    {
        inputField.SetActive(false);
        if (!commandLineCore.hideOpenWindowButton)
        {
            buttonOpenWindow.SetActive(true);
        }
    }
}
