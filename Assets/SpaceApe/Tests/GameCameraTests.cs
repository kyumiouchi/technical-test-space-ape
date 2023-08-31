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
    private static readonly Vector2EqualityComparer Vector2Comparer = new(0.05f);
        
    private static readonly Vector2 ScreenPositionForBottomLeft = Vector2.zero;
    private static readonly Vector3 WorldPositionForScreenBottomLeft = new(-2.815f, -5.00f, 0.00f);
    
    private static readonly Vector2 ScreenPositionForTopRight = new(Screen.width, Screen.height);
    private static readonly Vector3 WorldPositionForScreenTopRight = new(2.815f, 5.00f, 0.00f);
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        SceneManager.LoadScene("SpaceApe/Scenes/Main");
    }
    
    [UnityTest]
    public IEnumerator TestGameCamera_ScreenPositionToWorldPosition_BottomLeft_IsCorrect()
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
    
    
    [UnityTest]
    public IEnumerator TestGameCamera_ScreenPositionToWorldPosition_TopRight_IsCorrect()
    {
        // Find the game camera in the scene.
        var gameCamera = GameObject.FindObjectOfType<GameCamera>();
        
        // Perform a screen to world position conversion.
        var worldPosition = gameCamera.ScreenPositionToWorldPosition(ScreenPositionForTopRight);
        
        // Wait a frame.
        yield return null;

        // Ensure that it's correct.
        Assert.That(
            worldPosition,
            Is.EqualTo(WorldPositionForScreenTopRight)
                .Using(Vector3Comparer)
        );
    }
    
    
    [UnityTest]
    public IEnumerator TestGameCamera_WorldPositionToScreenPosition_BottomLeft_IsCorrect()
    {
        // Find the game camera in the scene.
        var gameCamera = GameObject.FindObjectOfType<GameCamera>();
        
        // Perform a world to screen position conversion.
        var screenPosition = gameCamera.WorldPositionToScreenPosition(WorldPositionForScreenBottomLeft);
        
        // Wait a frame.
        yield return null;
        
        // Ensure that it's correct.
        Assert.That(
            screenPosition,
            Is.EqualTo(ScreenPositionForBottomLeft)
                .Using(Vector2Comparer)
        );
    }
    
    
    [UnityTest]
    public IEnumerator TestGameCamera_WorldPositionToScreenPosition_TopRight_IsCorrect()
    {
        // Find the game camera in the scene.
        var gameCamera = GameObject.FindObjectOfType<GameCamera>();
        
        // Perform a world to screen position conversion.
        var screenPosition = gameCamera.WorldPositionToScreenPosition(WorldPositionForScreenTopRight);
        
        // Wait a frame.
        yield return null;
        
        // Ensure that it's correct.
        Assert.That(
            screenPosition,
            Is.EqualTo(ScreenPositionForTopRight)
                .Using(Vector2Comparer)
        );
    }
}
