using System;
using System.Collections;
using UnityEngine;

namespace DodgyBoxes
{
    /// <summary>
    /// Game component responsible for the behaviour and logic of the game. 
    /// </summary>
    [RequireComponent(typeof(GameSettings))]
    public class Game : MonoBehaviour
    {
        /// <summary>
        /// GameCamera component used for converting between screen and world coordinates.
        /// </summary>
        [SerializeField] private GameCamera gameCamera;

        /// <summary>
        /// Rigidbody2D component of the player.
        /// </summary>
        [SerializeField] private Rigidbody2D player;

        /// <summary>
        /// GameHUD component used for providing game UI.
        /// </summary>
        [SerializeField] private GameHUD hud;

        /// <summary>
        /// Enemy component of the enemy prefab, used to create new enemies
        /// </summary>
        [SerializeField] private Enemy enemyPrefab;

        /// <summary>
        /// SettingsGame Component used to get game settings.
        /// </summary>
        [SerializeField] private GameSettings gameSettings;
        /// <summary>
        /// The enemy in the game.
        /// </summary>
        private Enemy enemy;

        /// <summary>
        /// Delegate triggered when the game is complete.
        /// </summary>
        public Action GameComplete = delegate { };

        /// <summary>
        /// Initial setup of the game.
        /// </summary>
        /// <param name="difficultySo"></param>
        public void InitialiseGame(DifficultySO difficulty)
        {
            player.gameObject.SetActive(true);

            ResetPlayerPosition();
            
            gameSettings.SetDifficulty(difficulty);
        }

        /// <summary>
        /// Reset the position of the player to their starting position. 
        /// </summary>
        private void ResetPlayerPosition()
        {
            var playerScreenPosition = new Vector2(Screen.width * 0.5f, 200);
            var playerWorldPosition = gameCamera.ScreenPositionToWorldPosition(playerScreenPosition);
            player.position = playerWorldPosition;
        }

        /// <summary>
        /// Update the physics based components with in the game, namely the player, and enemy. 
        /// </summary>
        private void FixedUpdate()
        {
            UpdatePlayer();

            UpdateEnemies();
        }

        /// <summary>
        /// Update the player position based on the input.
        /// </summary>
        private void UpdatePlayer()
        {
            if (hud.LeftButton.IsButtonDown)
            {
                AudioController.Instance.RunAudio(AudioType.BTN_Click);
                player.position = gameCamera.ScreenPositionToWorldPosition(new Vector2(100, 200));
            }
            else if (hud.RightButton.IsButtonDown)
            {
                AudioController.Instance.RunAudio(AudioType.BTN_Click);
                player.position = gameCamera.ScreenPositionToWorldPosition(new Vector2(Screen.width - 100, 200));
            }
        }

        /// <summary>
        /// Update the enemy, including spawning, and destroying the enemy when it goes off screen.
        /// </summary>
        private void UpdateEnemies()
        {
            if (!enemy)
            {
                SpawnEnemy();
            }
            else
            {
                var enemyPositionOnScreen = gameCamera.WorldPositionToScreenPosition(enemy.transform.position);
                if (enemyPositionOnScreen.y < 0)
                {
                    DestroyEnemy();
                    SpawnEnemy();
                }
            }
        }

        /// <summary>
        /// Spawn a new enemy at it's starting position.
        /// </summary>
        private void SpawnEnemy()
        {
            enemy = Instantiate(enemyPrefab);
            enemy.SetPosition(
                gameCamera.ScreenPositionToWorldPosition(new Vector2(Screen.width * 0.5f, Screen.height))
            );
            enemy.SetVelocity(gameSettings.DifficultySo.velocity);
            enemy.CollisionOccurred += OnPlayerHitEnemy;
        }

        /// <summary>
        /// Destroy the enemy game object.
        /// </summary>
        private void DestroyEnemy()
        {
            Destroy(enemy.gameObject);
            enemy = null;
        }

        /// <summary>
        /// Triggered when the player hits the enemy.
        /// </summary>
        /// <param name="enemy">Enemy instance which the player has hit.</param>
        private void OnPlayerHitEnemy(Enemy enemy)
        {
            StartCoroutine(GameOverSequence());
        }

        /// <summary>
        /// Coroutine which runs the game over sequence.
        /// </summary>
        /// <returns></returns>
        private IEnumerator GameOverSequence()
        {
            DestroyEnemy();
            player.gameObject.SetActive(false);

            yield return new WaitForSeconds(2.0f);

            GameComplete();
        }
    }
}