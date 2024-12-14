namespace Game1.Authorization
{
    using System.Collections;
    using UnityEngine;
    using TMPro;

    public class AuthorizationController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _log;

        private AuthorizationSignIn _signIn;
        private AuthorizationSignUp _signUp;
        private AuthorizationReset _reset;

        private UIMenu _currentMenu;
        private bool _isSwitching;

        public void OpenSignIn() => StartCoroutine(Switch(_signIn));

        public void OpenSignUp() => StartCoroutine(Switch(_signUp));

        public void OpenReset() => StartCoroutine(Switch(_reset));

        public void WriteLog(string log) => _log.text = log;

        private void Start()
        {
            _signIn = GetComponentInChildren<AuthorizationSignIn>();
            _signUp = GetComponentInChildren<AuthorizationSignUp>();
            _reset = GetComponentInChildren<AuthorizationReset>();
            _signIn.Initialize(this);
            _signUp.Initialize(this);
            _reset.Initialize(this);
            OpenSignIn();
        }

        private IEnumerator Switch(UIMenu menu)
        {
            if (_isSwitching == true)
                yield break;
            _log.text = string.Empty;
            _isSwitching = true;
            if (_currentMenu != null)
                yield return _currentMenu.Close();
            _currentMenu = menu;
            yield return _currentMenu.Open();
            _isSwitching = false;
        }
    }
}