using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using DodgyBoxes;
using UnityEngine.TestTools.Utils;

public class GameCameraTests
{
    // Due to the nature of the conversion between screen and world positions, values are not exact.
    // As such we need to use a comparer that allows for a small amount of floating point error.
    private static readonly Vector3EqualityComparer Vector3Comparer = new(0.05f);
        
    private static readonly Vector2 ScreenPositionForBottomLeft = Vector2.zero;
    private static readonly Vector3 WorldPositionForScreenBottomLeft = new(-2.81f, -5.00f, 0.00f);

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.LoadScene("SpaceApe/Scenes/Main");
    }
    
    [UnityTest]
    public IEnumerator TestGameCamera_ScreenPositionToWorldPosition_IsCorrect()
    {
        // Find the game camera in the scene.
        var gameCamera = GameObject.FindObjectOfType<GameCamera>();
        
        // Perform a screen to world position conversion.
        var worldPosition = gameCamera.ScreenPositionToWorldPosition(ScreenPositionForBottomLeft);
        
        // Wait a frame.
        yield return null;

        // Ensure that it's correct.
        Assert.That(
            worldPosition,
            Is.EqualTo(WorldPositionForScreenBottomLeft)
            .Using(Vector3Comparer)
        );
    }
}
