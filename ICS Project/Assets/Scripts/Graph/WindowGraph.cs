using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using Seirs.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Seirs.Graph
{
  public class WindowGraph : MonoBehaviour { 
      
      private static WindowGraph instance;
      
      [SerializeField] private Sprite dotSprite; 
      private RectTransform graphContainer; 
      private RectTransform labelTemplateX; 
      private RectTransform labelTemplateY; 
      private List<GameObject> gameObjectList; 
      private List<IGraphVisualObject> graphVisualObjectList; 
      
      private List<int> valueList; 
      private IGraphVisual graphVisual; 
      private float yMaximum = 100f; 
      private float xSize; 
      private void Awake()
    {
        instance = this;
        if (GetGraphContainer() || GetLabelsXy()) return;
        
        gameObjectList =  new List<GameObject>();
        graphVisualObjectList = new List<IGraphVisualObject>();
        valueList = new List<int>(DrawData.GetInstance.SeriesI);
        Globals.ClearChartMethod = ClearChart;
        Globals.UpdateChartMethod = UpdateChart;
        graphVisual = new LineGraphVisual(graphContainer, dotSprite, Color.green);
        ShowGraph(valueList,graphVisual);
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

    public void UpdateChart(int[] stats)
    {
        if (Globals.stats.Step <= Globals.Steps)
        {
            UpdateValue(stats[5], stats[2]);
        }
    }
    /*public void Draw(DrawData drawData)
    {
        ShowGraph(drawData,graphVisual);
    }*/
    private void ShowGraph(List<int> valueList, IGraphVisual graphVisual) {
        
        this.graphVisual = graphVisual;
        ClearChart();
        
        var graphHeight = graphContainer.sizeDelta.y;
        var graphWidth = graphContainer.sizeDelta.x;
        xSize = graphWidth/Globals.Steps;
        
        DrawPoints(valueList, graphVisual, xSize, yMaximum, graphHeight, Color.green);
        //DrawPoints(drawData.SeriesE, graphVisual, xSize, yMaximum, graphHeight, Color.yellow);
        //DrawPoints(drawData.SeriesI, graphVisual, xSize, yMaximum, graphHeight, Color.red);
        //DrawPoints(drawData.SeriesR, graphVisual, xSize, yMaximum, graphHeight, Color.blue);

    }
    
    private void ClearChart()
    {
        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();
        foreach (IGraphVisualObject graphVisualObject in graphVisualObjectList)
        {
            graphVisualObject.CleanUp();
        }
        graphVisualObjectList.Clear();
        graphVisual.CleanUp();
    }
    
    private void DrawPoints(IReadOnlyList<int> valueList, IGraphVisual graphVisual, float xSize, float yMaximum, float graphHeight, Color color)
    {
        for (var i = 0; i < valueList.Count; i++)
        {
            var xPosition = i * xSize;
            var yPosition = (valueList[i] / yMaximum) * graphHeight;
            graphVisualObjectList.Add(graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize));
            
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -7f);
            labelX.GetComponent<Text>().text = i.ToString();
            gameObjectList.Add(labelX.gameObject);
        }
        int separatorCount = Globals.Steps;
        for (var i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f/separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue*graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();
            gameObjectList.Add(labelY.gameObject);
        }
    }
    private void UpdateValue(int index, int value)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        valueList[index] = value;
        var xPosition = index * xSize;
        var yPosition = (value/ yMaximum) * graphHeight;
        IGraphVisualObject graphVisualObject =
            graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize);
        graphVisualObjectList[index].SetGraphVisualObjectInfo(new Vector2(xPosition, yPosition), xSize);
    }

    
    private interface IGraphVisual
    {
        IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth);

        void CleanUp();
    }
    
    private interface IGraphVisualObject
    {
        void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth);
        void CleanUp();
    }
    /*
    private class BarChartVisual : IGraphVisual
    {
        private RectTransform graphContainer;
        private Color barColor;
        private float barWidthMultiplier;

        public BarChartVisual(RectTransform graphContainer, Color barColor, float barWidthMultiplier)
        {
            this.graphContainer = graphContainer;
            this.barColor = barColor;
            this.barWidthMultiplier = barWidthMultiplier;
        }

        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth)
        {
            GameObject barGameObject = CreateBar(graphPosition, graphPositionWidth * 0.9f);
            BarChartVisualObject barChartVisualObject = new BarChartVisualObject(barGameObject, barWidthMultiplier);
            barChartVisualObject.SetGraphVisualObjectInfo(graphPosition, graphPositionWidth);
            return barChartVisualObject;
        }
        private GameObject CreateBar(Vector2 graphPosition, float barWidth)
        {
            GameObject gameObject = new GameObject("bar", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().color = barColor;
            var rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
            rectTransform.sizeDelta = new Vector2(barWidth * barWidthMultiplier, graphPosition.y);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.pivot = new Vector2(.5f, 0f);
            return gameObject;
        }

        public class BarChartVisualObject : IGraphVisualObject
        {
            private GameObject barGameObject;
            private float barWidthMultiplier;
            public BarChartVisualObject(GameObject barGameObject, float barWidthMultiplier)
            {
                this.barGameObject = barGameObject;
                this.barWidthMultiplier = barWidthMultiplier;

            }
            public void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth)
            {
                RectTransform rectTransform = barGameObject.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
                rectTransform.sizeDelta = new Vector2(graphPositionWidth * barWidthMultiplier, graphPosition.y);
            }

            public void CleanUp()
            {
                Destroy(barGameObject);
            }
        }
    }
    */
    private class LineGraphVisual : IGraphVisual
    {
        private RectTransform graphContainer;
        private Sprite dotSprite;
        private LineGraphVisualObject lastLineGraphVisualObject;
        private Color lineColor;
        public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color lineColor)
        {
            this.graphContainer = graphContainer;
            this.dotSprite = dotSprite;
            lastLineGraphVisualObject = null;
            this.lineColor = lineColor;
        }
        
        public void CleanUp() {
            lastLineGraphVisualObject = null;
        }
        public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth)
        {
           // List<GameObject> gameObjectList = new List<GameObject>();
            GameObject dotGameObject = CreateDot(graphPosition, lineColor);

            //gameObjectList.Add(dotGameObject);
            GameObject dotConnectionGameObject = null;
            if (lastLineGraphVisualObject != null)
            {
                dotConnectionGameObject = CreateDotConnection(lastLineGraphVisualObject.GetGraphPosition(),
                    dotGameObject.GetComponent<RectTransform>().anchoredPosition, lineColor);
                //gameObjectList.Add(dotConnectionGameObject);
            }
            
            LineGraphVisualObject lineGraphVisualObject = new LineGraphVisualObject(dotGameObject, dotConnectionGameObject, null);
            lineGraphVisualObject.SetGraphVisualObjectInfo(graphPosition, graphPositionWidth);
            lastLineGraphVisualObject = lineGraphVisualObject;
            return lineGraphVisualObject;
        }
        
        private GameObject CreateDot(Vector2 anchoredPosition, Color color) {
            GameObject gameObject = new GameObject("dot", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.GetComponent<Image>().sprite = dotSprite;
            gameObject.GetComponent<Image>().color = color;
            
            var rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            return gameObject;
        }
    
        private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color color) {
            GameObject dotConnection = new GameObject("dotConnection", typeof(Image));
            dotConnection.transform.SetParent(graphContainer, false);
            dotConnection.GetComponent<Image>().color = color;
            dotConnection.GetComponent<Image>().raycastTarget = false;
            var rectTransform = dotConnection.GetComponent<RectTransform>();
            var dir = (dotPositionB - dotPositionA).normalized;
            var distance = Vector2.Distance(dotPositionA, dotPositionB);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.sizeDelta = new Vector2(distance, 3f);
            rectTransform.anchoredPosition = dotPositionA + (dir * (distance * .5f));
            rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
            return dotConnection;
        }
        
        public class LineGraphVisualObject : IGraphVisualObject
        {
            public event EventHandler OnChangedGraphVisualObjectInfo;
            private GameObject dotGameObject;
            private GameObject dotConnectionGameObject;
            private LineGraphVisualObject lastVisualObject;
            public LineGraphVisualObject(GameObject dotGameObject, GameObject dotConnectionGameObject, LineGraphVisualObject lastVisualObject)
            {
                this.dotGameObject = dotGameObject;
                this.dotConnectionGameObject = dotConnectionGameObject;
                this.lastVisualObject = lastVisualObject;

                if (lastVisualObject != null)
                {
                    lastVisualObject.OnChangedGraphVisualObjectInfo += LastVisualObject_OnChangedGraphVisualObjectInfo;
                }
            }

            private void LastVisualObject_OnChangedGraphVisualObjectInfo(object sender, EventArgs e)
            {
                UpdateDotConnection();
            }

            public void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth)
            {
                var rectTransform = dotGameObject.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = graphPosition;
                UpdateDotConnection();
                if (OnChangedGraphVisualObjectInfo != null) OnChangedGraphVisualObjectInfo(this, EventArgs.Empty);
            }

            public void CleanUp()
            {
                Destroy(dotGameObject);
                Destroy(dotConnectionGameObject);
            }

            public Vector2 GetGraphPosition()
            {
                var rectTransform = dotGameObject.GetComponent<RectTransform>();
                return rectTransform.anchoredPosition;
            }

            private void UpdateDotConnection()
            {
                if (dotConnectionGameObject != null && lastVisualObject !=null)
                {
                    RectTransform dotCOnnectionRectTransform = dotConnectionGameObject.GetComponent<RectTransform>();
                    Vector2 dir = (lastVisualObject.GetGraphPosition() - GetGraphPosition()).normalized;
                    float distance = Vector2.Distance(GetGraphPosition(), lastVisualObject.GetGraphPosition());
                    dotCOnnectionRectTransform.sizeDelta = new Vector2(distance, 3f);
                    dotCOnnectionRectTransform.anchoredPosition = GetGraphPosition() + dir * distance * .5f;
                    dotCOnnectionRectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
                }
            }
        }
    }
  }
}

