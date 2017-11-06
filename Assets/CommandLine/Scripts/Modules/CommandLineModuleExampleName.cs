using UnityEngine;
using System.Text;

public class CommandLineModuleExampleName : MonoBehaviour, ICommandLineModule {
    public void Execute(string[] args)
    {
        switch (args[1].ToLower()) {
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

    public void Help()
    {
        StringBuilder helpMessage = new StringBuilder();

        helpMessage.Append("dosomething string:messageToPrint\n");
        helpMessage.Append("dosomethingelse");

        CommandLineCore.Print(helpMessage.ToString());
    }
}

