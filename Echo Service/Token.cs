namespace Echo_Service;

public enum TokenType
{
    Identifier,
    Number,
    Operator,
    LeftParenthesis,
    RightParenthesis
}
public class Token
{

    public TokenType Type { get; set; }
    public string TokenText { get; set; }

    public override string ToString()
    {
        return (" TokenType" + Type + TokenText);
    }
}