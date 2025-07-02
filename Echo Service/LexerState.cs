using System.Numerics;

namespace Echo_Service;

public class LexerState :IState
{
    private enum ExpectedToken
    {
        ExpectingOperand,
        ExpectingOperator,
        Error
    }
    private List<ITokenSink> _tokenSinks;

    public LexerState(List<ITokenSink> tokenSinks)
    {
        _tokenSinks = tokenSinks;
    }
    
    public void Handle(string inputText,EchoService echoService)
    {
        List<Token> tokens = Tokenize(inputText, echoService);
        if (null == tokens)
        {
            return ;
        }
        foreach (var token in tokens)
        {

            foreach (var sink in _tokenSinks)
            {
                sink.Accept(token);
            }
        }
        foreach (var sink in _tokenSinks)
        {
       
                sink.GetTokens(echoService);
           
        }

        foreach (var sink in _tokenSinks)
        {
            sink.ClearTokens();
        }
        
    }

    public string GetPrompt()
    {
        return "Token";
    }

    public IService.States GetNextState(string inputText)
    {
       if(inputText.Equals("logout",StringComparison.CurrentCultureIgnoreCase))
       {
           return IService.States.AUTH;
       }

       if (inputText.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
       {
           return IService.States.EXIT;
       }

       if (inputText.Equals("echo", StringComparison.CurrentCultureIgnoreCase))
       {
           return IService.States.ECHO; 
       }

       return IService.States.TOKEN; 
    }


    private List<Token> Tokenize(string inputText, EchoService echoService)
    {
        List<Token> tokens = new List<Token>();
        int parenthesisCounter = 0;
        int i = 0;
        ExpectedToken currentExpected = ExpectedToken.ExpectingOperand; 
        while (i < inputText.Length)
        {
            
            Token token = null;
            if (currentExpected == ExpectedToken.ExpectingOperand)
            {
                if(IsWhitespace(inputText[i]))
                {
                    i++;
                    continue;
                }

                if (IsNumber(inputText[i]))
                {
                    int start = i;
                    while (i < inputText.Length && IsNumber(inputText[i]))
                    {
                        i++;
                    }
                    string value = inputText.Substring(start, i - start);
                    token = new Token(TokenType.Number, value, start);
                    tokens.Add(token);        
                    currentExpected = ExpectedToken.ExpectingOperator;
                    continue;
                }
                else if (IsIdentifier(inputText[i]))
                {
                    int start = i;
                    while (i < inputText.Length && (IsIdentifier(inputText[i]) || IsNumber(inputText[i])))
                    {
                        i++;
                    }
                    string value = inputText.Substring(start, i - start);
                    token = (new Token(TokenType.Identifier, value, start));
                    tokens.Add(token);  
                    currentExpected = ExpectedToken.ExpectingOperator;
                    continue;
                    
                }
                else if ('(' == inputText[i])
                {
                    token = (new Token(TokenType.LeftParenthesis, inputText.Substring(i, 1), i));
                    parenthesisCounter++;
                }
                else if (')' == inputText[i])
                {
                    if (parenthesisCounter >= 0)
                    {
                        token = (new Token(TokenType.RightParenthesis, inputText.Substring(i, 1), i));
                        parenthesisCounter--;
                    }
                    else
                    {
                        echoService.Output.Write("Error: Too many closing brackets at position" + i, true);
                        currentExpected = ExpectedToken.Error;
                    }
                    
                }
                else if ('+' == inputText[i] || '-' == inputText[i])
                {
                    token = (new Token(TokenType.Operator, inputText.Substring(i, 1), i));
                }
                else
                {
                    echoService.Output.Write("Error: Expected an operand at position:" + i, true);
                    currentExpected = ExpectedToken.Error;
                }
            }
            else if (currentExpected == ExpectedToken.ExpectingOperator)
            {
                if (IsOperator(inputText[i]))
                {
                    token = (new Token(TokenType.Operator, inputText.Substring(i, 1), i));
                    currentExpected = ExpectedToken.ExpectingOperand;
                }
               
                else if (')' == inputText[i])
                {
                    if (parenthesisCounter >= 0)
                    {
                        token = (new Token(TokenType.RightParenthesis, inputText.Substring(i, 1), i));
                        parenthesisCounter--;
                    }
                    else
                    {
                        echoService.Output.Write("Error: Too many closing brackets at position" + i, true);
                        currentExpected = ExpectedToken.Error;
                    }
                    
                }
                else
                {
                    echoService.Output.Write("Error: Expected an operator at position:" + i, true);
                    currentExpected = ExpectedToken.Error;
                }
            }
            else if (currentExpected == ExpectedToken.Error)
            {
                break;
            }
            tokens.Add(token);
            i++; 
        }
        
        return currentExpected == ExpectedToken.Error ? null : tokens;
    }
    
    private bool IsWhitespace(char c)
    {
        return c == ' ' || c == '\t' || c == '\n' || c == '\r';
    }

    private bool IsNumber(char c)
    {
        return c >= '0' && c <= '9';
    }

    private bool IsIdentifier(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    }

    private bool IsOperator(char c)
    {
        return '+' == c || c == '-' || c == '*' || c == '/';
    }
}
