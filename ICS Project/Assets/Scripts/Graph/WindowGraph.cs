using System;
using System.Collections.Generic;
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

    private void Awake() {
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
        if (transform is null)
        {
            return true;
        }

        graphContainer = transform.Find("graphContainer")?.GetComponent<RectTransform>();
        return false;
    }

    public void Draw(DrawData drawData)
    {
        ShowGraph(drawData);
    }
    private void ShowGraph(DrawData drawData) {
        var graphHeight = graphContainer.sizeDelta.y;
        const float yMaximum = 100f;
        const float xSize = 50f;

        DrawPoints(drawData.SeriaE, xSize, yMaximum, graphHeight);
        DrawPoints(drawData.SeriaI, xSize, yMaximum, graphHeight);
        DrawPoints(drawData.SeriaR, xSize, yMaximum, graphHeight);
        DrawPoints(drawData.SeriaS, xSize, yMaximum, graphHeight);

        const int separatorCount = 10;
        for (var i = 0; i < separatorCount; i++)
        {
            var labelY = Instantiate(labelTemplateY, graphContainer, false);
            labelY.gameObject.SetActive(true);
            var normalizedValue = i * 1f/separatorCount;
        }
    }

    private void DrawLabelsX()
    {
        throw new NotImplementedException();
    }
    
    private void DrawPoints(IReadOnlyList<int> valueList, float xSize, float yMaximum, float graphHeight)
    {
        GameObject lastCircleGameObject = null;
        for (var i = 0; i < valueList.Count; i++)
        {
            var xPosition = xSize + i * xSize;
            var yPosition = (valueList[i] / yMaximum) * graphHeight;
            var circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }

            lastCircleGameObject = circleGameObject;


        }
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        var cirlce = new GameObject("circle", typeof(Image));
        cirlce.transform.SetParent(graphContainer, false);
        cirlce.GetComponent<Image>().sprite = circleSprite;
        var rectTransform = cirlce.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return cirlce;
    }



    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {
        var dotConnection = new GameObject("dotConnection", typeof(Image));
        dotConnection.transform.SetParent(graphContainer, false);
        dotConnection.GetComponent<Image>().color = new Color(1,1,1, .5f);
        var rectTransform = dotConnection.GetComponent<RectTransform>();
        var dir = (dotPositionB - dotPositionA).normalized;
        var distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

}  
}

