using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandLineInputField : MonoBehaviour {

    public InputField inputField;
    public CommandLineCore cliCore;
    public EventSystem myEventSystem;
    public ScrollRect scroll; //Test
    public Text output;
    private List<string> previousCommands;
    int commandPos = -1;

    private void Start()
    {
        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            myEventSystem.gameObject.SetActive(true);
        }

        inputField.onEndEdit.AddListener(val =>
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                SendCommandToCore();
        });

        previousCommands = new List<string>();
    }

    void Update () {
        inputField.ActivateInputField();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            commandPos--;
            if (commandPos < 0)
            {
                commandPos = 0;
            }

            try
            {
                if (previousCommands.Count > 0)
                {
                    inputField.text = previousCommands[Mathf.Clamp(commandPos, 0, previousCommands.Count - 1)];
                }                
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
            PrintInputOnView(inputField.text);

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

    private void PrintInputOnView(string message)
    {
        output.text = output.text + "\n> " + message;
    }

    public void PrintOutputOnView(string message)
    {
        output.text = output.text + "\n" + message + "\n";
    }

    public void Clear()
    {
        output.text = "";
    }
}
