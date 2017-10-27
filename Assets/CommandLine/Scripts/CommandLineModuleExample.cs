using UnityEngine;

public class CommandLineModuleExample : MonoBehaviour, ICommandLineModule {

    public void Execute(string[] args)
    {
        switch (args[1].ToLower())
        {
            case "dosomething":
                Debug.Log("This module is doing something");
                break;
            case "help":
            case "h":
                Help();
                break;
        }
    }

    public void Help()
    {
        Debug.Log("This is the help message for this module");
    }
}
