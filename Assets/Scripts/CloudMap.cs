using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * CloudMap Class creates a map based on file input. The file perhaps could be a csv where
 * it gives information about coordinates and the color of the clouds.
 */
public class CloudMap : MonoBehaviour{

    private List<List<GameObject>> cloudMap;
    private string stageName;
    private int hoppedClouds;
    [SerializeField] private GameObject cloud;
    [SerializeField] private GameObject gameMap;

    void Start()
    {
        // Initialize a list here
        var listOfClouds = new List<Tuple<int, int, string>>();

        var colorSwitch = 0;

        for (int row = 0; row < 5; row++) {
            for (int col = 0; col < 20; col++) {
                if(colorSwitch == 0){
                    listOfClouds.Add(new Tuple<int, int, string>(row * 2,col * 2,"blue"));
                } else if(colorSwitch == 1){
                    listOfClouds.Add(new Tuple<int, int, string>(row * 2,col * 2,"red"));
                } else if(colorSwitch == 2) {
                    listOfClouds.Add(new Tuple<int, int, string>(row * 2,col * 2,"yellow"));
                } else {
                    listOfClouds.Add(new Tuple<int, int, string>(row * 2,col * 2,"white"));
                    colorSwitch = -1;
                }
                colorSwitch += 1;
            }
        }

        SetMap(listOfClouds);

        var map = GetMap();
        // foreach (List<GameObject> row in map) {
        //     foreach (GameObject cloud in row) {
        //         Debug.Log($"Newly created cloud at {cloud.transform.position}");
        //     }
        // }
    }

    public CloudMap() {
        cloudMap = new List<List<GameObject>>();
        stageName = "";
        hoppedClouds = 0;
    }

    public List<List<GameObject>> GetMap() {
        return cloudMap;
    }

    /*
     * SetMap assumes that input grid has coordinates written in 0 based index
     */
    public void SetMap(List<Tuple<int, int, string>> grid) {
        Debug.Log($"{cloud}");
        foreach(Tuple<int, int, string> cell in grid) {
            int x = cell.Item1;
            int y = cell.Item2;
            string color = cell.Item3;

            // Ensure the cloudMap is large enough to hold this cloud
            while (cloudMap.Count <= x) {
                cloudMap.Add(new List<GameObject>());
            }
            while (cloudMap[x].Count <= y) {
                cloudMap[x].Add(null);
            }

            GameObject newCloud = Instantiate(cloud, gameMap.transform);
            // newCloud.transform.position = new Vector3(x, y, 0);
            
            // Get the Cloud script from the instantiated cloud object
            Cloud cloudScript = newCloud.GetComponent<Cloud>();

            cloudScript.SetColor(color); // I need to make this async so that color is

            cloudMap[x][y] = newCloud;
        }
    }

    public void DeleteMap() {
        foreach (List<GameObject> row in cloudMap)
        {
            row.Clear();
        }
        cloudMap.Clear();
    }
}
