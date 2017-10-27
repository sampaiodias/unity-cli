using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CommandLineCore : MonoBehaviour {

    public bool destroyWhenOtherSceneIsLoaded = false;
    public bool startHidden = true;
    private GameObject modulesParent;
    private GameObject inputField;
    private GameObject[] commandLineModules;
    private List<CommandLineModuleSettings> moduleSettings;
    private List<string> moduleNames = new List<string>();

    private void Start()
    {
        InitiateModules();
        if (destroyWhenOtherSceneIsLoaded)
        {
            DontDestroyOnLoad(gameObject);
        }
        if (startHidden)
        {
            inputField.SetActive(false);
        }
    }

    private void Update()
    {
        if (!inputField.activeSelf && Input.GetKeyDown(KeyCode.RightBracket))
        {
            inputField.SetActive(true);
        }
    }

    public void RunCommand(string[] args)
    {
        string firstArg = args[0].ToLower();

        if(firstArg == "help" || firstArg == "h")
        {
            ShowHelp(args);            
        }
        else if (firstArg == "modules" || firstArg == "m")
        {
            ShowModulesLoaded();
        }
        else if (firstArg == "hide" || firstArg == "close")
        {
            inputField.SetActive(false);
        }
        else if (firstArg == "exit")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;        
#endif
            Application.Quit();
        }
        else if (firstArg == "clear")
        {
            inputField.GetComponent<CommandLineInputField>().Clear();
        }
        //Send a command to the Execute() of a specific module. Example: "Time Help"
        else if (args.Length > 1 && moduleNames.Contains(firstArg)) 
        {
            SendCommandTo(firstArg, "Execute", args);
        }
        //Send a command to the Execute() of ALL modules. Example: "TimeScale 0"
        else
        {
            SendCommandToAllModules("Execute", args);
        }
    }

    public void ShowHelp(string[] args)
    {
        if (args.Length > 1)
        {
            for (int i = 0; i < commandLineModules.Length; i++)
            {
                if (moduleNames.Contains(args[1].ToLower()))
                {
                    SendCommandTo(args[1].ToLower(), "Help");
                    i = commandLineModules.Length;
                }                
            }
        }
        else
        {
            PrintOnCLI("Enter 'help nameOfTheModule' to see what each module can do. To list all modules available, enter 'm' or 'modules'.");
            PrintOnCLI("Core Commands: help (or h), modules (or m), hide (or close), exit, clear");
        }
    }

    public void ShowModulesLoaded()
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < moduleNames.Count; i++)
        {
            builder.Append(moduleNames[i]);

            if (i < moduleNames.Count - 1)
            {
                builder.Append(", ");
            }
            else
            {
                builder.Append(".");
            }
        }

        PrintOnCLI("Modules loaded: " + builder.ToString());
    }

    public void SendCommandToAllModules(string command, string[] args)
    {
        string[] modifiedArgs = new string[args.Length + 1];

        modifiedArgs[0] = "";
        for (int i = 1; i < modifiedArgs.Length; i++)
        {
            modifiedArgs[i] = args[i - 1];
        }

        for (int i = 0; i < commandLineModules.Length; i++)
        {
            commandLineModules[i].SendMessage(command, modifiedArgs);
        }
    }

    public void SendCommandTo(string moduleName, string command)
    {
        for (int i = 0; i < commandLineModules.Length; i++)
        {
            if (moduleNames[i] == moduleName)
            {
                commandLineModules[i].SendMessage(command);
                i = commandLineModules.Length;
            }
        }
    }

    public void SendCommandTo(string moduleName, string command, string[] args)
    {
        for (int i = 0; i < commandLineModules.Length; i++)
        {
            if (moduleNames[i] == moduleName)
            {
                commandLineModules[i].SendMessage(command, args);
                i = commandLineModules.Length;
            }
        }
    }

    private void InitiateModules()
    {
        Object[] modules = Resources.LoadAll("Modules");
        commandLineModules = new GameObject[modules.Length];
        moduleSettings = new List<CommandLineModuleSettings>();

        modulesParent = transform.Find("Modules").gameObject;
        inputField = transform.Find("InputField").gameObject;

        for (int i = 0; i < modules.Length; i++)
        {
            commandLineModules[i] = (GameObject)Instantiate(modules[i], modulesParent.transform);
        }

        for (int i = 0; i < commandLineModules.Length; i++)
        {
            moduleSettings.Add(commandLineModules[i].GetComponent<CommandLineModuleSettings>());
            moduleNames.Add(moduleSettings[i].moduleName.ToLower());
        }
    }

    public static void PrintOnCLI(string message)
    {
        GameObject.Find("InputField").GetComponent<CommandLineInputField>().PrintOutputOnView(message);
    }
}
