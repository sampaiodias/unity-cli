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
                GameObject.Find(args[2]).transform.rotation = new Quaternion(float.Parse(args[3]), float.Parse(args[4]), float.Parse(args[5]), float.Parse(args[6]));
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
            case "-h":
                Help();
                break;
        }
    }

    public void Help()
    {
        CommandLineCore.Print(
            "position string:gameObjectName float:x float:y float:z\n" +
            "move string:gameObjectName float:x float:y float:z\n" +
            "rotation string:gameObjectName float:x float:y float:z float:w\n" +
            "rotate string:gameObjectName float:x float:y float:z\n" +
            "scale string:gameObjectName float:x float:y float:z\n" +
            "addscale string:gameObjectName float:x float:y float:z\n" +
            "getposition string:gameObjectName\n" +
            "getrotation string:gameObjectName\n" +
            "getscale string:gameObjectName\n");
    }
}

