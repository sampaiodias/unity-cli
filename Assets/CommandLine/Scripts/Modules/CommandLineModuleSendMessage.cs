using UnityEngine;

public class CommandLineModuleSendMessage : MonoBehaviour, ICommandLineModule {

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
            case "callTag":
                GameObject objTag = GameObject.FindGameObjectWithTag(args[3]);
                objTag.SendMessage(args[2]);
                break;
            case "help":
            case "h":
                Help();
                break;
        }
    }

    public void Help()
    {
        Debug.Log("Commands: call string:methodName string:gameObjectName; callByTag string:methodName string:gameObjectTag; callAll string:methodName");
    }
}
