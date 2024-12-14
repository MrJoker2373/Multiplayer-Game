namespace Game1
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Networking;
    using Unity.Netcode;

    public class RoomController : NetworkBehaviour
    {
        private const string URI = "http://zone-dead.ru/sqlconnect/rooms.php";

        [SerializeField]
        private Room[] _rooms;
        [SerializeField]
        private UIMenu _switchMenu;
        [SerializeField]
        private UIMenu _fadeMenu;
        [SerializeField]
        private Button _hide;
        [SerializeField]
        private Button _switch;

        private Room _currentRoom;
        private PlayerController _player;
        private RoomDoorType _type;

        private void Awake()
        {
            _hide.onClick.AddListener(HideMessage);
            _switch.onClick.AddListener(() => StartCoroutine(SwitchLocation()));
            foreach (var room in _rooms)
                room.OnTriggered += ShowMessage;
            _currentRoom = _rooms[0];
        }

        private void Start() => NetworkController.Instance.StartConnection();

        private void ShowMessage(RoomDoorType type, PlayerController player)
        {
            if (player.IsOwner == false)
                return;
            StartCoroutine(_switchMenu.Open());
            _player = player;
            _type = type;
            _player.Disable();
            _player.ResetAll();
        }

        private void HideMessage()
        {
            StartCoroutine(_switchMenu.Close());
            _player.Enable();
        }

        private IEnumerator SwitchLocation()
        {
            yield return _switchMenu.Close();
            var form = new WWWForm();
            form.AddField("room", _currentRoom.Index);
            form.AddField("door", _type.ToString());
            using (var request = UnityWebRequest.Post(URI, form))
            {
                yield return _fadeMenu.Open();
                yield return request.SendWebRequest();
                if (request.downloadHandler.text == "-1")
                    _currentRoom = _rooms[0];
                else if (int.TryParse(request.downloadHandler.text, out int index) == false)
                    _currentRoom = _rooms[0];
                else
                    _currentRoom = _rooms[index];
                _player.transform.position = _currentRoom.transform.position;
                _player.Camera.ResetAll();
                yield return _fadeMenu.Close();
                _player.Enable();
            }
        }
    }
}