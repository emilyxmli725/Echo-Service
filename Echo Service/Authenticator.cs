namespace Echo_Service;

public class Authenticator
{
    private Dictionary<string, string> _users;
    public Authenticator()
    {
     _users = new Dictionary<string, string>();
    }

    public void AddUser(string username, string password)
    {
        _users.Add(username, password);
        _users[username] = password;
    }

    public Boolean CheckPassword(string authenticatedUsername, string password)
    {
        return _users.ContainsKey(authenticatedUsername) && _users[authenticatedUsername] == password;
    }

    public Boolean CheckUserName(String username)
    {
        foreach (KeyValuePair<string, string> user in _users)
        {
            if (user.Key == username)
            {
                return true;
            }
        }
        return false; 
    }
    
}