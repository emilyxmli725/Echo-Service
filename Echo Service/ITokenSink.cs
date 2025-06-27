namespace Echo_Service;

public interface ITokenSink
{
    void Accept(Token token, EchoService echoService);
}