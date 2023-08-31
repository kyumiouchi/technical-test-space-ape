namespace DodgyBoxes
{
    /// <summary>
    /// IDamageable interface of characters with health and cause damage
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Max health
        /// </summary>
        int MaxHealth { get; set; }
        /// <summary>
        /// Recent Health
        /// </summary>
        float CurrentHealth { get; set; }

        /// <summary>
        /// Damage that character receive
        /// </summary>
        /// <param name="damageAmount"></param>
        void Damage(float damageAmount);
        /// <summary>
        /// Die action
        /// </summary>
        void Die();
    }
}