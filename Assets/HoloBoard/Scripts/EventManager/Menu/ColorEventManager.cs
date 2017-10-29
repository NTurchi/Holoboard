using UnityEngine;

namespace Assets.HoloBoard.Scripts.EventManager.Menu
{
    /// <summary>
    /// Event manager for Color selection menu
    /// </summary>
    public class ColorEventManager {

        #region Pen color changed event
        /// <summary>
        /// Delegate for Color changed event
        /// </summary>
        /// <param name="newColorItem">New color select by user</param>
        public delegate void PenColorChangedAction(GameObject newColorItem);

        /// <summary>
        /// Color changed event
        /// </summary>
        public event  PenColorChangedAction OnPenColorChanged;

        /// <summary>
        /// Trigger color changed event <see cref="OnPenColorChanged"/>
        /// </summary>
        /// <param name="newColor">The new color select by user (It's a gameObject but we just need to get his material object 
        /// to get the new seletected color)</param>
        public void ColorChanged(GameObject newColor)
        {
            PenColorChangedAction onPenColorChanged = this.OnPenColorChanged;
            if (onPenColorChanged != null) onPenColorChanged(newColor);
        }
        #endregion

        #region Pen color menu click event
        /// <summary>
        /// Delegate for pen color menu clicked event
        /// </summary>
        public delegate void PenColorMenuClickedAction();

        /// <summary>
        /// Pen color menu clicked event 
        /// </summary>
        public event PenColorMenuClickedAction OnPenColorMenuClicked;

        /// <summary>
        /// When user tap on color menu
        /// </summary>
        public void PenColorMenuClicked()
        {
            PenColorMenuClickedAction onPenColorMenuClicked = OnPenColorMenuClicked;
            if (onPenColorMenuClicked != null) onPenColorMenuClicked();
        }

        #endregion
    }
}
