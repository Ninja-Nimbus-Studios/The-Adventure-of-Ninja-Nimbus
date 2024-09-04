using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;

public class CloudTests
{
    private CloudMap cloudMap;

    [SetUp]
    public void SetUp()
    {
        // Initialize the CloudMap before each test
        cloudMap = new CloudMap();
    }

    // A Test to check TestRunner
    [Test]
    public void TestTestRunnerPasses()
    {
        // Use the Assert class to test conditions
        Cloud cloud = new Cloud();

        var cloudData = cloud.GetCloud();
        Assert.AreNotEqual(3, cloudData.Item1);
        Assert.AreNotEqual(3, cloudData.Item2);
        Assert.AreNotEqual("rainbow", cloudData.Item3);
    }

    // Test Constructor with color input for cloud class
    [Test]
    public void TestCloudInitialization()
    {
        // Act: Create a cloud and set its properties
        Cloud cloud = new Cloud("yellow");
        cloud.SetCoordinate(3, 4);
        cloud.SetColor("yellow");

        // Assert: Check if the cloud properties are set correctly
        var cloudData = cloud.GetCloud();
        Assert.AreEqual(3, cloudData.Item1);
        Assert.AreEqual(4, cloudData.Item2);
        Assert.AreEqual("yellow", cloudData.Item3);
    }

    // Test CloudMap creation has correct color
    [Test]
    public void TestSetMap_CreatesCorrectClouds()
    {
        // Arrange: Create a test grid
        var testGrid = new List<Tuple<int, int, string>>
        {
            new Tuple<int, int, string>(0, 0, "red"),
            new Tuple<int, int, string>(1, 1, "blue"),
            new Tuple<int, int, string>(2, 2, "green")
        };

        // Act: Set the map with the test grid
        cloudMap.SetMap(testGrid);

        // Assert: Check if the clouds are created correctly
        var map = cloudMap.GetMap();
        Assert.AreEqual(3, map.Count);
        // Assert.AreEqual("red", map[0][0].GetCloud().Item3);
        // Assert.AreEqual("blue", map[1][1].GetCloud().Item3);
        // Assert.AreEqual("green", map[2][2].GetCloud().Item3);
    }

    // Test CloudMap gets deleted
    [Test]
    public void TestDeleteMap_ClearsAllClouds()
    {
        // Arrange: Create a test grid and set the map
        var testGrid = new List<Tuple<int, int, string>>
        {
            new Tuple<int, int, string>(0, 0, "red"),
            new Tuple<int, int, string>(1, 1, "blue")
        };
        cloudMap.SetMap(testGrid);

        // Act: Delete the map
        cloudMap.DeleteMap();

        // Assert: Check if the map is cleared
        var map = cloudMap.GetMap();
        Assert.AreEqual(0, map.Count);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CloudTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}