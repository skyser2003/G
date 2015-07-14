using System.Reflection;
using System.Runtime.Serialization;

class NetworkClient
{
    private DummyServer ds;

    public void Init(DummyServer ds)
    {
        this.ds = ds;
    }

    public void Send<PKS>(PKS pks)
    {
        ds.Receive(pks);
    }

    public void Receive<PKS>(PKS pks)
    {
        var methodInfos = GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
        foreach (var method in methodInfos)
        {
            if (method.Name == "OnPacketReceive")
            {
                if (method.GetParameters()[0].GetType() == typeof(PKS))
                {
                    method.Invoke(this, new object[] { pks });
                }
            }
        }
    }

    public void OnPacketReceive(PKS_ENTER pks)
    {
        var user = new User();
        user.ID = pks.name;

        UserList.Inst.Add(user);
    }
}