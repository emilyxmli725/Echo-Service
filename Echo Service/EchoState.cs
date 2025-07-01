using System.Security.Cryptography;

namespace Echo_Service;
using System;
using System.Collections.Generic;
public class EchoState : IState
{
    public IState Handle(string inputString,EchoService echoService)
    {
        IOutput output = echoService.Output;
        output.Write( inputString, true);
        return this; 
    }

    public string GetPrompt()
    {
        return "Echo"; 
    }
}

public class AuthenticatorState : IState
{
    private string _username;
    public IState Handle(string inputString,EchoService echoService)
    {
        IOutput output = echoService.Output;

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
}


public class ExitState : IState
{
    public IState Handle(string inputText,EchoService echoService)
    {
        return this; 
    }

    public string GetPrompt()
    {
        return "Exit";
    }
}
