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
}

public class AuthenticatorState : IState
{
    public IState Handle(string inputString,EchoService echoService)
    {
        IOutput output = echoService.Output;
        IInput input = echoService.Input;
        Authenticator authenticator = new Authenticator();
        if (authenticator.CheckUserName(inputString))
        {
            output.Write("Password>",false);
            string inputPassword = input.Read();
            if (authenticator.CheckPassword(inputString, inputPassword))
            {
                return new EchoState();
            }
        }
        return this; 
    }
}


public class ExitState : IState
{
    public IState Handle(string inputText,EchoService echoService)
    {
        return this; 
    }
}
