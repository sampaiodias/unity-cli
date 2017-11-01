using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLineWindowManager : MonoBehaviour {

    public GameObject window;
    public CommandLineCore commandLineCore;
    public GameObject buttonOpenWindow;
	
	void Update () {
        if (!window.activeSelf && Input.GetKeyDown(commandLineCore.openWindowHotkey))
        {
            OpenCLIUWindow();
        }
    }

    public void OpenCLIUWindow()
    {
        window.SetActive(true);
        buttonOpenWindow.SetActive(false);
    }

    public void CloseCLIUWindow()
    {
        window.SetActive(false);
        if (!commandLineCore.hideOpenWindowButton)
        {
            buttonOpenWindow.SetActive(true);
        }
    }
}
