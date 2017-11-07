using System.Text;
using UnityEngine;

public class CommandLineModuleTransform : MonoBehaviour, ICommandLineModule {
    public void Execute(string[] args)
    {
        switch (args[1].ToLower()) {
            case "position":
            case "setposition":
                GameObject.Find(args[2]).transform.position = new Vector3(float.Parse(args[3]), float.Parse(args[4]), float.Parse(args[5]));
                break;
            case "move":
                GameObject.Find(args[2]).transform.position += new Vector3(float.Parse(args[3]), float.Parse(args[4]), float.Parse(args[5]));
                break;
            case "rotation":
            case "setrotation":
                GameObject.Find(args[2]).transform.eulerAngles = new Vector3(float.Parse(args[3]), float.Parse(args[4]), float.Parse(args[5]));
                break;
            case "rotate":
                GameObject.Find(args[2]).transform.Rotate(float.Parse(args[3]), float.Parse(args[4]), float.Parse(args[5]));
                break;
            case "scale":
            case "setscale":
                GameObject.Find(args[2]).transform.localScale = new Vector3(float.Parse(args[3]), float.Parse(args[4]), float.Parse(args[5]));
                break;
            case "addscale":
                GameObject.Find(args[2]).transform.localScale += new Vector3(float.Parse(args[3]), float.Parse(args[4]), float.Parse(args[5]));
                break;
            case "getposition":
                CommandLineCore.Print(GameObject.Find(args[2]).transform.position.ToString());
                break;
            case "getrotation":
                CommandLineCore.Print(GameObject.Find(args[2]).transform.rotation.ToString());
                break;
            case "getscale":
                CommandLineCore.Print(GameObject.Find(args[2]).transform.localScale.ToString());
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

        helpMessage.Append("position string:gameObjectName float:x float:y float:z\n");
        helpMessage.Append("move string:gameObjectName float:x float:y float:z\n");
        helpMessage.Append("rotation string:gameObjectName float:x float:y float:z float:w\n");
        helpMessage.Append("rotate string:gameObjectName float:x float:y float:z\n");
        helpMessage.Append("scale string:gameObjectName float:x float:y float:z\n");
        helpMessage.Append("addscale string:gameObjectName float:x float:y float:z\n");
        helpMessage.Append("getposition string:gameObjectName\n");
        helpMessage.Append("getrotation string:gameObjectName\n");
        helpMessage.Append("getscale string:gameObjectName");

        CommandLineCore.Print(helpMessage.ToString());
    }
}

