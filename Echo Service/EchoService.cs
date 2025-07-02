using System.Security.AccessControl;

namespace Echo_Service;

public class EchoService:IService
{

    private Dictionary<IService.States, IState> _states;
    private IState _currentState;
    private Authenticator _authenticator;

    public IInput Input {get;}
    public IOutput Output{get;}
    private List<ITokenSink> _sinks; 
    public Authenticator GetAuthenticator()
    {
        return _authenticator;
    }
    public void InitService()
    {
        InitStates();
        _authenticator = new Authenticator();
        _authenticator.AddUser("user1", "password1");
        _authenticator.AddUser("user2", "password2");
        _currentState =_states[IService.States.AUTH];
    }

    
    public EchoService( IInput input, IOutput output, List<ITokenSink> sinks) 
    {
        this.Input = input;
        this.Output = output;
        this._sinks = sinks;
        this._states = new Dictionary<IService.States, IState>();
    }
  public void InitStates()
    {
        _states.Add(IService.States.AUTH, new AuthenticatorState());
        _states.Add(IService.States.ECHO, new EchoState());
        _states.Add(IService.States.TOKEN, new LexerState(_sinks));
        _states.Add(IService.States.EXIT, new ExitState());
    }

    private void PrintPrompt(IState state)
    {
        Output.Write(state.GetPrompt()+ ">", false);
    }
    public void StartService()
    {
        Output.Write("Echo Service", true);
        while (_currentState is not ExitState)
        {   
           PrintPrompt(_currentState);
            string inputText = Input.Read();
            _currentState.Handle(inputText, this);
            SetState(inputText);
        }
    }

    private void SetState(string inputText)
    {
        _currentState = _states[_currentState.GetNextState(inputText)];
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