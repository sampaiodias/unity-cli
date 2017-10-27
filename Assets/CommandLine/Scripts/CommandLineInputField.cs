using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandLineInputField : MonoBehaviour {

    public InputField inputField;
    public CommandLineCore cliCore;
    public EventSystem myEventSystem;
    private List<string> previousCommands;
    int commandPos = -1;

    private void Start()
    {
        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            myEventSystem.gameObject.SetActive(true);
        }

        previousCommands = new List<string>();
    }

    void Update () {
        inputField.ActivateInputField();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            commandPos++;
            //inputField.text = previousCommands[commandPos];
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Show next command");
        }
    }

    public void SendCommandToCore()
    {
        if (inputField.text != "")
        {
            SaveCommand(inputField.text);

            string[] args = inputField.text.Split(' ');

            cliCore.RunCommand(args);
            inputField.text = "";
        }
    }

    private void SaveCommand(string fullCommand)
    {
        previousCommands.Add(fullCommand);
        commandPos = -1;
    }
}
