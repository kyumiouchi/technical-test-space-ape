using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DodgyBoxes
{
    public class GameSettings : MonoBehaviour
    {
        /// <summary>
        /// Cached the difficult datas
        /// </summary>
        private DifficultySO _difficultySo;
        public DifficultySO DifficultySo => _difficultySo;

        public void SetDifficulty(DifficultySO difficulty)
        {
            _difficultySo = difficulty;
        }
    }
}