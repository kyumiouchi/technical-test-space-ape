using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

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
        /// Game settings Component used to get game settings.
        /// </summary>
        [SerializeField] private GameSettings gameSettings;
        
        /// <summary>
        /// Enemy Spawner Component used to get spawn enemies.
        /// </summary>
        [SerializeField] private EnemyPool enemyPool;

        /// <summary>
        /// Delegate triggered when the game is complete.
        /// </summary>
         public Action gameComplete = delegate { };
        
        /// <summary>
        /// Max time to spawn next enemy
        /// </summary>
        [SerializeField] private float secondsBetweenSpawn = 5f;
        
        /// <summary>
        /// Add time to spawn enemies
        /// </summary>
        private float _elapsedTime = 0.0f;

        
        /// <summary>
        /// Get half size of player to not pass the screen
        /// </summary>
        private float _halfSizePlayer;
        /// <summary>
        /// Current player screen position
        /// </summary>
        private Vector2 _currentPlayerScreenPosition;

        private void Start()
        {
            var playerRenderer = player.gameObject.GetComponent<Renderer>();
            _halfSizePlayer = gameCamera.GetWidthBySize(playerRenderer.bounds.extents.x);

            gameSettings.SetPlayerStartPosition(Screen.width * 0.5f, gameCamera.GetHeightBySize(playerRenderer.bounds.size.y)*2);
        }
        
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
            _currentPlayerScreenPosition = gameSettings.PlayerStartPosition;
            player.position = gameCamera.ScreenPositionToWorldPosition(_currentPlayerScreenPosition);
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
                SetPlayerNextPosition(_currentPlayerScreenPosition.x - 5);
            }
            else if (hud.RightButton.IsButtonDown)
            {
                AudioController.Instance.RunAudio(AudioType.BTN_Click);
                SetPlayerNextPosition(_currentPlayerScreenPosition.x + 5);
            }
        }
        /// <summary>
        /// Set player position does not go beyond the screen
        /// </summary>
        /// <param name="position"></param>
        private void SetPlayerNextPosition(float position)
        {
            if (position < _halfSizePlayer)
                position = _halfSizePlayer;
            else if (position > (Screen.width - _halfSizePlayer))
                position = Screen.width - _halfSizePlayer;
            _currentPlayerScreenPosition.x = position;
            player.position = gameCamera.ScreenPositionToWorldPosition(_currentPlayerScreenPosition);
        }
        
        /// <summary>
        /// Update the enemy, including spawning, and destroying the enemy when it goes off screen.
        /// </summary>
        private void UpdateEnemies()
        {
            if (!player.gameObject.activeSelf)
                return;
            
            _elapsedTime += Time.fixedDeltaTime;
            if (!(_elapsedTime > secondsBetweenSpawn)) return;
            _elapsedTime = 0;
            SpawnEnemy();
        }

        /// <summary>
        /// Spawn a new enemy at it's starting position.
        /// </summary>
        private void SpawnEnemy()
        {
            var enemy = enemyPool.ObjectPool.Get();
            var spawnPoint = new Vector2(Screen.width * gameSettings.RandomRange(), Screen.height);
            enemy.SetPosition(gameCamera.ScreenPositionToWorldPosition(spawnPoint));
            enemy.SetVelocity(gameSettings.DifficultySo.velocity);
            enemy.SetEnemyData(gameSettings.GetEnemyData());
            enemy.collisionOccurred += OnPlayerHitEnemy;
        }

        /// <summary>
        /// Destroy the enemy game object.
        /// </summary>
        /// <param name="enemy"></param>
        private void DestroyEnemy(Enemy enemy)
        {
            enemy.collisionOccurred -= OnPlayerHitEnemy;
            enemyPool.ObjectPool.Release(enemy);
        }

        /// <summary>
        /// Triggered when the player hits the enemy.
        /// </summary>
        /// <param name="enemy">Enemy instance which the player has hit.</param>
        private void OnPlayerHitEnemy(Enemy enemy)
        {
            StartCoroutine(GameOverSequence(enemy));
        }

        /// <summary>
        /// Coroutine which runs the game over sequence.
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        private IEnumerator GameOverSequence(Enemy enemy)
        {
            DestroyEnemy(enemy);
            player.gameObject.SetActive(false);

            yield return new WaitForSeconds(2.0f);

            gameComplete();
        }
    }
}