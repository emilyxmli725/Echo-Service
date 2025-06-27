using System.Security.AccessControl;

namespace Echo_Service;

public class EchoService
{
    private IState _state; 
    public IInput input; 
    public IOutput output;

    public EchoService(IInput input, IOutput output)
    {
        _state = new AuthenticatorState(); 
        this.input = input;
        this.output = output;
    }

    public void StartEchoService()
    {
        output.Write("Echo Service", true);
        while (_state is not ExitState)
        {
            switch (_state)
            {
                case AuthenticatorState authenticatorState:
                    output.Write("User>", false);
                    break;
                case EchoState echoState:
                    output.Write("Echo>", false);
                    break;
                case ExitState exitState:
                    output.Write("Exit Program", false);
                    break;
            }

            string inputText = input.Read();
            switch (inputText)
            {
                case "exit":
                    _state = new ExitState();
                    break;
                case "logout":
                    _state = new AuthenticatorState();
                    break;
                default:
                    _state = _state.Handle(inputText, input, output);
                    break;
            }
        }
    }
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
    public void Write(string text, bool addNewLine)
    {
        if (addNewLine)
        {
            Console.WriteLine(text);
            return;
        }
        Console.Write(text);
    }
}