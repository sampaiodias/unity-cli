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
            case "loadnextscene":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "loadpreviousscene":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                break;
            case "sceneindex":
                CommandLineCore.Print("Current scene index: " + SceneManager.GetActiveScene().buildIndex);
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
        CommandLineCore.Print("loadscene int:sceneIndex\nreloadscene\nloadnextscene\nloadpreviousscene\nsceneindex");
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
