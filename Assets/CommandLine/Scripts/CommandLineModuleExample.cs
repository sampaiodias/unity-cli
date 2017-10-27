using UnityEngine;

public class CommandLineModuleExample : MonoBehaviour, ICommandLineModule {

    public void Execute(string[] args)
    {
        switch (args[1].ToLower())
        {
            case "dosomething":
                Debug.Log("This module is printing " + args[1]);
                break;
            case "dosomethingelse":
                Debug.Log("This module is printing " + args[1]);
                break;
            case "help":
            case "h":
                Help();
                break;
        }
    }

    public void Help()
    {
        CommandLineCore.PrintOnCLI("dosomething string:messageToPrint\ndosomethingelse");
    }
}
