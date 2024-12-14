namespace Game1.Authorization
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Networking;
    using TMPro;

    public class AuthorizationSignUp : UIMenu
    {
        private const string URI = "https://zone-dead.ru/sqlconnect/signup.php";

        [SerializeField]
        private TMP_InputField _nickname;
        [SerializeField]
        private TMP_InputField _password;
        [SerializeField]
        private TMP_InputField _confirm;
        [SerializeField]
        private TMP_InputField _email;
        [SerializeField]
        private Button _signUp;
        [SerializeField]
        private Button _signIn;

        private AuthorizationController _controller;

        public void Initialize(AuthorizationController controller)
        {
            _controller = controller;
            _signUp.onClick.AddListener(() => StartCoroutine(OnSignUp()));
            _signIn.onClick.AddListener(_controller.OpenSignIn);
        }

        public override IEnumerator Close()
        {
            yield return base.Close();
            _nickname.text = string.Empty;
            _password.text = string.Empty;
            _confirm.text = string.Empty;
            _email.text = string.Empty;
        }

        private IEnumerator OnSignUp()
        {
            if (string.IsNullOrWhiteSpace(_nickname.text))
                _controller.WriteLog("Incorrect nickname");
            else if (string.IsNullOrWhiteSpace(_password.text))
                _controller.WriteLog("Incorrect password");
            else if (_password.text != _confirm.text)
                _controller.WriteLog("Password isn't match");
            else if (string.IsNullOrWhiteSpace(_email.text))
                _controller.WriteLog("Incorrect email");
            else
            {
                var form = new WWWForm();
                form.AddField("nickname", _nickname.text);
                form.AddField("password", _password.text);
                form.AddField("email", _email.text);
                using (var request = UnityWebRequest.Post(URI, form))
                {
                    yield return request.SendWebRequest();
                    if (request.downloadHandler.text == "0")
                    {
                        _controller.OpenSignIn();
                        _controller.WriteLog("Account created successfully!");
                    }
                    else
                        _controller.WriteLog(request.downloadHandler.text);
                }
            }
        }
    }
}