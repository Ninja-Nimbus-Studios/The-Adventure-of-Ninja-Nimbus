using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> selectedPositions = new List<Vector3>();
    private List<GameObject> selectedClouds = new List<GameObject>();

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f; // Set the line width
        lineRenderer.endWidth = 0.1f;
    }

    public void AddCloud(GameObject cloud)
    {
        selectedClouds.Add(cloud);
        selectedPositions.Add(cloud.transform.position);
        UpdateLine();
    }

    public void RemoveCloud(GameObject cloud)
    {
        selectedClouds.Remove(cloud);
        selectedPositions.Remove(cloud.transform.position);
        UpdateLine();
    }

    public void ClearLines()
    {
        selectedPositions.Clear();
        UpdateLine();
    }

    private void UpdateLine()
    {
        lineRenderer.positionCount = selectedPositions.Count;
        var result = "";
        foreach (var position in selectedPositions)
        {
            result += position.ToString() + ", ";
        }
        Debug.Log($"{selectedPositions.Count()}:{result}");
        for (int i = 0; i < selectedPositions.Count; i++)
        {
            lineRenderer.SetPosition(i, selectedPositions[i]);
        }
    }
}