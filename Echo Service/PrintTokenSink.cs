namespace Echo_Service;

public class PrintTokenSink : ITokenSink
{
    private List<Token> _tokens = new List<Token>();
    public void Accept(Token token)
    {
        _tokens.Add(token);
   
    }

    public List<Token> GetTokens( EchoService echoService)
    {
        foreach (var token in _tokens)
        {
            IOutput output = echoService.Output;
            output.Write(token.TokenText, true);
        }
        return _tokens;
    }

    public void ClearTokens()
    {
        _tokens.Clear();
    }
}



