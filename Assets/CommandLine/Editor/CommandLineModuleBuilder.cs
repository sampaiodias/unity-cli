﻿using UnityEngine;
using UnityEditor;
using System.IO;

public class CommandLineModuleBuilder : EditorWindow {

    string moduleName = "ModuleName"; //Prefab Name
    string moduleCode = "example";
    string moduleScriptName = "CommandLineModuleExampleName";
    Texture2D logo;

    [MenuItem("Tools/CLIU/Module Builder")]
    public static void OpenModuleBuilder()
    {
        CommandLineModuleBuilder window = GetWindow<CommandLineModuleBuilder>("Module Builder");
        window.minSize = new Vector2(415, 440);
    }

    private void OnEnable()
    {
        logo = (Texture2D)Resources.Load("Textures/CLIU-Logo", typeof(Texture2D));
    }

    void OnGUI()
    {
        GUILayout.Label(logo);
        GUILayout.Label("Prefab Builder", EditorStyles.largeLabel);
        GUILayout.Space(10);
        moduleName = EditorGUILayout.TextField("Module Name:", moduleName);
        moduleCode = EditorGUILayout.TextField("Internal Code:", moduleCode);
        GUILayout.Space(10);
        if (GUILayout.Button("Build Prefab"))
        {
            BuildModule();
        }
        GUILayout.Space(20);
        GUILayout.Label("Module's Script Builder", EditorStyles.largeLabel);
        GUILayout.Space(10);
        moduleScriptName = EditorGUILayout.TextField("Script Name:", moduleScriptName);
        if (GUILayout.Button("Build Script"))
        {
            BuildModuleClass();
        }
        GUILayout.Space(20);
        EditorGUILayout.HelpBox("Remember to attach the script created to the prefab of your module!", MessageType.Info);
        //GUILayout.Label("Remember to attach the script created to the prefab of your module!", EditorStyles.centeredGreyMiniLabel);
        GUILayout.Space(10);
        GUILayout.Label("Made by Lucas Sampaio Dias\n(lucassampaiodias@gmail.com)", EditorStyles.centeredGreyMiniLabel);
    }

    void BuildModule()
    {
        GameObject module = new GameObject(moduleName);
        CommandLineModuleSettings settings = module.AddComponent<CommandLineModuleSettings>();
        settings.moduleInternalCode = moduleCode;
        PrefabUtility.CreatePrefab("Assets/CommandLine/Resources/Modules/" + moduleName + ".prefab", module);
        DestroyImmediate(module);
        Debug.Log("Module prefab built successfully at \"/CommandLine/Resources/Modules/\"");
    }

    string BuildModuleClass()
    {
        string name = moduleScriptName;
        name = name.Replace(" ", "_");
        name = name.Replace("-", "_");
        string copyPath = "/CommandLine/Scripts/Modules";

        if(File.Exists(copyPath) == false){
            using (StreamWriter outfile = new StreamWriter("Assets/CommandLine/Scripts/Modules/" + name + ".cs"))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using System.Text;");
                outfile.WriteLine("");
                outfile.WriteLine("public class " + name + " : MonoBehaviour, ICommandLineModule {");
                outfile.WriteLine("    public void Execute(string[] args)");
                outfile.WriteLine("    {");
                outfile.WriteLine("        try");
                outfile.WriteLine("        {");
                outfile.WriteLine("            switch (args[1].ToLower())");
                outfile.WriteLine("            {");
                outfile.WriteLine("                case \"dosomething\":");
                outfile.WriteLine("                    Debug.Log(\"This module is printing \" + args[1]);");
                outfile.WriteLine("                    break;");
                outfile.WriteLine("                case \"dosomethingelse\":");
                outfile.WriteLine("                    Debug.Log(\"This module is printing \" + args[1]);");
                outfile.WriteLine("                    break;");
                outfile.WriteLine("                case \"help\":");
                outfile.WriteLine("                case \"h\":");
                outfile.WriteLine("                case \"-h\":");
                outfile.WriteLine("                    Help();");
                outfile.WriteLine("                    break;");
                outfile.WriteLine("            }");
                outfile.WriteLine("        }");
                outfile.WriteLine("        catch (System.Exception e)");
                outfile.WriteLine("        {");
                outfile.WriteLine("            CommandLineCore.PrintError(e.ToString());");
                outfile.WriteLine("        }");
                outfile.WriteLine("    }");
                outfile.WriteLine("");
                outfile.WriteLine("    public void Help()");
                outfile.WriteLine("    {");
                outfile.WriteLine("        StringBuilder helpMessage = new StringBuilder();");
                outfile.WriteLine("");
                outfile.WriteLine("        helpMessage.Append(\"dosomething string:messageToPrint\\n\");");
                outfile.WriteLine("        helpMessage.Append(\"dosomethingelse\");");
                outfile.WriteLine("");
                outfile.WriteLine("        CommandLineCore.Print(helpMessage.ToString());");
                outfile.WriteLine("    }");
                outfile.WriteLine("}");
                outfile.WriteLine("");
            }
        }
        AssetDatabase.Refresh();
        Debug.Log(name + ".cs created at \"" + copyPath + "\". Be sure to attach it to the prefab of your module!");
        return name;
    }
}
