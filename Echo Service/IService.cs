namespace Echo_Service;

public interface IService
{
    IInput Input{get;}
    IOutput Output{get;}
    void InitStates();
    void InitService(); 
    void StartService();
}