namespace Echo_Service;

public class CalculateSink : ITokenSink
{
    private PostFixSink _postFixSink;
    private List<Token> _tokens = new List<Token>();

    public CalculateSink(PostFixSink postFixSink)
    {
        _postFixSink = postFixSink;
    }

    public void Accept(Token token)
    {
        _tokens.Add(token);
        _postFixSink.Accept(token);
    }

    public void ClearTokens()
    {
        _postFixSink.ClearTokens();
        _tokens.Clear();
    }

    public List<Token> GetTokens(EchoService echoService)
    
    {
        List<Token> postfixTokens = _postFixSink.GetTokens(echoService);
        
        foreach (var token in postfixTokens)
        {
            echoService.Output.Write(token.TokenText + " ", false);
            echoService.Output.Write( " ", true);
        }
        double result = EvaluatePostFix(postfixTokens);
        echoService.Output.Write($"Result: " + result, true);
        return postfixTokens;
    }

    private double EvaluatePostFix(List<Token> tokens)
    {
        Stack<double> postFixStack = new Stack<double>();
        foreach (var token in tokens)
        {
            switch (token.Type)
            {
                case TokenType.Number:
                    double value = double.Parse(token.TokenText);
                    postFixStack.Push(value);
                    break;
                case TokenType.Operator:
                    if (postFixStack.Count < 2)
                    {
                        throw new Exception("not enough operands");
                    }

                    double right = postFixStack.Pop();
                    double left = postFixStack.Pop();
                    double result = 0;
                    switch (token.TokenText)
                    {
                        case "+": result = left + right; break;
                        case "-": result = left - right; break;
                        case "*": result = left * right; break;
                        case "/":
                        {
                            if (left == 0)
                            {
                                throw new DivideByZeroException();
                            }

                            result = left / right;
                            break;
                        }
                    }

                    postFixStack.Push(result);
                    break;
                default:
                    throw new Exception("unexpected token type");
            }
        }
        return postFixStack.Pop();
    }

}