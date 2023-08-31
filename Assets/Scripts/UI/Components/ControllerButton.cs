using UnityEngine;
using UnityEngine.EventSystems;

namespace DodgyBoxes
{
    /// <summary>
    /// Button the player can press on screen to trigger an action in game.
    /// </summary>
    public class ControllerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        /// <summary>
        /// Is the button held down?
        /// </summary>
        public bool IsButtonDown { get; private set; }

        /// <summary>
        ///  Inform the button that it is being pressed.
        /// </summary>
        /// <param name="eventData">Data about the press event</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            IsButtonDown = true;
        }

        /// <summary>
        /// Inform the button that is has been released.
        /// </summary>
        /// <param name="eventData">Data about the press event</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            IsButtonDown = false;
        }
    }
}