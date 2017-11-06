using System.Text;
using UnityEngine;

public class CommandLineModulePlayerPrefs : MonoBehaviour, ICommandLineModule {
    public void Execute(string[] args)
    {
        switch (args[1].ToLower()) {
            case "getint":
                CommandLineCore.Print(PlayerPrefs.GetInt(args[2]).ToString());
                break;
            case "getfloat":
                CommandLineCore.Print(PlayerPrefs.GetFloat(args[2]).ToString());
                break;
            case "getstring":
                CommandLineCore.Print(PlayerPrefs.GetString(args[2]));
                break;
            case "setint":
                PlayerPrefs.SetInt(args[2], int.Parse(args[3]));
                break;
            case "setfloat":
                PlayerPrefs.SetFloat(args[2], float.Parse(args[3]));
                break;
            case "setstring":
                PlayerPrefs.SetString(args[2], args[3]);
                break;
            case "saveprefs":
                PlayerPrefs.Save();
                break;
            case "deleteallprefs":
                PlayerPrefs.DeleteAll();
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

        helpMessage.Append("getint string:key\n");
        helpMessage.Append("getfloat string:key\n");
        helpMessage.Append("getstring string:key\n");
        helpMessage.Append("setint string:key int:value\n");
        helpMessage.Append("setfloat string:key float:value\n");
        helpMessage.Append("setstring string:key string:value\n");
        helpMessage.Append("saveprefs\n");
        helpMessage.Append("deleteallprefs");

        CommandLineCore.Print(helpMessage.ToString());
    }
}

