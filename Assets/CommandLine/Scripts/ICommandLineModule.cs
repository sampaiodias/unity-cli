/// <summary>
/// The interface which all CLIU modules have to implement.
/// </summary>
public interface ICommandLineModule
{
    void Execute(string[] args);
    void Help();
}
