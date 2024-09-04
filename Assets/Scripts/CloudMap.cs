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

    void Start()
    {
        // Initialize a list here
        var listOfClouds = new List<Tuple<int, int, string>>();

        listOfClouds.Add(new Tuple<int, int, string>(0,0,"blue"));
        listOfClouds.Add(new Tuple<int, int, string>(0,1,"red"));
        listOfClouds.Add(new Tuple<int, int, string>(1,0,"green"));
        listOfClouds.Add(new Tuple<int, int, string>(1,1,"white"));

        SetMap(listOfClouds);

        var map = GetMap();
        foreach (List<GameObject> row in map) {
            foreach (GameObject cloud in row) {
                Debug.Log($"Newly created cloud at {cloud.transform.position}");
            }
        }
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

            GameObject newCloud = Instantiate(cloud);
            newCloud.transform.position = new Vector3(x, y, 0);

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
