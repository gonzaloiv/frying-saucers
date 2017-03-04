using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PDollarGestureRecognizer;

public class GestureRecognizer : MonoBehaviour {

  #region Fields

  private GestureSpawner gestureSpawner;
  private List<Gesture> trainingSet = new List<Gesture>();

  private List<LineRenderer> gestureLines = new List<LineRenderer>();
  private LineRenderer currentGestureLine;
  private List<Point> points = new List<Point>();

  private int strokeId = 0;
  private int vertexCount = 0;

  #endregion

  #region Mono Behaviour

  void Awake() {
    gestureSpawner = GetComponent<GestureSpawner>();
    foreach (TextAsset gestureXml in Resources.LoadAll<TextAsset>("GestureSet/"))
      trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
  }

  #endregion

  #region Public Behaviour

  public void NewLine(Transform p) {
    currentGestureLine = gestureSpawner.SpawnGestureLineRenderer(transform);
    gestureLines.Add(currentGestureLine);
    
    ++strokeId;
    vertexCount = 0;
  }

  public void NewPoint(Vector2 position) {
    ++vertexCount;
    points.Add(new Point(position.x, -position.y, strokeId));
    currentGestureLine.numPositions = vertexCount;
    currentGestureLine.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 1)));
  }

  public Result RecognizeGesture() {    
    Result result = PointCloudRecognizer.Classify(new Gesture(points.ToArray()), trainingSet.ToArray());
    ResetGestureLines();
    
    return result;
  }

  #endregion

  #region Private Behaviour

  private void ResetGestureLines() {
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