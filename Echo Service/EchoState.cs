using System.Security.Cryptography;

namespace Echo_Service;
using System;
using System.Collections.Generic;
public class EchoState : IState
{
    public void Handle(string inputString,EchoService echoService)
    {
        IOutput output = echoService.Output;
        output.Write( inputString, true);
    }

    public string GetPrompt()
    {
        return "Echo"; 
    }
    public IService.States GetNextState(string inputText)
    {
        if(inputText.Equals("logout",StringComparison.CurrentCultureIgnoreCase))
        {
            return IService.States.AUTH;
        }

        if (inputText.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
        {
            return IService.States.EXIT;
        }

        if (inputText.Equals("token", StringComparison.CurrentCultureIgnoreCase))
        {
            return IService.States.TOKEN; 
        }

        return IService.States.ECHO; 
    }
}

public class AuthenticatorState : IState
{
    private bool _authenticated = false;
    private string _username;
    public void Handle(string inputString,EchoService echoService)
    {
        _authenticated = false;
        IOutput output = echoService.Output;
        if (_username == null)
        {
            _username = inputString;
        }
        else
        {
            if (echoService.GetAuthenticator().CheckPassword(_username, inputString))
            {
                _authenticated = true;
            }

            _username = null; 
        }

    }

    public string GetPrompt()
    {
        if (_username == null)
        {
            return "User";
        }
        else
        {
            return "Pwd"; 
        }
    }

    public IService.States GetNextState(string inputText)
    {
        return _authenticated ? IService.States.ECHO : IService.States.AUTH;
    }
}


public class ExitState : IState
{
    public void Handle(string inputText,EchoService echoService)
    {
    }
    public IService.States GetNextState(string inputText)
    {
        return IService.States.EXIT;
    }

    public string GetPrompt()
    {
        return "Exit";
    }
}
