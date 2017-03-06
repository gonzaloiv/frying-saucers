using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PDollarGestureRecognizer;
using System.Linq;

public class GestureRecognizer : MonoBehaviour {

  #region Fields

  private GestureSpawner gestureSpawner;
  private List<Gesture> trainingSet = new List<Gesture>();

  public int CurrentPointAmount { get { return points.Count; } } 
  private List<Point> points = new List<Point>();

  private List<LineRenderer> gestureLines = new List<LineRenderer>();
  private LineRenderer currentGestureLine;

  private int strokeId = 0;
  private int vertexCount = 0;
  private Vector2 currentPointPosition = Vector2.zero;

  #endregion

  #region Mono Behaviour

  void Awake() {
    gestureSpawner = GetComponent<GestureSpawner>();
    foreach (TextAsset gestureXml in Resources.LoadAll<TextAsset>("GestureSet/"))
      trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
  }

  #endregion

  #region Public Behaviour

  public void NewLine(Transform position) {
    if (gestureLines.Count < 2) { // Problems with time based recognition
      currentGestureLine = gestureSpawner.SpawnGestureLineRenderer(position);
      gestureLines.Add(currentGestureLine);

      ++strokeId;
      vertexCount = 0;
    }
  }

  public void NewPoint(Vector2 position) {

    if(currentPointPosition != position) {

      ++vertexCount;

      currentPointPosition = position;
      points.Add(new Point(position.x, -position.y, strokeId));

      Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 1));
      currentGestureLine.numPositions = vertexCount;
      currentGestureLine.SetPosition(vertexCount - 1, worldPosition);

    }

  }

  public Result RecognizeGesture() {
    return PointCloudRecognizer.Classify(new Gesture(points.ToArray()), trainingSet.ToArray());
  }

  public void ResetGestureLines() {
    strokeId = 0;
    points.Clear();

    foreach (LineRenderer lineRenderer in gestureLines) {
      lineRenderer.numPositions = 0;
      lineRenderer.gameObject.SetActive(false);
    }

    gestureLines.Clear();
  }

  #endregion

}