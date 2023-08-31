using UnityEngine;
using UnityEngine.Pool;

namespace DodgyBoxes
{
    /// <summary>
    /// Spawn the Enemy using Object Pool Pattern
    /// </summary>
    public class EnemyPool : MonoBehaviour
    {        
        /// <summary>
        /// Stack-based ObjectPool available
        /// </summary>
        private IObjectPool<Enemy> objectPool;

        /// <summary>
        /// Public property to give the enemy a reference to its ObjectPool
        /// </summary>
        public IObjectPool<Enemy> ObjectPool
        {
            get => objectPool;
            set => objectPool = value;
        }

        /// <summary>
        /// Collection checks will throw errors if we try to release an item that is already in the pool.
        /// </summary>
        [SerializeField] private bool collectionCheck = false;
        /// <summary>
        /// Size of the object pool
        /// </summary>
        [SerializeField] private int defaultCapacity = 10;
        /// <summary>
        /// Max Size of the object pool
        /// </summary>
        [SerializeField] private int maxSize = 30;
        /// <summary>
        /// Enemy component of the enemy prefab, used to create new enemies
        /// </summary>
        [SerializeField] private Enemy enemyPrefab;
        
        private void Start()
        {
            objectPool = new ObjectPool<Enemy>(CreateEnemy,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
        }

        /// <summary>
        /// Invoked when creating an enemy to populate the object pool
        /// </summary>
        private Enemy CreateEnemy()
        {
            Enemy enemy = Instantiate(enemyPrefab);
            enemy.ObjectPool = objectPool;
            return enemy;
        }

        /// <summary>
        /// Invoked when returning an enemy to the object pool
        /// </summary>
        private void OnReleaseToPool(Enemy pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        /// <summary>
        /// Invoked when retrieving the next enemy from the object pool
        /// </summary>
        private void OnGetFromPool(Enemy pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        /// <summary>
        /// Invoked when we exceed the maximum number of pooled enemies (i.e. destroy the pooled object)
        /// </summary>
        private void OnDestroyPooledObject(Enemy pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }
    }
}