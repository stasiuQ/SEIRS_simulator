using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using CodeMonkey.Utils;
using Seirs.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Seirs.Graph
{
  public class WindowGraph : MonoBehaviour {

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private List<GameObject> gameObjectList;

    private void Awake() {
        gameObjectList =  new List<GameObject>();
        Globals.ClearChartMethod = ClearChart;
        Globals.DrawChartMethod = Draw;
        if (GetGraphContainer() || GetLabelsXy()) return;
    }

    private bool GetLabelsXy()
    {
        if (graphContainer is null)
        {
            Debug.Log("graphContainer is null");
            return true;
        }
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        
        if (!(labelTemplateX is null) && !(labelTemplateY is null)) return false;
        Debug.Log("labelTemplateX is null || labelTemplateY is null");
        return true;

    }

    private bool GetGraphContainer()
    {
        graphContainer = transform.Find("graphContainer")?.GetComponent<RectTransform>();
        return false;
    }

    public void Draw(DrawData drawData)
    {
        ShowGraph(drawData);
    }
    private void ShowGraph(DrawData drawData) {
        var graphHeight = graphContainer.sizeDelta.y;
        var graphWidth = graphContainer.sizeDelta.x;
        Debug.Log(graphWidth.ToString());
        const float yMaximum = 100f;
        float xSize = graphWidth/Globals.Steps;
        ClearChart();
        Color color = Color.blue;
        
        DrawPoints(drawData.SeriesS, xSize, yMaximum, graphHeight, Color.green);
        DrawPoints(drawData.SeriesE, xSize, yMaximum, graphHeight, Color.yellow);
        DrawPoints(drawData.SeriesI, xSize, yMaximum, graphHeight, Color.red);
        DrawPoints(drawData.SeriesR, xSize, yMaximum, graphHeight, Color.blue);
       
        
    }
    
    private void ClearChart()
    {
        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();
    }
    
    private void DrawPoints(IReadOnlyList<int> valueList, float xSize, float yMaximum, float graphHeight, Color color)
    {
        GameObject lastCircleGameObject = null;
        for (var i = 0; i < valueList.Count; i++)
        {
            var xPosition = i * xSize;
            var yPosition = (valueList[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), color);
            gameObjectList.Add(circleGameObject);
            if (lastCircleGameObject != null)
            {
                GameObject dotConnection = CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition, color);
                gameObjectList.Add(dotConnection);
            }
            lastCircleGameObject = circleGameObject;
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -20f);
            labelX.GetComponent<Text>().text = i.ToString();
            gameObjectList.Add(labelX.gameObject);
        }
        int separatorCount = Globals.Steps;
        for (var i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY, graphContainer, false);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f/separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue*graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();
            gameObjectList.Add(labelY.gameObject);
        }
    }

    private GameObject CreateCircle(Vector2 anchoredPosition, Color color) {
        var cirlce = new GameObject("circle", typeof(Image));
        cirlce.GetComponent<Image>().color = color;
        cirlce.transform.SetParent(graphContainer, false);
        cirlce.GetComponent<Image>().sprite = circleSprite;
       
        var rectTransform = cirlce.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return cirlce;
    }
    
    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color color) {
        GameObject dotConnection = new GameObject("dotConnection", typeof(Image));
        dotConnection.transform.SetParent(graphContainer, false);
        dotConnection.GetComponent<Image>().color = color;
        var rectTransform = dotConnection.GetComponent<RectTransform>();
        var dir = (dotPositionB - dotPositionA).normalized;
        var distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
        return dotConnection;
    }

}  
}

