using System.Security.AccessControl;

namespace Echo_Service;

public class EchoService
{
    private IState _state; 
    public IInput Input {get; set;}
    public IOutput Output{get; set;}
    private List<ITokenSink> _sinks; 

    public EchoService(IState startingState, IInput input, IOutput output, List<ITokenSink> sinks)
    {
        _state = startingState; 
        this.Input = input;
        this.Output = output;
        this._sinks = sinks;
    }

    public void StartEchoService()
    {
        Output.Write("Echo Service", true);
        while (_state is not ExitState)
        {
            switch (_state)
            {
                case AuthenticatorState authenticatorState:
                    Output.Write("User>", false);
                    break;
                case EchoState echoState:
                    Output.Write("Echo>", false);
                    break;
                case ExitState exitState:
                    Output.Write("Exit Program", false);
                    break;
                case LexerState lexerState:
                    Output.Write("Token>", false);
                    break;
            }

            string inputText = Input.Read();
            switch (inputText)
            {
                case "exit":
                    _state = new ExitState();
                    break;
                case "logout":
                    _state = new AuthenticatorState();
                    break;
                case "token":
                    _state = new LexerState(_sinks);
                    break;
                case "echo":
                    _state = new EchoState();
                    break;
                default:
                    _state = _state.Handle(inputText, this);
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