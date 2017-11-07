using System.Text;
using UnityEngine;

public class CommandLineModuleTransform : MonoBehaviour, ICommandLineModule {
    public void Execute(string[] args)
    {
        switch (args[1].ToLower()) {
            case "position":
            case "setposition":
                GameObject.Find(CommandLineCore.StringWithSpaces(args, 5)).transform.position = new Vector3(float.Parse(args[2]), float.Parse(args[3]), float.Parse(args[4]));
                break;
            case "move":
                GameObject.Find(CommandLineCore.StringWithSpaces(args, 5)).transform.position += new Vector3(float.Parse(args[2]), float.Parse(args[3]), float.Parse(args[4]));
                break;
            case "rotation":
            case "setrotation":
                GameObject.Find(CommandLineCore.StringWithSpaces(args, 5)).transform.eulerAngles = new Vector3(float.Parse(args[2]), float.Parse(args[3]), float.Parse(args[4]));
                break;
            case "rotate":
                GameObject.Find(CommandLineCore.StringWithSpaces(args, 5)).transform.Rotate(float.Parse(args[2]), float.Parse(args[3]), float.Parse(args[4]));
                break;
            case "scale":
            case "setscale":
                GameObject.Find(CommandLineCore.StringWithSpaces(args, 5)).transform.localScale = new Vector3(float.Parse(args[2]), float.Parse(args[3]), float.Parse(args[4]));
                break;
            case "addscale":
                GameObject.Find(CommandLineCore.StringWithSpaces(args, 5)).transform.localScale += new Vector3(float.Parse(args[2]), float.Parse(args[3]), float.Parse(args[4]));
                break;
            case "getposition":
                CommandLineCore.Print(GameObject.Find(CommandLineCore.StringWithSpaces(args, 2)).transform.position.ToString());
                break;
            case "getrotation":
                CommandLineCore.Print(GameObject.Find(CommandLineCore.StringWithSpaces(args, 2)).transform.rotation.ToString());
                break;
            case "getscale":
                CommandLineCore.Print(GameObject.Find(CommandLineCore.StringWithSpaces(args, 2)).transform.localScale.ToString());
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

        helpMessage.Append("position name float:x float:y float:z string:gameObjectName\n");
        helpMessage.Append("move float:x float:y float:z string:gameObjectName\n");
        helpMessage.Append("rotation float:x float:y float:z string:gameObjectName\n");
        helpMessage.Append("rotate name float:x float:y float:z string:gameObjectName\n");
        helpMessage.Append("scale float:x float:y float:z string:gameObjectName\n");
        helpMessage.Append("addscale float:x float:y float:z string:gameObjectName\n");
        helpMessage.Append("getposition string:gameObjectName\n");
        helpMessage.Append("getrotation string:gameObjectName\n");
        helpMessage.Append("getscale string:gameObjectName");

        CommandLineCore.Print(helpMessage.ToString());
    }
}

