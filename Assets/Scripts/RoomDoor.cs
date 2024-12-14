namespace Game1
{
    using System;
    using UnityEngine;

    public class RoomDoor : MonoBehaviour
    {
        [SerializeField]
        private RoomDoorType _type;

        public Action<RoomDoorType, PlayerController> OnTriggered;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<PlayerController>(out var player))
                OnTriggered?.Invoke(_type, player);
        }
    }
}