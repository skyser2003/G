using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;

class NetworkClient : MonoBehaviour
{
    public void Send<PKS>(PKS pks)
    {
        DummyServer.Inst.Receive(pks);
    }

    public void Receive<PKS>(PKS pks)
    {
        var methodInfos = GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
        foreach (var method in methodInfos)
        {
            if (method.Name == "OnPacketReceive")
            {
                if (method.GetParameters()[0].ParameterType == typeof(PKS))
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
        user.Obj = Object.Instantiate(GameObject.Find("Arland_green"));
        Object.Destroy(user.Obj.GetComponent<Keyboard>());

        UserList.Inst.Add(user);
    }

    private void Awake()
    {
        DummyServer.Inst.AddClient(this);
    }
}