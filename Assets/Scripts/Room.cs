namespace Game1
{
    using System;
    using UnityEngine;

    public class Room : MonoBehaviour
    {
        [SerializeField]
        private int _index;

        private RoomDoor[] _doors;

        public int Index => _index;

        public Action<RoomDoorType, PlayerController> OnTriggered;

        private void Awake()
        {
            _doors = GetComponentsInChildren<RoomDoor>();
            foreach (var door in _doors)
                door.OnTriggered += (type, player) => OnTriggered?.Invoke(type, player);
        }
    }
}