﻿// See https://aka.ms/new-console-template for more information
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
        CalculateSink calculateSink = new CalculateSink(postFix);
        List<ITokenSink> tokenSinks = new List<ITokenSink>();
        
        tokenSinks.Add(printTokens);
        //tokenSinks.Add(postFix);
        tokenSinks.Add(calculateSink);
        EchoService echoService = new EchoService(input, output, tokenSinks);
        echoService.InitService();
        echoService.StartService();
    }
}

