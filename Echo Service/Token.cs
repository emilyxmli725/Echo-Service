namespace Echo_Service;

public enum TokenType
{
    Identifier,
    Number,
    Operator,
    LeftParenthesis,
    RightParenthesis,
    Unknown
}
public class Token
{

    public TokenType Type { get; set; }
    public string TokenText { get; set; }
    public int Position {get; set;}

    public Token(TokenType type, string tokenText, int position)
    {
        Type = type;
        TokenText = tokenText;
        Position = position;
    }
    public override string ToString()
    {
        return (" TokenType" + Type + TokenText);
    }
}