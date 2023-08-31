using System;
using UnityEngine;

namespace DodgyBoxes
{
    /// <summary>
    /// MainMenu component, responsible for beginning a game.
    /// </summary>
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Delegate triggered when the Start Game button is pressed.
        /// </summary>
        public Action StartGame = delegate { };

        /// <summary>
        /// Begins the process of starting the game.
        /// </summary>
        public void StartGameButtonPressed()
        {
            AudioController.Instance.RunAudio(AudioType.BTN_Click);
            StartGame();
        }
    }
}