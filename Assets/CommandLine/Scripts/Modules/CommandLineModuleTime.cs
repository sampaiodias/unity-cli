using UnityEngine;

public class CommandLineModuleTime : MonoBehaviour, ICommandLineModule {

    public void Execute(string[] args)
    {
        switch (args[1].ToLower())
        {
            case "timescale":
                TimeScale(args[2]);
                break;
            default:
                Help();
                break;
        }
    }

    public void Help()
    {
        Debug.Log("Time module help message");
    }

    public void TimeScale(string amount)
    {
        Time.timeScale = float.Parse(amount);
        Debug.Log("TimeScale set to " + Time.timeScale);
    }
}
