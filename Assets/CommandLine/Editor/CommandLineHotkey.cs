using UnityEngine;
using UnityEditor;

public class CommandLineHotkey : ScriptableObject {

    static GameObject cliuWindow;

	[MenuItem("Window/CLIU/Instantiate or Destroy CLIU &0")]
    static void OpenCLIU()
    {
        if (cliuWindow == null)
        {
            cliuWindow = Instantiate(Resources.Load("CLIU")) as GameObject;
        }
        else
        {
            try
            {
                if (EditorApplication.isPlaying)
                {
                    Destroy(cliuWindow);
                }
                else
                {
                    DestroyImmediate(cliuWindow);
                }
                
                cliuWindow = null;
            }
            catch (System.Exception)
            {
            }
            
        }
    }
}
