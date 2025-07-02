namespace Echo_Service;
using System;
using System.Collections.Generic;
public interface IState
{
    void Handle(string inputText, EchoService echoService);
    string GetPrompt();
    IService.States GetNextState(string inputText); 

}

public interface IInput
{ 
    string Read();
}
public interface IOutput
{
    void Write(string input, bool addNewLine);
}
