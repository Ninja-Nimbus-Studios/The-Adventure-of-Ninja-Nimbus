using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

/*
 * Cloud class implements individual cloud game object spawned.
 * This class does not have a Deconstructor because the reference to a Cloud GameObject
 * should be made outside of the class.
 */
public class Cloud : MonoBehaviour{

    public GameObject cloudPrefab;
    private Color color; // Change string to Color class
    private int x;
    private int y;
    private Image frontCloud;
    private Image backCloud;
    private bool isInitialized = false;

    public void InitializeCloud() {
        // frontCloud = cloudPrefab.GetComponentsInChildren<SpriteRenderer>()[0];
        frontCloud = transform.Find("border_cloud_front").GetComponent<Image>();
        backCloud = transform.Find("border_cloud_back").GetComponent<Image>();

        // Optionally, log the references to verify they're correct
        if (frontCloud == null || backCloud == null)
        {
            Debug.LogError("Failed to find child GameObjects or their SpriteRenderer components!");
        } else {
            Debug.Log("Child Objects have been found!");
        }

        isInitialized = true;
    }

    public (int, int, Color) GetCloud() {
        return (x, y, color);
    }

    public void SetColor(string newColor){
        if(!isInitialized) {
            InitializeCloud();
        }

        // guard for reference to front and back cloud
        if (frontCloud is null || backCloud is null) {
            Debug.LogError($"Error: frontCloud reference -> {frontCloud}; backCloud reference -> {backCloud}");
            return;
        }

        switch (newColor) {
            case "blue":
                color = Color.blue;
                break;
            case "red":
                color = Color.red;
                break;
            case "yellow":
                color = Color.yellow;
                break;
            default:
                color = Color.white;
                break;
        }

        frontCloud.color = color;
        backCloud.color = color;
    }

    public void SetCoordinate(int newX, int newY){
        x = newX;
        y = newY;
    }

}
