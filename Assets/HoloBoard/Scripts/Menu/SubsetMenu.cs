using Assets.HoloBoard.Scripts.EventManager;
using Assets.HoloBoard.Scripts.EventManager.Menu;
using UnityEngine;

namespace Assets.HoloBoard.Scripts.Menu
{
    public class SubsetMenu : MonoBehaviour
    {
        /// <summary>
        /// If the subset menu is enabled or not
        /// </summary>
        private bool _isEnabled;

        void Start()
        {
            HoloBoardEventManager.Instance.EventManager<MenuEventManager>().OnMenuHeaderClicked +=
                OnMenuHeaderClicked;
            this.gameObject.SetActive(_isEnabled);
        }

        /// <summary>
        /// When menu header is clicked
        /// </summary>
        public void OnMenuHeaderClicked()
        {
            _isEnabled = !_isEnabled;
            this.gameObject.SetActive(_isEnabled);
        }
    }
}
