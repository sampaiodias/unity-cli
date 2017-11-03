using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CommandLineCore : MonoBehaviour {

    [Header("Basic Settings")]
    [Tooltip("Leave empty if you don't want a hotkey")]
    public string openWindowHotkey = "]";
    [Tooltip("Leave empty if you don't want a hotkey")]
    public string closeWindowHotkey = "escape";
    [Tooltip("If enabled, the CLIU Window will automatically close when the game starts")]
    public bool startHidden = true;
    [Tooltip("If enabled, the 'Open CLIU' button will never appear on the screen")]
    public bool hideOpenWindowButton = false;
    [Tooltip("If enabled, you'll be able to drag the CLIU Window around by holding the white bar")]
    public bool draggableWindow = true;
    [Header("Advanced Settings")]
    [Tooltip("If disabled, loading other scenes will NOT destroy the CLIU gameObject")]
    public bool destroyOnSceneLoad = false;

    [HideInInspector]
    public Vector3 initPos;

    private GameObject modulesParent;
    private GameObject window;
    private CommandLineInputField inputField;
    private GameObject[] commandLineModules;
    private List<CommandLineModuleSettings> moduleSettings;
    private List<string> moduleNames = new List<string>();
    private GameObject buttonOpenWindow;
    private CommandLineWindowManager windowManager;    

    private void Start()
    {
        InitiateCore();
        InitiateModules();
        SettingBasedProcedures();
    }

    /// <summary>
    /// This is the Core main method. Core interpret args and execute the appropriate operations.
    /// </summary>
    /// <param name="args">Get the command on args[0] and its (optional) parameters on args[1], args[2], and so on. Example: { "call", "PlayerGameObject", "RespawnMethod" } OR { "help", "time" }</param>
    public void RunCommand(string[] args)
    {
        string firstArg = args[0].ToLower();

        //Search for a match on the Core commands. Don't name your custom commands with these names (like "hide" or "exit")
        if (firstArg == "help" || firstArg == "h" || firstArg == "-h")
        {
            ShowHelp(args);            
        }
        else if (firstArg == "modules" || firstArg == "m")
        {
            ShowModulesLoaded();
        }
        else if (firstArg == "hide" || firstArg == "close")
        {
            windowManager.CloseCLIUWindow();
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
            inputField.Clear();
        }
        else if (firstArg == "reset")
        {
            window.transform.position = initPos;
        }

        //If it isn't a Core command, Core sends a command to the Execute() of a specific module. Example: "time help"
        else if (args.Length > 1 && moduleNames.Contains(firstArg)) 
        {
            SendCommandTo(firstArg, "Execute", args);
        }

        //If everything above fails, Core sends a command to the Execute() of ALL modules. Example: "timescale 0"
        else
        {
            SendCommandToAllModules("Execute", args);
        }
    }

    /// <summary>
    /// Call Help() from a specific module or, if the module is not found or specified, the help message of Core.
    /// </summary>
    /// <param name="args"></param>
    public void ShowHelp(string[] args)
    {
        if (args.Length > 1)
        {
            for (int i = 0; i < commandLineModules.Length; i++)
            {
                if (moduleNames.Contains(args[1].ToLower()))
                {
                    SendCommandTo(args[1].ToLower(), "Help", args);
                    i = commandLineModules.Length;
                }                
            }
        }
        else
        {
            Print("Enter 'help codeofthemodule' to see what each module can do. To list all module codes, enter 'm' or 'modules'.");
            Print("Core Commands: help (or h), modules (or m), hide (or close), exit, clear, reset");
        }
    }    

    /// <summary>
    /// Print a message on the CLIU Window.
    /// </summary>
    /// <param name="message"></param>
    public static void Print(string message)
    {
        GameObject.Find("CLIU-InputField").GetComponent<CommandLineInputField>().PrintOutputOnView(message);
    }

    /// <summary>
    /// Print a message on the CLIU Window using a specified color.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="colorTag">The color tag for the Rich Text tag. Example: #00800ff</param>
    public static void Print(string message, string colorTag)
    {
        GameObject.Find("CLIU-InputField").GetComponent<CommandLineInputField>().PrintColoredOutputOnView(message, colorTag);
    }

    /// <summary>
    /// Print a message on the CLIU Window using a red color.
    /// </summary>
    /// <param name="message"></param>
    public static void PrintError(string message)
    {
        Print(message, "#ff0000ff");
    }

    /// <summary>
    /// Print a message on the CLIU Window using a yellow color.
    /// </summary>
    /// <param name="message"></param>
    public static void PrintWarning(string message)
    {
        Print(message, "#ffff00ff");
    }

    /// <summary>
    /// Print a message on the CLIU Window using a green color.
    /// </summary>
    /// <param name="message"></param>
    public static void PrintSuccess(string message)
    {
        Print(message, "#008000ff");
    }

    private void ShowModulesLoaded()
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

        Print("Modules loaded: " + builder.ToString());
    }

    private void SendCommandToAllModules(string command, string[] args)
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

    private void SendCommandTo(string moduleName, string command, string[] args)
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

    private void InitiateCore()
    {
        buttonOpenWindow = GameObject.Find("CLIU-OpenCLIUButton");
        window = GameObject.Find("CLIU-Window").gameObject;
        windowManager = FindObjectOfType<CommandLineWindowManager>();
        inputField = FindObjectOfType<CommandLineInputField>();
    }

    private void InitiateModules()
    {
        Object[] modules = Resources.LoadAll("Modules");
        commandLineModules = new GameObject[modules.Length];
        moduleSettings = new List<CommandLineModuleSettings>();
        modulesParent = transform.Find("CLIU-Modules").gameObject;

        for (int i = 0; i < modules.Length; i++)
        {
            commandLineModules[i] = (GameObject)Instantiate(modules[i], modulesParent.transform);
        }

        for (int i = 0; i < commandLineModules.Length; i++)
        {
            moduleSettings.Add(commandLineModules[i].GetComponent<CommandLineModuleSettings>());
            moduleNames.Add(moduleSettings[i].moduleInternalCode.ToLower());
        }
    }

    private void SettingBasedProcedures()
    {
        if (destroyOnSceneLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
        if (startHidden)
        {
            window.SetActive(false);
            if (hideOpenWindowButton)
            {
                buttonOpenWindow.SetActive(false);
            }
        }
        else
        {
            buttonOpenWindow.SetActive(false);
        }
    }
}
