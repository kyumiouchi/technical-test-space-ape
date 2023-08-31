using UnityEngine;

namespace DodgyBoxes
{
    /// <summary>
    /// LoopBackground which provides loop background in vertical movement.
    /// </summary>
    public class LoopBackground : MonoBehaviour
    {
        /// <summary>
        /// Velocity of the background.
        /// </summary>
        [SerializeField] private float velocity = 4f;

        /// <summary>
        /// Cached the Y value of the reset position.
        /// </summary>
        private float _endYPositionToReset;
        
        /// <summary>
        /// Cached the start position of background.
        /// </summary>
        private Vector3 _startPosition;

        void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            _startPosition = transform.position;

            // Get the next position
            _endYPositionToReset = _startPosition.y + spriteRenderer.size.y * -1;
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * (velocity * Time.deltaTime));
            if (transform.position.y < _endYPositionToReset)
            {
                // Move the BG to start position
                transform.position = _startPosition;
            }
        }
    }
}