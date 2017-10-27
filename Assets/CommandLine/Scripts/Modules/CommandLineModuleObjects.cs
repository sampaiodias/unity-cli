using UnityEngine;

public class CommandLineModuleObjects : MonoBehaviour, ICommandLineModule {

    public void Execute(string[] args)
    {
        switch (args[1].ToLower())
        {
            case "callall":
                GameObject[] allObjectsOnScene = GameObject.FindObjectsOfType<GameObject>();
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
                GameObject obj = GameObject.Find(args[3]);
                obj.SendMessage(args[2]);
                break;
            case "calltag":
                GameObject objTag = GameObject.FindGameObjectWithTag(args[3]);
                objTag.SendMessage(args[2]);
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
            case "help":
            case "h":
                Help();
                break;
        }
    }

    public void Help()
    {
        CommandLineCore.PrintOnCLI("call string:methodName string:gameObjectName\ncallByTag string:methodName string:gameObjectTag\ncallAll string:methodName\ndestroy string:gameObjectName\ndestroytag string:gameObjectsTag");
    }
}
