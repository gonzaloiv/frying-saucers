using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PDollarGestureRecognizer;
using System.Linq;

public class GestureRecognizer {

    #region Fields

    public int StrokeIndex { get { return strokeIndex; } } 

    private List<Gesture> trainingSet;
    private List<Point> points;
    private int strokeIndex;
    private Vector2 currentPointPosition;

    #endregion

    #region Public Behaviour

    public GestureRecognizer () {
        this.trainingSet = new List<Gesture>();
        this.points = new List<Point>();
        this.strokeIndex = 0;
        this.currentPointPosition = Vector2.zero;
        foreach (TextAsset gestureXml in Resources.LoadAll<TextAsset>("GestureSet/"))
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
    }

    public void NewLine () {
        strokeIndex++;
    }

    public void NewPoint (Vector2 position) {
        if (currentPointPosition == position)
            return;
        currentPointPosition = position;
        points.Add(new Point(position.x, -position.y, strokeIndex));
    }

    public Result RecognizeGesture () {
        return PointCloudRecognizer.Classify(new Gesture(points.ToArray()), trainingSet.ToArray());
    }

    public void Reset () {
        strokeIndex = 0;
        points.Clear();
    }

    #endregion

}