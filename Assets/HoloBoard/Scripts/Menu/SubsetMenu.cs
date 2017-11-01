using Assets.HoloBoard.Scripts.EventManager;
using Assets.HoloBoard.Scripts.EventManager.Menu;
using HoloToolkit.Sharing;
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
            Debug.Log(_isEnabled);

            this.gameObject.SetActive(_isEnabled);
        }

        /// <summary>
        /// When menu header is clicked
        /// </summary>
        public void OnMenuHeaderClicked()
        {
            Debug.Log(_isEnabled);
            _isEnabled = !_isEnabled;
            this.gameObject.SetActive(_isEnabled);
        }
    }
}
