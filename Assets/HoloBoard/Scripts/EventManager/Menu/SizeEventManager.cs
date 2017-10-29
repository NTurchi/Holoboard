using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.HoloBoard.Scripts.EventManager.Menu
{
    /// <summary>
    /// Event manager pen size menu
    /// </summary>
    public class SizeEventManager
    {
        #region Pen size changed event
        /// <summary>
        /// Delegate for Pen size changed event
        /// </summary>
        /// <param name="newSize"></param>
        public delegate void PenSizeChangedAction(float newSize);

        /// <summary>
        /// Pen Size changed event
        /// </summary>
        public event PenSizeChangedAction OnPenSizeChanged;

        /// <summary>
        /// Trigger pen size changed event <see cref="OnPenSizeChanged"/>
        /// </summary>
        /// <param name="newSize">The new pen size selected by user</param>
        public void PenSizeChanged(float newSize)
        {
            PenSizeChangedAction onPenSizeChanged = this.OnPenSizeChanged;
            if (onPenSizeChanged != null) onPenSizeChanged(newSize);
        }
        #endregion

        #region Pen size menu clicked event
        /// <summary>
        /// Delegate for pen size menu clicked event
        /// </summary>
        public delegate void PenSizeMenuClickedAction();

        /// <summary>
        /// Pen size menu clicked event
        /// </summary>
        public event PenSizeMenuClickedAction OnPenSizeMenuClicked;

        /// <summary>
        /// When user tap on pen size click menu
        /// </summary>
        public void PenSizeMenuClicked()
        {
            PenSizeMenuClickedAction onPenSizeMenuClicked = OnPenSizeMenuClicked;
            if (onPenSizeMenuClicked != null) onPenSizeMenuClicked();
        }
        #endregion

    }
}
