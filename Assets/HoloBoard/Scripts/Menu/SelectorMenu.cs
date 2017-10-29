using Assets.HoloBoard.Scripts.EventManager;
using Assets.HoloBoard.Scripts.EventManager.Menu;
using UnityEngine;

namespace Assets.HoloBoard.Scripts.Menu
{
    public class SelectorMenu : MonoBehaviour
    {
        /// <summary>
        /// If selector is open 
        /// </summary>
        private bool _isEnabled;

        // Use this for initialization
        void Start()
        {
            this.gameObject.SetActive(_isEnabled);
            // Attach method to event
            HoloBoardEventManager.Instance.EventManager<MenuEventManager>().OnSubsetMenuClicked += OnSubsetMenuClicked;
            HoloBoardEventManager.Instance.EventManager<MenuEventManager>().OnMenuHeaderClicked += OnMenuHeaderClicked;
        }

        /// <summary>
        /// When user tap on Size subset menu
        /// </summary>
        public void OnSubsetMenuClicked(GameObject sender)
        {
            GameObject parent = gameObject.GetComponent<Transform>().parent.gameObject;
            if (sender.name == parent.name)
            {
                this.SetActiveSelector();
            } else if (_isEnabled)
            {
                this.SetActiveSelector();
            }
        }

        /// <summary>
        /// When menu header is clicked
        /// </summary>
        public void OnMenuHeaderClicked()
        {
            if (_isEnabled)
            {
               this.SetActiveSelector();
            }
        }

        /// <summary>
        /// Open or color selector
        /// </summary>
        private void SetActiveSelector()
        {
            _isEnabled = !_isEnabled;
            this.gameObject.SetActive(_isEnabled);
        }
    }
}
