using System.Text;
using UnityEngine;

public class CommandLineModulePlayerPrefs : MonoBehaviour, ICommandLineModule {
    public void Execute(string[] args)
    {
        try
        {
            switch (args[1].ToLower())
            {
                case "getint":
                    CommandLineCore.Print(PlayerPrefs.GetInt(CommandLineCore.StringWithSpaces(args, 2)).ToString());
                    break;
                case "getfloat":
                    CommandLineCore.Print(PlayerPrefs.GetFloat(CommandLineCore.StringWithSpaces(args, 2)).ToString());
                    break;
                case "getstring":
                    CommandLineCore.Print(PlayerPrefs.GetString(CommandLineCore.StringWithSpaces(args, 2)));
                    break;
                case "setint":
                    PlayerPrefs.SetInt(CommandLineCore.StringWithPipes(args[2]), int.Parse(args[3]));
                    break;
                case "setfloat":
                    PlayerPrefs.SetFloat(CommandLineCore.StringWithPipes(args[2]), float.Parse(args[3]));
                    break;
                case "setstring":
                    PlayerPrefs.SetString(CommandLineCore.StringWithPipes(args[2]), CommandLineCore.StringWithSpaces(args, 3));
                    break;
                case "saveprefs":
                    PlayerPrefs.Save();
                    CommandLineCore.PrintSuccess("PlayerPrefs successfully saved!");
                    break;
                case "deleteallprefs":
                    PlayerPrefs.DeleteAll();
                    CommandLineCore.PrintSuccess("All PlayerPrefs were successfully deleted!");
                    break;
                case "help":
                case "h":
                case "-h":
                    Help();
                    break;
            }
        }
        catch (System.Exception e)
        {
            CommandLineCore.PrintError(e.ToString());
        }
    }

    public void Help()
    {
        StringBuilder helpMessage = new StringBuilder();

        helpMessage.Append("getint string:key\n");
        helpMessage.Append("getfloat string:key\n");
        helpMessage.Append("getstring string:key\n");
        helpMessage.Append("setint string:keyWithPipes int:value\n");
        helpMessage.Append("setfloat string:keyWithPipes float:value\n");
        helpMessage.Append("setstring string:keyWithPipes string:value\n");
        helpMessage.Append("saveprefs\n");
        helpMessage.Append("deleteallprefs");

        CommandLineCore.Print(helpMessage.ToString());
    }
}

