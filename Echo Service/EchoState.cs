using System.Security.Cryptography;

namespace Echo_Service;
using System;
using System.Collections.Generic;
public class EchoState : IState
{
    public IState Handle(string inputString, IInput input, IOutput output)
    {
        output.Write( inputString, true);
        return this; 
    }
}

public class AuthenticatorState : IState
{
    public IState Handle(string inputString, IInput input, IOutput output)
    {
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
    public IState Handle(string inputText, IInput input, IOutput output)
    {
        return this; 
    }
}
