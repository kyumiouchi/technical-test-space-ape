using System;
using UnityEngine;

namespace DodgyBoxes
{
    /// <summary>
    /// DifficultyMenu component, responsible for allowing the player to select a difficulty.
    /// </summary>
    public class DifficultyMenu : MonoBehaviour
    {
        /// <summary>
        /// Delegate triggered when a difficulty is selected.
        /// </summary>
        public Action DifficultySelected = delegate { };

        /// <summary>
        /// Triggered when the player selects a difficulty.
        /// </summary>
        public void OnDifficultySelectedButtonPressed()
        {
            AudioController.Instance.RunAudio(AudioType.BTN_Click);
            DifficultySelected();
        }
    }
}