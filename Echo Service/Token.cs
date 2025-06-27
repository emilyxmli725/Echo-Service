namespace Echo_Service;

public class Token
{
    public enum TokenType
    {
        Identifier,
        Number,
        Operator,
        Unkown
    }
    public TokenType Type { get; set; }
    public string TokenText { get; set; }

    public override string ToString()
    {
        return (" TokenType" + Type + TokenText);
    }
}