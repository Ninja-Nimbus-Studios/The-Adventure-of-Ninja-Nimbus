using UnityEngine;

/*
 * Cloud class implements individual cloud game object spawned.
 * This class does not have a Deconstructor because the reference to a Cloud GameObject
 * should be made outside of the class.
 */
public class Cloud : MonoBehaviour{

    private string color; // Change string to Color class
    private int x;
    private int y;
    public GameObject cloudPrefab;
    public Cloud() {
        color = "";
        x = 0;
        y = 0;
    }

    public Cloud(string color) {
        x = 0;
        y = 0;
        this.color = color;
    }

    public Cloud(int x, int y, string color) {
        this.x = x;
        this.y = y;
        this.color = color;
    }

    public (int, int, string) GetCloud() {
        return (x, y, color);
    }

    public void SetColor(string newColor){
        color = newColor;
    }

    public void SetCoordinate(int newX, int newY){
        x = newX;
        y = newY;
    }

}
