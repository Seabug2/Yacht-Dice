using UnityEngine;
using Mirror;

public class ForGuest : MonoBehaviour
{
    private void Start()
    {
        NetworkManager manager = GetComponent<NetworkManager>();
        manager.StartHost();
        //manager.networkAddress = GetMyAddress.GetLocalIPv4();
    }
}
