using UnityEngine;
using Mirror;

public class ForHost : MonoBehaviour
{
    private void Start()
    {
        NetworkManager manager = GetComponent<NetworkManager>();
        manager.networkAddress = GetMyAddress.GetLocalIPv4();
        manager.StartHost();
    }
}
