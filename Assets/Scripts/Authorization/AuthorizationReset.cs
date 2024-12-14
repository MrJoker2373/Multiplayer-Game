namespace Game1.Authorization
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Networking;
    using TMPro;

    public class AuthorizationReset : UIMenu
    {
        private const string URI = "http://zone-dead.ru/sqlconnect/reset.php";

        [SerializeField]
        private TMP_InputField _email;
        [SerializeField]
        private Button _reset;
        [SerializeField]
        private Button _signIn;

        private AuthorizationController _controller;

        public void Initialize(AuthorizationController controller)
        {
            _controller = controller;
            _reset.onClick.AddListener(() => StartCoroutine(OnReset()));
            _signIn.onClick.AddListener(_controller.OpenSignIn);
        }

        public override IEnumerator Close()
        {
            yield return base.Close();
            _email.text = string.Empty;
        }

        private IEnumerator OnReset()
        {
            if (string.IsNullOrWhiteSpace(_email.text))
                _controller.WriteLog("Incorrect email");
            else
            {
                var form = new WWWForm();
                form.AddField("email", _email.text);
                using (var request = UnityWebRequest.Post(URI, form))
                {
                    yield return request.SendWebRequest();
                    if (request.downloadHandler.text == "0")
                    {
                        _controller.OpenSignIn();
                        _controller.WriteLog("Letter send");
                    }
                    else
                        _controller.WriteLog(request.downloadHandler.text);
                }
            }
        }
    }
}