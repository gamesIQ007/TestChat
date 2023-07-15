using UnityEngine;
using Mirror;

namespace NetworkChat
{
    /// <summary>
    /// Подключение к чату
    /// </summary>
    public class ConnectToChat : NetworkBehaviour
    {
        [SerializeField] private NetworkManager networkManager;


        public void StartHost()
        {
            networkManager.StartHost();
        }

        public void StartClient()
        {
            networkManager.StartClient();
        }
    }
}