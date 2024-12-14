namespace Game1
{
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    public class UIMenu : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        private CanvasGroup _group;
        private bool _isActive;

        public virtual IEnumerator Open()
        {
            if (_isActive == true)
                yield break;
            _isActive = true;
            _group.blocksRaycasts = true;
            _group.alpha = 0;
            while (_group.alpha != 1)
            {
                _group.alpha = Mathf.MoveTowards(_group.alpha, 1, _speed);
                yield return null;
            }
            _group.interactable = true;
            _isActive = false;
        }

        public virtual IEnumerator Close()
        {
            if (_isActive == true)
                yield break;
            _isActive = true;
            _group.interactable = false;
            _group.alpha = 1;
            while (_group.alpha != 0)
            {
                _group.alpha = Mathf.MoveTowards(_group.alpha, 0, _speed);
                yield return null;
            }
            _group.blocksRaycasts = false;
            _isActive = false;
        }

        protected virtual void Awake()
        {
            _group = GetComponent<CanvasGroup>();
        }
    }
}