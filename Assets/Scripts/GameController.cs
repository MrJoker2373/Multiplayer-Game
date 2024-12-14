namespace Game1
{
    using UnityEngine;

    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private string _sceneName;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            SceneController.Instance.Load(_sceneName);
        }
    }
}