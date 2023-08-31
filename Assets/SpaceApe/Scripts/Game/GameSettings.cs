using UnityEngine;

namespace DodgyBoxes
{
    /// <summary>
    /// Main Game Settings
    /// </summary>
    public class GameSettings : MonoBehaviour
    {
        /// <summary>
        /// Enemy variation list 
        /// </summary>
        [SerializeField] private CharacterSO[] listEnemies;
        
        /// <summary>
        /// Cached the difficult datas
        /// </summary>
        private DifficultySO _difficultySo;
        public DifficultySO DifficultySo => _difficultySo;
        
        /// <summary>
        /// Cached the Player start position
        /// </summary>
        private Vector2 _playerStartPosition;
        public Vector2 PlayerStartPosition => _playerStartPosition;

        public void SetDifficulty(DifficultySO difficulty)
        {
            _difficultySo = difficulty;
        }
        /// <summary>
        /// Get enemy information
        /// </summary>
        /// <returns>One enemy</returns>
        public CharacterSO GetEnemyData()
        {
            return listEnemies[Random.Range(0, listEnemies.Length - 1)];
        }
        public float RandomRange()
        {
            return Random.Range(0f, 1f);
        }

        public void ResetPlayerStartPosition(float sizeY)
        {
            _playerStartPosition = new Vector2(Screen.width * 0.5f, sizeY);
        }
    }
}