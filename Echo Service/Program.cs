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
        EchoService echoService = new EchoService(input, output);
        echoService.StartEchoService();
    }
}

