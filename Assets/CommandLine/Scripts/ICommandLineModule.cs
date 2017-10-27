using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommandLineModule
{
    void Execute(string[] args);
    void Help();
}
