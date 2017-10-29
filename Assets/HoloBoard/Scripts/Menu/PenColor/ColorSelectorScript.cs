using Assets.HoloBoard.Scripts.EventManager;
using Assets.HoloBoard.Scripts.EventManager.Menu;
using UnityEngine;

namespace Assets.HoloBoard.Scripts.Menu.PenColor
{
    public class ColorSelectorScript : MonoBehaviour
    {
        /// <summary>
        /// Black color selector
        /// </summary>
        public GameObject SelectorPrefab;

        /// <summary>
        /// Current color selected
        /// </summary>
        private GameObject _currentSelectedGameObject;

        /// <summary>
        /// Current black selector prefabs object instantiated
        /// </summary>
        private GameObject _tmpSelector;

       

        void Start()
        {
            _currentSelectedGameObject = gameObject.GetComponentsInChildren<Transform>()[1].gameObject;
            _tmpSelector = Instantiate(SelectorPrefab, _currentSelectedGameObject.transform);

            // Attach method to event
            HoloBoardEventManager.Instance.EventManager<ColorEventManager>().OnPenColorChanged += ColorClicked;
        }

        /// <summary>
        /// When a color is clicked on the menu
        /// </summary>
        /// <param name="sender"></param>
        private void ColorClicked(GameObject sender)
        {
            if (sender != _currentSelectedGameObject)
            {
                
                Destroy(_tmpSelector);
                _currentSelectedGameObject = sender;
                _tmpSelector = Instantiate(SelectorPrefab, _currentSelectedGameObject.transform);
            }     
        }
    }
}
