using System.Collections.Generic;
using System.Runtime.Serialization;

class DummyServer
{
    static private DummyServer inst = new DummyServer();
    static public DummyServer Inst { get { return inst; } }

    private List<NetworkClient> userList = new List<NetworkClient>();

    public void Init()
    {

    }

    public void AddClient(NetworkClient user)
    {
        userList.Add(user);
    }

    public void Receive<PKS>(PKS pks)
    {
        foreach (var user in userList)
        {
            user.Receive(pks);
        }
    }
}