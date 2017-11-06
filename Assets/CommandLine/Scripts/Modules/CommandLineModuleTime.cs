using System.Collections;
using System.Text;
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
            case "currenttimescale":
            case "currentts":
                CommandLineCore.Print("Current TimeScale: " + Time.timeScale);
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
        StringBuilder helpMessage = new StringBuilder();

        helpMessage.Append("timescale float:amount\n");
        helpMessage.Append("slowmo float:amount float:duration\n");
        helpMessage.Append("devicetime\n");
        helpMessage.Append("currenttimescale");

        CommandLineCore.Print(helpMessage.ToString());
    }

    public void TimeScale(string amount)
    {
        Time.timeScale = float.Parse(amount);
        CommandLineCore.Print("TimeScale set to " + Time.timeScale);
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
        CommandLineCore.Print(System.DateTime.Now.ToString());
    }
}
