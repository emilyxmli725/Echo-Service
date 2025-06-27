namespace Echo_Service;

public class Authenticator
{
    private Dictionary<string, string> _users = new Dictionary<string, string>();

    public Authenticator()
    {
        _users.Add("user1", "password1");
        _users.Add("user2", "password2");
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