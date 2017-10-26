using UnityEngine;

namespace Assets.HoloBoard.Scripts
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

        /// <summary>
        /// The application main Board  
        /// </summary>
        public GameObject Board;

        void Awake()
        {
            _currentSelectedGameObject = gameObject.GetComponentsInChildren<Transform>()[1].gameObject;
            _tmpSelector = Instantiate(SelectorPrefab, _currentSelectedGameObject.transform);
            foreach (Renderer r in _tmpSelector.GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;
            }
            Board.SendMessage("ChangeColor", _currentSelectedGameObject.GetComponent<Renderer>().material);
        }

        /// <summary>
        /// When a color is clicked on the menu
        /// </summary>
        /// <param name="sender"></param>
        public void ColorClicked(GameObject sender)
        {
            if (gameObject.GetComponent<Renderer>().enabled)
            {
                if (sender != _currentSelectedGameObject)
                {
                    Destroy(_tmpSelector);
                    _currentSelectedGameObject = sender;
                    _tmpSelector = Instantiate(SelectorPrefab, _currentSelectedGameObject.transform);
                }
                // On notifie le tableau du changement de la couleur
                Board.SendMessage("ChangeColor", _currentSelectedGameObject.GetComponent<Renderer>().material);
            }
        }
    }
}
