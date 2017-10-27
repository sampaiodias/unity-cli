using UnityEngine;
using UnityEditor;

public class CommandLineHotkey : ScriptableObject {

    static GameObject cli;

	[MenuItem("Window/Command Line/Create CLI &0")]
    static void OpenCLI()
    {
        if (cli == null)
        {
            cli = Instantiate(Resources.Load("CLI")) as GameObject;
        }
        else
        {
            try
            {
                if (EditorApplication.isPlaying)
                {
                    Destroy(cli);
                }
                else
                {
                    DestroyImmediate(cli);
                }
                
                cli = null;
            }
            catch (System.Exception)
            {
            }
            
        }
    }
}
