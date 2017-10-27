using System.Collections;
using UnityEngine;

public class CommandLineModuleTime : MonoBehaviour, ICommandLineModule {

    public void Execute(string[] args)
    {
        switch (args[1].ToLower())
        {
            case "timescale":
                TimeScale(args[2]);
                break;
            case "slowmo":
            case "slomo":
            case "slowmotion":
                SlowMo(args[2], args[3]);
                break;
            case "help":
            case "h":
                Help();
                break;
        }
    }

    public void Help()
    {
        CommandLineCore.PrintOnCLI("timescale float:amount\nslowmo float:amount float:duration");
    }

    public void TimeScale(string amount)
    {
        Time.timeScale = float.Parse(amount);
        CommandLineCore.PrintOnCLI("TimeScale set to " + Time.timeScale);
    }

    public void SlowMo(string amount, string duration)
    {
        StartCoroutine(ActivateSlowMo(float.Parse(amount), float.Parse(duration)));
    }

    IEnumerator ActivateSlowMo(float amount, float duration)
    {
        float previousTimeScale = Time.timeScale;
        Time.timeScale = amount;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = previousTimeScale;
    }
}
