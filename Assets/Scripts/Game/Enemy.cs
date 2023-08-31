using System;
using UnityEngine;

namespace DodgyBoxes
{
	/// <summary>
	/// Enemy component, responsible for providing the logic for the enemy.
	/// </summary>
	public class Enemy : MonoBehaviour
	{
		/// <summary>
		/// Rigidbody2D component of the enemy.
		/// </summary>
		[SerializeField] private Rigidbody2D rigidBody = null;

		/// <summary>
		/// Delegate triggered when the enemy collides with another object.
		/// </summary>
		public Action<Enemy> CollisionOccurred = delegate { };

		/// <summary>
		/// Update the position of the enemy.
		/// </summary>
		private void FixedUpdate()
		{
			rigidBody.position += Vector2.down * (Time.fixedDeltaTime * 1.0f);
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
		/// Triggered when the enemy collides with another object.
		/// </summary>
		/// <param name="other">The other object involved in the collision.</param>
		private void OnTriggerEnter2D(Collider2D other)
		{
			CollisionOccurred(this);
		}
	}
}