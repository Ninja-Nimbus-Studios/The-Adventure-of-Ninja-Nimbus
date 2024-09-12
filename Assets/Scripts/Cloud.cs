using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.UI;

/*
 * Cloud class implements individual cloud game object spawned.
 * This class does not have a Deconstructor because the reference to a Cloud GameObject
 * should be made outside of the class.
 */
public class Cloud : MonoBehaviour{
    private Color color; // Change string to Color class
    private int x;
    private int y;

    private CloudMap cloudMap;
    private Image frontCloud;
    private Image backCloud;
    
    private bool isSelected = false;
    private bool isInitialized = false;
    private Vector3 inputPosition = Vector3.zero;

    void Update() {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Got Touch Event!");
                DetectTouch(touch.position);
            }
        }
        
        // Optional: For mouse click detection on desktop
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Got Mouse Event!");
            DetectTouch(Input.mousePosition);
        }
    }

    public void InitializeCloud() {
        frontCloud = this.transform.Find("border_cloud_front").GetComponent<Image>();
        backCloud = this.transform.Find("border_cloud_back").GetComponent<Image>();
        cloudMap = this.transform.parent.gameObject.GetComponent<CloudMap>();

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

    public void SetCoordinate(int newX, int newY) {
        x = newX;
        y = newY;
    }

    public void SaveCloudToSelectedMap() {
        cloudMap.OnCloudSelected(gameObject);
    }

    public void DeleteCloudFromSelectedMap(){
        cloudMap.OnCloudDeselected(gameObject);
    }

    void DetectTouch(Vector3 touchPosition)
    {
        inputPosition = touchPosition;
        // If we got a valid input position (either from mouse or touch)
        if (inputPosition != Vector3.zero)
        {
            // Create a visual line for the raycast using Debug.DrawLine
            Debug.DrawLine(inputPosition, inputPosition + Vector3.forward * 10, Color.red, 1f);

            // Raycast at the input position
            RaycastHit2D hit = Physics2D.Raycast(inputPosition, Vector2.zero);

            // Check if the raycast hit something
            if (hit.collider != null)
            {
                // Check if the hit object has a "Cloud" component
                Cloud cloud = hit.collider.GetComponent<Cloud>();
                if (cloud != null)
                {
                    // Cloud has been touched or clicked, perform the action
                    Debug.Log("Cloud touched or clicked!");

                    // Example of changing the cloud's color
                    cloud.SetColor("red"); // You can modify this action as needed

                    if (!isSelected) 
                    {
                        isSelected = true;
                        SaveCloudToSelectedMap();
                    } 
                    else
                    {
                        isSelected = false;
                        DeleteCloudFromSelectedMap();    
                    }
                }
                else
                {
                    Debug.LogError("Hit object doesn't have Cloud component!");
                }
            }
            else
            {
                Debug.LogError("Raycast didn't hit anything!");
            }
        }
    }

}
