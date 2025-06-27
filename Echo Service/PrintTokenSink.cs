namespace Echo_Service;

public class PrintTokenSink : ITokenSink
{
    public void Accept(Token token, EchoService echoService)
    {
        IOutput output = echoService.Output;
        output.Write(token.TokenText, true);
    }
}

public class PostFixTokenSink : ITokenSink
{
    private List<Token> _outputTokens = new List<Token>();
    private Stack<Token> _operatorStack = new Stack<Token>();
    public void Accept(Token token, EchoService echoService)
    {
        IOutput output = echoService.Output;
        switch (token.Type)
        {
            case TokenType.Number:
            case TokenType.Identifier:
                _outputTokens.Add(token);
                break;
        }   
    }
    
}