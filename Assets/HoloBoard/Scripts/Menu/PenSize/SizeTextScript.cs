using Assets.HoloBoard.Scripts.EventManager;
using Assets.HoloBoard.Scripts.EventManager.Menu;
using UnityEngine;

namespace Assets.HoloBoard.Scripts.Menu.PenSize
{
    public class SizeTextScript : MonoBehaviour {

        // Use this for initialization
        void Start () {
		   HoloBoardEventManager.Instance.EventManager<SizeEventManager>().PenSizeChanged(float.Parse(this.gameObject.GetComponent<TextMesh>().text));
        }
    }
}
