using System.Text;
using UnityEngine;

public class CommandLineModuleObjects : MonoBehaviour, ICommandLineModule {

    public void Execute(string[] args)
    {
        switch (args[1].ToLower())
        {
            case "callall":
                GameObject[] allObjectsOnScene = FindObjectsOfType<GameObject>();
                for (int i = 0; i < allObjectsOnScene.Length; i++)
                {
                    try
                    {
                        allObjectsOnScene[i].SendMessage(args[2]);
                    }
                    catch (System.Exception)
                    {
                    }                    
                }                
                break;
            case "call":
                GameObject obj = GameObject.Find(args[2]);
                obj.SendMessage(args[3]);
                break;
            case "calltag":
                GameObject objTag = GameObject.FindGameObjectWithTag(args[2]);
                objTag.SendMessage(args[3]);
                break;
            case "destroy":
                Destroy(GameObject.Find(args[2]));               
                break;
            case "destroytag":
                GameObject[] objsTag = (GameObject.FindGameObjectsWithTag(args[2]));
                foreach (var item in objsTag)
                {
                    Destroy(item);
                }
                break;
            case "instantiate":
                UnityEditor.PrefabUtility.InstantiatePrefab(Resources.Load(args[2]));
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

        helpMessage.Append("call string:gameObjectName string:methodName\n");
        helpMessage.Append("callbytag string:gameObjectTag string:methodName\n");
        helpMessage.Append("callall string:methodName\n");
        helpMessage.Append("destroy string:gameObjectName\n");
        helpMessage.Append("destroytag string:gameObjectsTag\n");
        helpMessage.Append("instantiate string:pathInAResourcesFolder");

        CommandLineCore.Print(helpMessage.ToString());
    }
}
