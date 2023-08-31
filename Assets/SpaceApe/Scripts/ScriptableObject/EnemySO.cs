using UnityEngine;

namespace DodgyBoxes
{
    /// <summary>
    /// Character Scriptable Object responsible for standard character data,
    /// as Player or Enemy
    /// </summary>
    [CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
    public class CharacterSO : ScriptableObject
    {
        /// <summary>
        /// Image of character
        /// </summary>
        [SerializeField] private Sprite sprite;
        /// <summary>
        /// Quantity of damage
        /// </summary>
        [SerializeField] private int damage;
        /// <summary>
        /// Start health of character
        /// </summary>
        [SerializeField] private int maxHealth;

        public Sprite Sprite => sprite;
        public int Damage => damage;
        public int MaxHealth => maxHealth;
    }
}
