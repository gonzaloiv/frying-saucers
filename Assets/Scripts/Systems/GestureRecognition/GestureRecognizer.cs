using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PDollarGestureRecognizer;
using System.Linq;

public class GestureRecognizer : MonoBehaviour {

    #region Fields

    private List<Gesture> trainingSet = new List<Gesture>();

    public int CurrentPointAmount { get { return points.Count; } }
    private List<Point> points = new List<Point>();

    private int strokeID = 0;
    private int vertexCount = 0;
    private Vector2 currentPointPosition = Vector2.zero;

    #endregion

    #region Mono Behaviour

    void Awake () {
        foreach (TextAsset gestureXml in Resources.LoadAll<TextAsset>("GestureSet/"))
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
    }

    #endregion

    #region Public Behaviour

    public void NewLine (Transform position) {
        ++strokeID;
        vertexCount = 0;
    }

    public void NewPoint (Vector2 position) {
        if (currentPointPosition == position)
            return;
        ++vertexCount;
        currentPointPosition = position;
        points.Add(new Point(position.x, -position.y, strokeID));
    }

    public Result RecognizeGesture () {
        return PointCloudRecognizer.Classify(new Gesture(points.ToArray()), trainingSet.ToArray());
    }

    public void ResetGestureLines () {
        strokeID = 0;
        points.Clear();
    }

    #endregion

}