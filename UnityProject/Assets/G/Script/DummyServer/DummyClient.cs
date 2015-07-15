using UnityEngine;

class DummyClient : MonoBehaviour
{
    private void Start()
    {
        var pks = new PKS_ENTER();
        pks.name = "pikachu";

        DummyServer.Inst.Receive(pks);
    }
}