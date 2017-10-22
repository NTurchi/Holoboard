using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace Assets.HoloBoard.Scripts
{
    public class MenuScript : MonoBehaviour, IInputClickHandler
    {
        /// <summary>
        /// MenuSubset (there are active when menu header was clicked)
        /// </summary>
        public List<GameObject> GameObjectToShowWhenClick;

        /// <summary>
        /// If the attached is not the main menu, then this is a subset menu
        /// </summary>
        public GameObject MainMenu;

        /// <summary>
        /// If gameObject is active or not <see cref="GameObjectToShowWhenClick"/>
        /// </summary>
        private bool _active = false;

        // Use this for initialization
        void Awake () {
            ChangeGameObjectStatus();
        }

        /// <summary>
        /// When user tap with his finger on menu header
        /// </summary>
        /// <param name="eventData"></param>
        public void OnInputClicked(InputClickedEventData eventData)
        {
            if (MainMenu != null)
            {
                MainMenu.SendMessage("ResetOpenedSubsetSelectorMenu", gameObject);
            }
            else
            {
                if (!_active)
                {
                    gameObject.SendMessage("ResetOpenedSubsetSelectorMenu", gameObject);
                }
            }

            if (gameObject.GetComponent<Renderer>().enabled)
            {
                ChangeGameObjectStatus();
            }
        }

        /// <summary>
        /// When menu gameObject is clicked, it's submenu are disable or not
        /// </summary>
        private void ChangeGameObjectStatus()
        {
            // On désactive les sous menus dès le départ
            foreach (GameObject go in GameObjectToShowWhenClick)
            {
                go.GetComponent<Renderer>().enabled = _active;
                foreach (Renderer childR in go.GetComponentsInChildren<Renderer>())
                {
                    childR.enabled = _active;
                }
            }
            _active = !_active;
        }

        /// <summary>
        /// If other menu is clicked
        /// </summary>
        private void DisableSelectorMenu()
        {
            if (!_active)
            {
                ChangeGameObjectStatus();
            }            
        }
    }
}
