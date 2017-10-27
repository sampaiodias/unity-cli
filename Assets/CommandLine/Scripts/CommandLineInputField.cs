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
            commandPos--;
            try
            {
                inputField.text = previousCommands[Mathf.Clamp(commandPos, 0, previousCommands.Count - 1)];
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            commandPos++;

            if (commandPos > previousCommands.Count - 1)
            {
                commandPos = previousCommands.Count;
                inputField.text = "";
            }
            else
            {
                try
                {
                    inputField.text = previousCommands[Mathf.Clamp(commandPos, 0, previousCommands.Count - 1)];
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            
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
        commandPos = previousCommands.Count;
    }
}
