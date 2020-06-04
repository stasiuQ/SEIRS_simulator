using System;
using System.Collections.Generic;
using CodeMonkey.Utils;
using Seirs.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Seirs.Graph
{
  public class WindowGraph : MonoBehaviour { 
      
      private static WindowGraph _instance;
      
      [SerializeField] private Sprite dotSprite; 
      private RectTransform graphContainer; 
      private RectTransform labelTemplateX; 
      private RectTransform labelTemplateY; 
      private List<GameObject> gameObjectList1; 
      private List<IGraphVisualObject> graphVisualObjectList1; 
      private List<GameObject> gameObjectList2; 
      private List<IGraphVisualObject> graphVisualObjectList2; 
      private List<GameObject> gameObjectList3; 
      private List<IGraphVisualObject> graphVisualObjectList3; 
      private List<GameObject> gameObjectList4; 
      private List<IGraphVisualObject> graphVisualObjectList4;

      private List<int> valueList1; 
      private List<int> valueList2; 
      private List<int> valueList3; 
      private List<int> valueList4;

      private IGraphVisual graphVisual1; 
      private IGraphVisual graphVisual2; 
      private IGraphVisual graphVisual3; 
      private IGraphVisual graphVisual4; 

      private float yMaximum = Globals.AgentNumbers; 
      private float xSize; 
      private void Awake()
    {
        _instance = this;
        if (GetGraphContainer() || GetLabelsXy()) return;
        
        gameObjectList1 =  new List<GameObject>();
        graphVisualObjectList1 = new List<IGraphVisualObject>();
        gameObjectList2 =  new List<GameObject>();
        graphVisualObjectList2 = new List<IGraphVisualObject>();
        gameObjectList3 =  new List<GameObject>();
        graphVisualObjectList3 = new List<IGraphVisualObject>();
        gameObjectList4 =  new List<GameObject>();
        graphVisualObjectList4 = new List<IGraphVisualObject>();
        
        valueList1 = new List<int>(new int[Globals.Steps]);
        valueList2 = new List<int>(new int[Globals.Steps]);
        valueList3 = new List<int>(new int[Globals.Steps]);
        valueList4 = new List<int>(new int[Globals.Steps]);

        Globals.ClearChartMethod = ClearCharts;
        Globals.UpdateChartMethod = UpdateChart;
        graphVisual1 = new LineGraphVisual(graphContainer, dotSprite, Color.green);
        graphVisual2 = new LineGraphVisual(graphContainer, dotSprite, Color.yellow);
        graphVisual3 = new LineGraphVisual(graphContainer, dotSprite, Color.red);
        graphVisual4 = new LineGraphVisual(graphContainer, dotSprite, Color.blue);
        ClearCharts();
        ShowChart();
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
        if (Globals.Stats[5] <= Globals.Steps)
        {
            valueList1 = UpdateValue(valueList1, graphVisual1, graphVisualObjectList1, stats[5], stats[1]);
            valueList2 = UpdateValue(valueList2, graphVisual2, graphVisualObjectList2,stats[5], stats[2]);
            valueList3 = UpdateValue(valueList3, graphVisual3, graphVisualObjectList3, stats[5], stats[3]);
            valueList4 = UpdateValue(valueList4, graphVisual4, graphVisualObjectList4,stats[5], stats[4]);
        }
    }

    public void ShowChart()
    {
        ShowGraph(valueList1, gameObjectList1, graphVisualObjectList1, graphVisual1);
        ShowGraph(valueList2, gameObjectList2, graphVisualObjectList2, graphVisual2);
        ShowGraph(valueList3, gameObjectList3, graphVisualObjectList3, graphVisual3);
        ShowGraph(valueList4, gameObjectList4, graphVisualObjectList4, graphVisual4);
    }
    private void ShowGraph(List<int> valueList, List<GameObject> gameObjectList, List<IGraphVisualObject> graphVisualObjectList, IGraphVisual graphVisual) {
        
        ClearChart(graphVisual, gameObjectList, graphVisualObjectList);
        var graphHeight = graphContainer.sizeDelta.y;
        var graphWidth = graphContainer.sizeDelta.x;
        xSize = graphWidth/Globals.Steps;
        
        DrawPoints(valueList, graphVisual, gameObjectList, graphVisualObjectList, xSize, yMaximum, graphHeight);
    }

    public void ClearCharts()
    {
        ClearChart(graphVisual1, gameObjectList1, graphVisualObjectList1);
        ClearChart(graphVisual1, gameObjectList2, graphVisualObjectList2);
    }
    private void ClearChart(IGraphVisual graphVisual, List<GameObject> gameObjectList, List<IGraphVisualObject> graphVisualObjectList )
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
    
    private void DrawPoints(IReadOnlyList<int> valueList, IGraphVisual graphVisual, List<GameObject> gameObjectList, List<IGraphVisualObject> graphVisualObjectList, float xSize, float yMaximum, float graphHeight)
    {
        int proportionX = Globals.Steps / 10;
        int proportionY = Globals.AgentNumbers / 10;
        for (var i = 0; i < valueList.Count; i++)
        {
            var xPosition = i * xSize;
            var yPosition = (valueList[i] / yMaximum) * graphHeight;
            graphVisualObjectList.Add(graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize));
            if (i % proportionX == 0)
            {
                RectTransform labelX = Instantiate(labelTemplateX);
                labelX.SetParent(graphContainer, false);
                labelX.gameObject.SetActive(true);
                labelX.anchoredPosition = new Vector2(xPosition, -7f);
                labelX.GetComponent<Text>().text = i.ToString();
                gameObjectList.Add(labelX.gameObject);
            }
        }
        int separatorCount = Globals.Steps;
        for (var i = 0; i <= separatorCount; i+= separatorCount/proportionY)
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
    private List<int> UpdateValue(List<int> valueList, IGraphVisual graphVisual, List<IGraphVisualObject> graphVisualObjectList,  int index, int value)
    {
        Debug.Log(index.ToString());
        if (index < Globals.Steps-1)
        {
            float graphHeight = graphContainer.sizeDelta.y;
            valueList[index] = value;
            var xPosition = index * xSize;
            var yPosition = (value/ yMaximum) * graphHeight;
            graphVisualObjectList[index].SetGraphVisualObjectInfo(new Vector2(xPosition, yPosition), xSize);
        }
        return valueList;
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
            GameObject dotGameObject = CreateDot(graphPosition, lineColor);

            //gameObjectList.Add(dotGameObject);
            GameObject dotConnectionGameObject = null;
            if (lastLineGraphVisualObject != null)
            {
                //dotConnectionGameObject = CreateDotConnection(lastLineGraphVisualObject.GetGraphPosition(), dotGameObject.GetComponent<RectTransform>().anchoredPosition, lineColor);
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
            rectTransform.sizeDelta = new Vector2(5, 5);
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

