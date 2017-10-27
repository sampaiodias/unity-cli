using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandLineModuleScene : MonoBehaviour, ICommandLineModule {

    public void Execute(string[] args)
    {
        switch (args[1].ToLower())
        {
            case "loadscene":
                try
                {
                    int sceneToLoad = int.Parse(args[2]);
                    SceneManager.LoadScene(sceneToLoad);
                }
                catch (System.Exception)
                {
                    SceneManager.LoadScene(args[2]);
                }
                break;
            case "reloadscene":
                ReloadScene();
                break;
            case "help":
            case "h":
                Help();
                break;
        }
    }

    public void Help()
    {
        Debug.Log("Scene module help message not implemented :(");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
