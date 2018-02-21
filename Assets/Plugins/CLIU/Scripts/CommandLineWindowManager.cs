using UnityEngine;

/// <summary>
/// This script handles what screens should appear or disappear based on certain events
/// </summary>
public class CommandLineWindowManager : MonoBehaviour
{

    public GameObject window;
    public CommandLineCore commandLineCore;
    public GameObject buttonOpenWindow;

    void Update()
    {
        if (!window.activeSelf)
        {
            if (!string.IsNullOrEmpty(commandLineCore.openWindowHotkey))
            {
                if (Input.GetKeyDown(commandLineCore.openWindowHotkey))
                {
                    OpenCLIUWindow();
                }
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(commandLineCore.closeWindowHotkey))
            {
                if (Input.GetKeyDown(commandLineCore.closeWindowHotkey))
                {
                    CloseCLIUWindow();
                }
            }
        }

        if (!string.IsNullOrEmpty(commandLineCore.resetWindowHotkey))
        {
            if (Input.GetKeyDown(commandLineCore.resetWindowHotkey))
            {
                commandLineCore.RunCommand(new string[1] { "reset" });
            }
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