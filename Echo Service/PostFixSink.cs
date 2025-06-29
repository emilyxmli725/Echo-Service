namespace Echo_Service;

public class PostFixSink : ITokenSink
{
    private List<Token> _tokens = new List<Token>();
    private Stack<Token> _operatorStack = new Stack<Token>();

    private int Precedence(Token token)
    {
        if (token.TokenText == "+" || token.TokenText == "-")
        {
            return 1;
        }

        if (token.TokenText == "*" || token.TokenText == "/")
        {
            return 2;
        }
        else return 0; 
    }
    public void Accept(Token token)
    {
        switch (token.Type)
        {
            case TokenType.Number:
            case TokenType.Identifier:
                _tokens.Add(token);
                break;
            case TokenType.Operator:
                while (_operatorStack.Count > 0 && Precedence(_operatorStack.Peek()) >= Precedence(token))
                {
                    _tokens.Add(_operatorStack.Pop());
                }
                _operatorStack.Push(token);
                break;
            case TokenType.LeftParenthesis:
                _operatorStack.Push(token);
                break;
            case TokenType.RightParenthesis:
                while (_operatorStack.Count > 0 && _operatorStack.Peek().Type != TokenType.LeftParenthesis)
                {
                    _tokens.Add(_operatorStack.Pop());
                }
                if (_operatorStack.Count > 0 && _operatorStack.Peek().Type == TokenType.LeftParenthesis)
                {
                    _operatorStack.Pop(); 
                }
                break;
        }
    }

    public List<Token> GetTokens(EchoService echoService)
    {
        IOutput output = echoService.Output;
        /*output.Write("PostFix List:" ,true);*/
        while (_operatorStack.Count > 0)
        {
            _tokens.Add(_operatorStack.Pop());
        }

        /*
        foreach (var token in _tokens)
        {
            output.Write(token.TokenText + " ", false);
            output.Write( " ", true);
        }
        */
        
        return _tokens;
    }
    public void ClearTokens()
    {
        _tokens.Clear();
    }
}