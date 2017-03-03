using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PDollarGestureRecognizer;

public class GestureManager : MonoBehaviour {

  #region Fields

  public Transform gestureOnScreenPrefab;

  private List<Gesture> trainingSet = new List<Gesture>();

  private List<Point> points = new List<Point>();
  private int strokeId = -1;

  private Vector3 virtualKeyPosition = Vector2.zero;
  private Rect drawArea;

  private RuntimePlatform platform;
  private int vertexCount = 0;

  private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
  private LineRenderer currentGestureLineRenderer;

  // TODO: corregir el uso de banderas de la demo...
  private bool recognized = false;
  private bool listening = false;

  #endregion

  #region Mono Behaviour

  void Awake() {
    platform = Application.platform;
    drawArea = new Rect(0, 0, Screen.width, Screen.height);

    //Load gestures
    TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/");
    foreach (TextAsset gestureXml in gesturesXml)
      trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
  }

  void Update() {

    if (recognized)
      Reset(); 

    if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
      if (Input.touchCount > 0) {
        virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
      }
    } else {
      if (Input.GetMouseButton(0)) {
        virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
      }
    }

    if (drawArea.Contains(virtualKeyPosition)) {

      if (Input.GetMouseButtonDown(0)) {

        if (!listening)
          StartCoroutine(ListenToGestures());

        ++strokeId;
        
        Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
        currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
        currentGestureLineRenderer.sortingLayerName = SortingLayer.UI.ToString();
        
        gestureLinesRenderer.Add(currentGestureLineRenderer);
        
        vertexCount = 0;
      }
      
      if (Input.GetMouseButton(0)) {
        points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));
        currentGestureLineRenderer.SetVertexCount(++vertexCount);
        currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
      }
    }

  }

  void OnDisable() {
    StopAllCoroutines();
  }

  #endregion

  #region Private Behaviour

  private IEnumerator ListenToGestures() {
    listening = true;
    yield return new WaitForSeconds(Config.GESTURE_TIME);

    Result result = PointCloudRecognizer.Classify(new Gesture(points.ToArray()), trainingSet.ToArray());
    if (result.Score < Config.GESTURE_MIN_SCORE)
      EventManager.TriggerEvent(new WrongGestureInput(result));
    else
      EventManager.TriggerEvent(new GestureInput(result));

    recognized = true;
    listening = false;
  }

  private void Reset() {
    recognized = false;
    strokeId = -1;
    points.Clear();

    foreach (LineRenderer lineRenderer in gestureLinesRenderer) {
      lineRenderer.SetVertexCount(0);
      Destroy(lineRenderer.gameObject);
    }

    gestureLinesRenderer.Clear();
  }

  #endregion

}