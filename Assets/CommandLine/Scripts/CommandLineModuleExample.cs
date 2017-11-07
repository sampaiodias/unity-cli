using UnityEngine;
using System.Text;

/// <summary>
/// If for some reason the Module Builder fails to build the script file, copy the contents of this file into a new script file.
/// </summary>
public class CommandLineModuleExample : MonoBehaviour, ICommandLineModule {
    public void Execute(string[] args)
    {
        try
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
                case "-h":
                    Help();
                    break;
            }
        }
        catch (System.Exception e)
        {
            CommandLineCore.PrintError(e.ToString());
        }
    }

    public void Help()
    {
        StringBuilder helpMessage = new StringBuilder();

        helpMessage.Append("dosomething string:messageToPrint\n");
        helpMessage.Append("dosomethingelse");

        CommandLineCore.Print(helpMessage.ToString());
    }
}

