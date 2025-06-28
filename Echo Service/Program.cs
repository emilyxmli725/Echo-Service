// See https://aka.ms/new-console-template for more information
namespace Echo_Service;
using System;
using System.Collections.Generic;

class Program
{
    public static void Main(String[] args)
    {
        ConsoleInput input = new ConsoleInput();
        ConsoleOutput output = new ConsoleOutput();
        PrintTokenSink printTokens = new PrintTokenSink();
        PostFixSink postFix = new PostFixSink();
        List<ITokenSink> tokenSinks = new List<ITokenSink>();
        tokenSinks.Add(printTokens);
        tokenSinks.Add(postFix);
        LexerState start = new LexerState(tokenSinks);
  
        EchoService echoService = new EchoService(start, input, output,tokenSinks);
        echoService.StartEchoService();
    }
}

