using System.Collections.Generic;

class UserList
{
    static private UserList inst = new UserList();
    static public UserList Inst { get { return inst; } }

    private UserList()
    {

    }

    private Dictionary<string, User> userList;

    public void Init()
    {
        userList = new Dictionary<string, User>();
    }

    public void Add(User user)
    {
        userList.Add(user.ID, user);
    }

    public void Remove(string id)
    {
        userList.Remove(id);
    }

    public void Remove(User user)
    {
        userList.Remove(user.ID);
    }
}