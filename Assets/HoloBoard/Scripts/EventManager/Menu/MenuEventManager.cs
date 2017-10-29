using UnityEngine;

namespace Assets.HoloBoard.Scripts.EventManager.Menu
{
    /// <summary>
    /// Menu header event manager
    /// </summary>
    public class MenuEventManager
    {
        /// <summary>
        /// Delegate for menu header clicked event
        /// </summary>
        public delegate void MenuHeaderClickedAction();

        /// <summary>
        /// Menu header clicked event
        /// </summary>
        public event MenuHeaderClickedAction OnMenuHeaderClicked;

        /// <summary>
        /// When user clicked on menu header (GameObject with label "Menu")
        /// </summary>
        public void MenuHeaderClicked()
        {
            MenuHeaderClickedAction onMenuHeaderClicked = OnMenuHeaderClicked;
            if (onMenuHeaderClicked != null) onMenuHeaderClicked();
        }

        #region Subset menu clicked event
        /// <summary>
        /// Delegate for subset menu clicked event
        /// </summary>
        /// <param name="sender"></param>
        public delegate void SubsetMenuClickedAction(GameObject sender);

        /// <summary>
        /// Subset menu clicked event
        /// </summary>
        public event SubsetMenuClickedAction OnSubsetMenuClicked;

        /// <summary>
        /// When subset menu is clicked
        /// </summary>
        /// <param name="sender"></param>
        public void SubsetMenuClicked(GameObject sender)
        {
            SubsetMenuClickedAction onSubsetMenuClickedAction = OnSubsetMenuClicked;
            if (onSubsetMenuClickedAction != null) onSubsetMenuClickedAction(sender);
        }
        #endregion
    }
}
