namespace Game1
{
    using UnityEngine;
    using Unity.Netcode;

    public class NetworkController : MonoBehaviour
    {
        [SerializeField]
        private bool _isServer;

        public static NetworkController Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void StartConnection()
        {
            if (_isServer == true)
                NetworkManager.Singleton.StartServer();
            else
                NetworkManager.Singleton.StartClient();
        }

        public void StopConnection()
        {
            NetworkManager.Singleton.Shutdown();
        }
    }
}