namespace Echo_Service;

public interface ITokenSink
{
    public void Accept(Token token);
    public List<Token> GetTokens(EchoService echoService);
    public void ClearTokens();
}