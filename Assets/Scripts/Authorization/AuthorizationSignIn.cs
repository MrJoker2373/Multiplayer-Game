namespace Game1.Authorization
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Networking;
    using TMPro;

    public class AuthorizationSignIn : UIMenu
    {
        private const string URI = "http://zone-dead.ru/sqlconnect/signin.php";

        [SerializeField]
        private TMP_InputField _nickname;
        [SerializeField]
        private TMP_InputField _password;
        [SerializeField]
        private Button _signIn;
        [SerializeField]
        private Button _signUp;
        [SerializeField]
        private Button _reset;
        [SerializeField]
        private string _sceneName;

        private AuthorizationController _controller;

        public void Initialize(AuthorizationController controller)
        {
            _controller = controller;
            _signIn.onClick.AddListener(() => StartCoroutine(OnSignIn()));
            _signUp.onClick.AddListener(_controller.OpenSignUp);
            _reset.onClick.AddListener(_controller.OpenReset);
        }

        public override IEnumerator Close()
        {
            yield return base.Close();
            _nickname.text = string.Empty;
            _password.text = string.Empty;
        }

        private IEnumerator OnSignIn()
        {
            if (string.IsNullOrWhiteSpace(_nickname.text))
                _controller.WriteLog("Incorrect nickname");
            else if (string.IsNullOrWhiteSpace(_password.text))
                _controller.WriteLog("Incorrect password");
            else
            {
                var form = new WWWForm();
                form.AddField("nickname", _nickname.text);
                form.AddField("password", _password.text);
                using (var request = UnityWebRequest.Post(URI, form))
                {
                    yield return request.SendWebRequest();
                    if (request.downloadHandler.text == "0")
                        SceneController.Instance.Load(_sceneName);
                    else
                        _controller.WriteLog(request.downloadHandler.text);
                }
            }
        }
    }
}