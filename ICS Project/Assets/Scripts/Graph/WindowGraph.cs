using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Seirs.Graph
{
    public class WindowGraph : MonoBehaviour
    {
        private static WindowGraph _instance;

        [SerializeField] private Sprite dotSprite;
        private List<GameObject> gameObjectList1;
        private List<GameObject> gameObjectList2;
        private List<GameObject> gameObjectList3;
        private List<GameObject> gameObjectList4;
        private RectTransform graphContainer;

        private LineGraphVisual graphVisual1;
        private LineGraphVisual graphVisual2;
        private LineGraphVisual graphVisual3;
        private LineGraphVisual graphVisual4;
        private List<IGraphVisualObject> graphVisualObjectList1;
        private List<IGraphVisualObject> graphVisualObjectList2;
        private List<IGraphVisualObject> graphVisualObjectList3;
        private List<IGraphVisualObject> graphVisualObjectList4;
        private RectTransform labelTemplateX;
        private RectTransform labelTemplateY;

        private List<int> valueList1;
        private List<int> valueList2;
        private List<int> valueList3;
        private List<int> valueList4;
        private float xSize;
        private float yMaximum;

        private void Awake()
        {
            _instance = this;
            Globals.InitChartMethod = InitChart;
            Globals.ClearChartMethod = ClearCharts;
            Globals.UpdateChartMethod = UpdateChart;
            //InitChart();
            //ClearCharts();
        }

        public void InitChart()
        {
            if (GetGraphContainer() || GetLabelsXy()) return;
            yMaximum = (int) (Globals.Parameters.Size * Globals.Parameters.Size * Globals.Parameters.Concentration /
                              (Math.PI * Globals.Parameters.Radius * Globals.Parameters.Radius)) + 1;
            gameObjectList1 = new List<GameObject>();
            graphVisualObjectList1 = new List<IGraphVisualObject>();
            gameObjectList2 = new List<GameObject>();
            graphVisualObjectList2 = new List<IGraphVisualObject>();
            gameObjectList3 = new List<GameObject>();
            graphVisualObjectList3 = new List<IGraphVisualObject>();
            gameObjectList4 = new List<GameObject>();
            graphVisualObjectList4 = new List<IGraphVisualObject>();

            valueList1 = new List<int>(new int[Globals.MaxPointDraw]);
            valueList2 = new List<int>(new int[Globals.MaxPointDraw]);
            valueList3 = new List<int>(new int[Globals.MaxPointDraw]);
            valueList4 = new List<int>(new int[Globals.MaxPointDraw]);
 
            graphVisual1 = new LineGraphVisual(graphContainer, dotSprite, Color.green);
            graphVisual2 = new LineGraphVisual(graphContainer, dotSprite, Color.yellow);
            graphVisual3 = new LineGraphVisual(graphContainer, dotSprite, Color.red);
            graphVisual4 = new LineGraphVisual(graphContainer, dotSprite, Color.blue);
            ShowChart();
        }

        private bool GetLabelsXy()
        {
            if (graphContainer is null)
            {
                return true;
            }

            labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
            labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();

            if (!(labelTemplateX is null) && !(labelTemplateY is null)) return false;
            return true;
        }

        private bool GetGraphContainer()
        {
            graphContainer = transform.Find("graphContainer")?.GetComponent<RectTransform>();
            return false;
        }

        public void UpdateChart()
        {
            if (Globals.Stats[5] >= Globals.Steps ||
                Globals.Stats[5] % (Globals.Steps / Globals.MaxPointDraw) != 0 ||
                Globals.CurrentStep >= Globals.MaxPointDraw) return;

            //Debug.Log($"Stats step: {Globals.Stats[5]}, Current step: {Globals.CurrentStep}");

            valueList1 = UpdateValue(valueList1, graphVisual1, graphVisualObjectList1, Globals.CurrentStep,
                Globals.Stats[1]);
            valueList2 = UpdateValue(valueList2, graphVisual2, graphVisualObjectList2, Globals.CurrentStep,
                Globals.Stats[2]);
            valueList3 = UpdateValue(valueList3, graphVisual3, graphVisualObjectList3, Globals.CurrentStep,
                Globals.Stats[3]);
            valueList4 = UpdateValue(valueList4, graphVisual4, graphVisualObjectList4, Globals.CurrentStep,
                Globals.Stats[4]);
            Globals.CurrentStep += 1;
        }

        public void ShowChart()
        {
            ShowGraph(valueList1, gameObjectList1, graphVisualObjectList1, graphVisual1);
            ShowGraph(valueList2, gameObjectList2, graphVisualObjectList2, graphVisual2);
            ShowGraph(valueList3, gameObjectList3, graphVisualObjectList3, graphVisual3);
            ShowGraph(valueList4, gameObjectList4, graphVisualObjectList4, graphVisual4);
        }

        private void ShowGraph(List<int> valueList, List<GameObject> gameObjectList,
            List<IGraphVisualObject> graphVisualObjectList, LineGraphVisual graphVisual)
        {
            ClearChart(graphVisual, gameObjectList, graphVisualObjectList);
            var graphHeight = graphContainer.sizeDelta.y;
            var graphWidth = graphContainer.sizeDelta.x;
            xSize = graphWidth / Globals.MaxPointDraw;
            DrawPoints(valueList, graphVisual, gameObjectList, graphVisualObjectList, xSize, yMaximum, graphHeight);
        }

        public void ClearCharts()
        {
            ClearChart(graphVisual1, gameObjectList1, graphVisualObjectList1);
            ClearChart(graphVisual2, gameObjectList2, graphVisualObjectList2);
            ClearChart(graphVisual3, gameObjectList3, graphVisualObjectList3);
            ClearChart(graphVisual4, gameObjectList4, graphVisualObjectList4);
        }

        private void ClearChart(LineGraphVisual graphVisual, List<GameObject> gameObjectList, List<IGraphVisualObject> graphVisualObjectList)
        {
            foreach (var gameObject in gameObjectList) Destroy(gameObject);
            gameObjectList.Clear();
            foreach (var graphVisualObject in graphVisualObjectList) graphVisualObject.CleanUp();
            graphVisualObjectList.Clear();
            graphVisual.CleanUp();
        }

        private void DrawPoints(IReadOnlyList<int> valueList, LineGraphVisual graphVisual,
            List<GameObject> gameObjectList, List<IGraphVisualObject> graphVisualObjectList, float xSize,
            float yMaximum, float graphHeight)
        {
            int proportionX = (int)(Globals.Steps / 10);
            int proportionY = (int)yMaximum / 10;
            var yValues = new int[11];
            var xValues = new int[11];
            int counterY = 0;
            int counterX = 0;
            for (var i = 0; i < 11; i++)
            {
                xValues[i] = counterX;
                counterX += proportionX;
                yValues[i] = counterY;
                counterY += proportionY;
            }

            var pointer = 0;
            for (var i = 0; i < valueList.Count; i++)
            {
                var xPosition = i * xSize;
                var yPosition = valueList[i] / yMaximum * graphHeight;
                
                graphVisualObjectList.Add(
                    graphVisual.CreateGraphVisualObject(new Vector2(xPosition, yPosition), xSize));
                
                if (i % 75 == 0)
                {
                    var labelX = Instantiate(labelTemplateX, graphContainer, false);
                    labelX.gameObject.SetActive(true);
                    labelX.anchoredPosition = new Vector2(xPosition, -7f);
                    labelX.GetComponent<Text>().text = xValues[pointer].ToString();
                    gameObjectList.Add(labelX.gameObject);
                    pointer++;
                }

                if (i == valueList.Count - 1)
                {
                    var labelX = Instantiate(labelTemplateX, graphContainer, false);
                    labelX.gameObject.SetActive(true);
                    labelX.anchoredPosition = new Vector2(xPosition, -7f);
                    labelX.GetComponent<Text>().text = xValues[xValues.Length-1].ToString();
                    gameObjectList.Add(labelX.gameObject);
                }
                
                
            }

            var separatorCount = 10;
            for (var i = 0; i <= separatorCount; i ++)
            {
                var labelY = Instantiate(labelTemplateY);
                labelY.SetParent(graphContainer, false);
                labelY.gameObject.SetActive(true);
                var normalizedValue = i * 1f / 10;
                labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
                labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMaximum).ToString();
                gameObjectList.Add(labelY.gameObject);
            }
        }

        private List<int> UpdateValue(List<int> valueList, LineGraphVisual graphVisual,
            List<IGraphVisualObject> graphVisualObjectList, int index, int value)
        {
            if (index < Globals.Steps - 1)
            {
                var graphHeight = graphContainer.sizeDelta.y;
                valueList[index] = value;
                var xPosition = index * xSize;
                var yPosition = value / yMaximum * graphHeight;
                graphVisualObjectList[index].SetGraphVisualObjectInfo(new Vector2(xPosition, yPosition), xSize);
                graphVisualObjectList[index].SetVisible();
                
            }

            return valueList;
        }

        private interface IGraphVisualObject
        {
            void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth);
            void CleanUp();
            void SetVisible();
        }


        private class LineGraphVisual
        {
            private readonly Sprite dotSprite;
            private readonly RectTransform graphContainer;
            private readonly Color lineColor;

            public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color lineColor)
            {
                this.graphContainer = graphContainer;
                this.dotSprite = dotSprite;
                this.lineColor = lineColor;
            }

            public void CleanUp()
            {
            }

            public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPosition, float graphPositionWidth)
            {
                var dotGameObject = CreateDot(graphPosition, lineColor);
                var lineGraphVisualObject = new LineGraphVisualObject(dotGameObject);
                lineGraphVisualObject.SetGraphVisualObjectInfo(graphPosition, graphPositionWidth);
                return lineGraphVisualObject;
            }

            private GameObject CreateDot(Vector2 anchoredPosition, Color color)
            {
                var gameObject = new GameObject("dot", typeof(Image));
                gameObject.transform.SetParent(graphContainer, false);
                gameObject.GetComponent<Image>().sprite = dotSprite;
                gameObject.GetComponent<Image>().color = color;
                gameObject.GetComponent<Image>().enabled = false;
                var rectTransform = gameObject.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = anchoredPosition;
                rectTransform.sizeDelta = new Vector2(7, 7);
                rectTransform.anchorMin = new Vector2(0, 0);
                rectTransform.anchorMax = new Vector2(0, 0);
                return gameObject;
            }

            public class LineGraphVisualObject : IGraphVisualObject
            {
                private readonly GameObject dotGameObject;

                public LineGraphVisualObject(GameObject dotGameObject)
                {
                    this.dotGameObject = dotGameObject;
                }

                public void SetGraphVisualObjectInfo(Vector2 graphPosition, float graphPositionWidth)
                {
                    var rectTransform = dotGameObject.GetComponent<RectTransform>();
                    rectTransform.anchoredPosition = graphPosition;
                }

                public void SetVisible()
                {
                    dotGameObject.GetComponent<Image>().enabled = true;
                }
                public void CleanUp()
                {
                    Destroy(dotGameObject);
                }

                public Vector2 GetGraphPosition()
                {
                    var rectTransform = dotGameObject.GetComponent<RectTransform>();
                    return rectTransform.anchoredPosition;
                }
            }
        }
    }
}