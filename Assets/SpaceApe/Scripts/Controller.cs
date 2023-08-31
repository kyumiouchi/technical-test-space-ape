using System;
using UnityEngine;

namespace DodgyBoxes
{
    public class Controller : MonoBehaviour
    {
        /// <summary>
        /// States of the game.
        /// </summary>
        private enum GameStates
        {
            /// <summary>
            /// Initial setup.
            /// </summary>
            Init,

            /// <summary>
            /// Player is in the main menu.
            /// </summary>
            MainMenu,

            /// <summary>
            /// Player is in the difficulty menu.
            /// </summary>
            DifficultyMenu,

            /// <summary>
            /// Player is playing the game.
            /// </summary>
            Game
        }

        /// <summary>
        /// MainMenu component on the main menu GameObject.
        /// </summary>
        [SerializeField] private MainMenu mainMenu = null;

        /// <summary>
        /// DifficultyMenu component on the difficulty GameObject.
        /// </summary>
        [SerializeField] private DifficultyMenu difficultyMenu = null;

        /// <summary>
        /// Game component on the toys GameObject.
        /// </summary>
        [SerializeField] private Game game = null;

        /// <summary>
        /// The current state of the game.
        /// </summary>
        private GameStates state = GameStates.Init;

        private void Start()
        {
            mainMenu.StartGame += OnStartGame;
            difficultyMenu.DifficultySelected += OnDifficultySelected;
            game.GameComplete += OnGameComplete;
            State = GameStates.MainMenu;
        }

        /// <summary>
        /// State of the game. Changing the state will transition the game.
        /// </summary>
        private GameStates State
        {

            get => state;

            set
            {
                // Cannot return to init.
                if (value == GameStates.Init)
                {
                    throw new ArgumentException("Cannot return to init state.");
                }

                state = value;

                switch (state)
                {
                    case GameStates.MainMenu:
                        mainMenu.gameObject.SetActive(true);
                        difficultyMenu.gameObject.SetActive(false);
                        game.gameObject.SetActive(false);
                        break;

                    case GameStates.DifficultyMenu:
                        mainMenu.gameObject.SetActive(false);
                        difficultyMenu.gameObject.SetActive(true);
                        game.gameObject.SetActive(false);
                        break;

                    case GameStates.Game:
                        mainMenu.gameObject.SetActive(false);
                        difficultyMenu.gameObject.SetActive(false);
                        game.gameObject.SetActive(true);
                        break;

                    case GameStates.Init:
                    // Intentional fallthrough. Init isn't supported but will be explicitly handled above.
                    default:
                        throw new ArgumentOutOfRangeException($"State machine doesn't support state {state}.");
                }
            }
        }

        /// <summary>
        /// Triggered when the player presses the start game button.
        /// </summary>
        private void OnStartGame()
        {
            State = GameStates.DifficultyMenu;
        }

        /// <summary>
        /// Triggered when the player chooses a difficulty.
        /// </summary>
        private void OnDifficultySelected(DifficultySO difficulty)
        {
            State = GameStates.Game;
            game.InitialiseGame(difficulty);
        }

        /// <summary>
        /// Triggered when the player completes a game.
        /// </summary>
        private void OnGameComplete()
        {
            State = GameStates.MainMenu;
        }
    }
}