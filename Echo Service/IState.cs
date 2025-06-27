namespace Echo_Service;
using System;
using System.Collections.Generic;
public interface IState
{
    IState Handle(string inputText, EchoService echoService); 

}

public interface IInput
{ 
    string Read();
}
public interface IOutput
{
    void Write(string input, bool addNewLine);
}
