using UnityEngine;
using UnityEditor;

public class CommandLineHotkey : ScriptableObject {

    static GameObject cli;

	[MenuItem("Window/Command Line/Create CLI #_0")]
    static void OpenCLI()
    {
        if (cli == null)
        {
            cli = Instantiate(Resources.Load("CLI")) as GameObject;
        }
        else
        {
            Destroy(cli);
            cli = null;
        }
    }
}
