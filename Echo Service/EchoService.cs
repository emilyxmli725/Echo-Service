using System.Security.AccessControl;

namespace Echo_Service;

public class EchoService:IService
{
    public enum States
    {
        AUTH,
        ECHO, 
        TOKEN, 
        EXIT
        
    }

    private Dictionary<States, IState> _states;
    private IState _currentState; 
    public IInput Input {get;}
    public IOutput Output{get;}
    private List<ITokenSink> _sinks; 

    public void InitService()
    {
        Authenticator authenticator = new Authenticator();
        authenticator.AddUser("user1", "password1");
        authenticator.AddUser("user2", "password2");
        _currentState =_states[States.AUTH];
    }

    
    public EchoService( IInput input, IOutput output, List<ITokenSink> sinks) 
    {
        this.Input = input;
        this.Output = output;
        this._sinks = sinks;
        this._states = new Dictionary<States, IState>();
    }
  public void InitStates()
    {
        _states.Add(States.AUTH, new AuthenticatorState());
        _states.Add(States.ECHO, new EchoState());
        _states.Add(States.TOKEN, new LexerState(_sinks));
        _states.Add(States.EXIT, new ExitState());
    }

    private void PrintPrompt(IState state)
    {
        Output.Write(state.GetPrompt(), false);
    }
    public void StartService()
    {
        Output.Write("Echo Service", true);
        while (_currentState is not ExitState)
        {   
           PrintPrompt(_currentState);
            string inputText = Input.Read();
            switch (inputText)
            {
                case "exit":
                    _currentState = new ExitState();
                    break;
                case "logout":
                    _currentState = new AuthenticatorState();
                    break;
                case "token":
                    _currentState = new LexerState(_sinks);
                    break;
                case "echo":
                    _currentState = new EchoState();
                    break;
                default:
                    _currentState = _currentState.Handle(inputText, this);
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