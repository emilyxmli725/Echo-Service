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
    
    public IState Handle(string inputText,EchoService echoService)
    {
        return null;
    }

    private List<Token> Tokenize(string inputText, EchoService echoService)
    {
        List<Token> tokens = new List<Token>();
        int parenthesisCounter = 0;
        int i = 0;
        ExpectedToken currentExpected = ExpectedToken.ExpectingOperand; 
        while (i < inputText.Length)
        {
            char c = inputText[i];
            Token token = null;
            if (currentExpected == ExpectedToken.ExpectingOperator)
            {
                if(IsWhitespace(c))
                {
                    i++;
                    continue;
                }

                if (IsNumber(c))
                {
                    int start = i;
                    while (i < inputText.Length && IsNumber(c))
                    {
                        i++;
                    }
                    string value = inputText.Substring(start, i - start);
                    token = (new Token(TokenType.Number, value, start));
                    currentExpected = ExpectedToken.ExpectingOperator;
                }
                else if (IsIdentifier(c))
                {
                    int start = i;
                    while (i < inputText.Length && IsIdentifier(c) || IsNumber(c))
                    {
                        i++;
                    }
                    string value = inputText.Substring(start, i - start);
                    token = (new Token(TokenType.Identifier, value, start));
                    currentExpected = ExpectedToken.ExpectingOperator;
                    
                }
                else if ('(' == c)
                {
                    token = (new Token(TokenType.LeftParenthesis, inputText.Substring(i, 1), i));
                    parenthesisCounter++;
                }
                else if ('+' == c || '-' == c)
                {
                    token = (new Token(TokenType.Operator, inputText.Substring(i, 1), i));
                }
                else
                {
                    echoService.Output.Write("Error: Expected an operand at position:" + i, true);
                    currentExpected = ExpectedToken.Error;
                }
            }
            if (currentExpected == ExpectedToken.ExpectingOperator)
            {
                if (IsOperator(c))
                {
                    token = (new Token(TokenType.Operator, inputText.Substring(i, 1), i));
                    currentExpected = ExpectedToken.ExpectingOperand;
                }
               
                else if (')' == c)
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
        return c >= 'a' || c <= 'z' || c >= 'A' || c <= 'Z';
    }

    private bool IsOperator(char c)
    {
        return '+' == c || c == '-' || c == '*' || c == '/';
    }
}
