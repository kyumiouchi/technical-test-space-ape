using UnityEngine;

/// <summary>
/// Scriptable Objects responsible with the difficulty data
/// </summary>
[CreateAssetMenu(fileName = "Difficulty", menuName = "ScriptableObjects/Difficulty")]
public class DifficultySO : ScriptableObject
{
    /// <summary>
    /// Velocity of the enemies
    /// </summary>
    public float velocity;
}
