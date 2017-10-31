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
            case "devicetime":
                DeviceTime();
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
        CommandLineCore.PrintOnCLIU("timescale float:amount\nslowmo float:amount float:duration\ndevicetime");
    }

    public void TimeScale(string amount)
    {
        Time.timeScale = float.Parse(amount);
        CommandLineCore.PrintOnCLIU("TimeScale set to " + Time.timeScale);
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

    public void DeviceTime()
    {
        CommandLineCore.PrintOnCLIU(System.DateTime.Now.ToString());
    }
}
