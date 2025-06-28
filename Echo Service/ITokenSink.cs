namespace Echo_Service;

public interface ITokenSink
{
    void Accept(Token token);
    List<Token> GetTokens(EchoService echoService);
}