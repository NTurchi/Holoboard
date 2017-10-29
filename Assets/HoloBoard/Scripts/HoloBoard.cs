using Assets.HoloBoard.Scripts.EventManager;
using Assets.HoloBoard.Scripts.EventManager.Menu;
using UnityEngine;

namespace Assets.HoloBoard.Scripts
{
    /// <summary>
    /// Main app script. Regroups parameters and app initialization method
    /// </summary>
    public class HoloBoard : MonoBehaviour
    {
        /// <summary>
        /// App event manager
        /// </summary>
        private HoloBoardEventManager _eventManager;

        #region Board parameters
        /// <summary>
        /// Board PenSize
        /// </summary>
        public static float PenSize = 0.01f;
        
        /// <summary>
        /// Pen color
        /// </summary>
        public static Material PenColor;

        /// <summary>
        /// TODO: Change this
        /// </summary>
        public Material DefaultPenColor;

        /// <summary>
        /// When color selector changed
        /// </summary>
        /// <param name="newColorItem">New Pen Color</param>
        public void ChangeColor(GameObject newColorItem)
        {
            HoloBoard.PenColor = newColorItem.GetComponent<Renderer>().material;
        }

        /// <summary>
        /// When size selector changed
        /// </summary>
        /// <param name="newSize">New Size</param>
        public void ChangeSize(float newSize)
        {
            HoloBoard.PenSize = newSize;
        }
        #endregion 

        void Awake ()
        {
            // Default
            PenColor = DefaultPenColor;

            _eventManager = HoloBoardEventManager.Instance;

            // Register all Event manager
            _eventManager.RegisterManager<ColorEventManager>();
            _eventManager.RegisterManager<SizeEventManager>();
            _eventManager.RegisterManager<MenuEventManager>();

            // Assign method to event
            _eventManager.EventManager<ColorEventManager>().OnPenColorChanged += this.ChangeColor;
            _eventManager.EventManager<SizeEventManager>().OnPenSizeChanged += this.ChangeSize;
        }
	
    }
}
