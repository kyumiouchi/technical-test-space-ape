using System;
using UnityEngine;
using UnityEngine.Pool;

namespace DodgyBoxes
{
	/// <summary>
	/// Enemy component, responsible for providing the logic for the enemy.
	/// </summary>
	public class Enemy : MonoBehaviour, IDamageable
	{
		/// <summary>
		/// Rigidbody2D component of the enemy.
		/// </summary>
		[SerializeField] private Rigidbody2D rigidBody = null;
		
		/// <summary>
		/// BoxCollider2D component of the enemy.
		/// </summary>
		[SerializeField] private BoxCollider2D boxCollider2D = null;
		
		/// <summary>
		/// SpriteRenderer component of the enemy.
		/// </summary>
		[SerializeField] private SpriteRenderer spriteRenderer = null;
		
		/// <summary>
		/// Delegate triggered when the enemy collides with another object.
		/// </summary>
		public Action<Enemy> collisionOccurred = delegate { };

		/// <summary>
		/// Velocity of enemy get by difficulty
		/// </summary>
		private float _velocity = 1f;
        
        /// <summary>
        /// Stack-based ObjectPool available
        /// </summary>
		private IObjectPool<Enemy> objectPool;

		/// <summary>
		/// Public property to give the enemy a reference to its ObjectPool
		/// </summary>
		public IObjectPool<Enemy> ObjectPool { set => objectPool = value; }
		
		/// <summary>
		/// Update the position of the enemy.
		/// </summary>
		private void FixedUpdate()
		{
			rigidBody.position += Vector2.down * (Time.fixedDeltaTime * _velocity);
		}

		/// <summary>
		/// Set the position of the enemy.
		/// </summary>
		/// <param name="position">A position in world space.</param>
		public void SetPosition(Vector3 position)
		{
			rigidBody.position = position;
		}
		/// <summary>
		/// Set enemy information
		/// </summary>
		/// <param name="enemy">Character SO with data</param>
		public void SetEnemyData(CharacterSO enemy)
		{
			spriteRenderer.sprite = enemy.Sprite;
			var size = spriteRenderer.bounds.size;
			var localScale = transform.localScale;
			boxCollider2D.size = new Vector2(size.x/localScale.x, size.y/localScale.y);
		}

		/// <summary>
		/// Triggered when the enemy collides with another object.
		/// </summary>
		/// <param name="other">The other object involved in the collision.</param>
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				collisionOccurred(this);
			}
		}

		/// <summary>
		/// Set velocity to enemy
		/// </summary>
		/// <param name="velocity">Velocity of enemy</param>
		public void SetVelocity(float velocity)
		{
			_velocity = velocity;
		}
        
		private void OnBecameInvisible()
		{
			collisionOccurred = null;
			objectPool?.Release(this);
		}
		
		#region Damage Actions

		public int MaxHealth { get; set; }
		public float CurrentHealth { get; set; }
		public void Damage(float damageAmount)
		{
			// Damage action
			CurrentHealth -= damageAmount;
		}

		public void Die()
		{
			// Die action
		}
		#endregion

	}
}