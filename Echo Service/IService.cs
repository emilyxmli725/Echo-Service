namespace Echo_Service;

public interface IService
{
    public Authenticator GetAuthenticator();
    public enum States
    {
        AUTH,
        ECHO, 
        TOKEN, 
        EXIT
        
    }
    IInput Input{get;}
    IOutput Output{get;}
    void InitStates();
    void InitService(); 
    void StartService();
}