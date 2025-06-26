using System.Security.AccessControl;

namespace Echo_Service;

public class Echo
{
    
}
public class ConsoleInput : IInput
{
    public string Read()
    {
        return Console.ReadLine();
    }
}

public class ConsoleOutput : IOutput
{
    public void Write(string text)
    {
        Console.WriteLine(text);
    }
}