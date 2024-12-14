namespace Game1
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class SceneController : MonoBehaviour
    {       
        private UIMenu _menu;
        private Slider _progress;

        public static SceneController Instance { get; private set; }

        public void Load(string scene)
        {
            StartCoroutine(LoadAsync(scene));
        }

        private void Awake()
        {
            Instance = this;
            _menu = GetComponentInChildren<UIMenu>();
            _progress = GetComponentInChildren<Slider>();
        }

        private IEnumerator LoadAsync(string scene)
        {
            yield return _menu.Open();
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(0.5f);

            var operation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
            while (operation.isDone == false)
            {
                _progress.value = Mathf.InverseLerp(0, 0.9f, operation.progress);
                yield return null;
            }

            yield return new WaitForSecondsRealtime(0.5f);
            Time.timeScale = 1;
            yield return _menu.Close();
            _progress.value = 0;
        }
    }
}