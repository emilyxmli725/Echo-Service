namespace Echo_Service;
using System;
using System.Collections.Generic;
public interface IEchoState
{
    void Handle(string inputString,IOutput output); 

}

public interface IInput
{
    string Read();
}
public interface IOutput
{
    void Write(string input);
}
