﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public Transform origin, curvePoint, controlPoint;
    private Vector3 originPos, curvePointPos, controlPointPos;
    private Quaternion controlRotation, startRotation;
    private float timeAlive, timeCreated, curveTime = 0;
    private bool curving = true;
    private double curveDistance;
    public float speed = 5;
    

    public void SetupBullet(float providedSpeed, Transform providedOrigin, Transform providedCurvepoint, Transform providedControlPoint)
    {

        speed = providedSpeed; //set the speed
        origin = providedOrigin;  //set this bullets curve variables
        curvePoint = providedCurvepoint;
        controlPoint = providedControlPoint;
        controlRotation = providedControlPoint.rotation;

        originPos = origin.position; 
        controlPointPos = controlPoint.position;
        curvePointPos = curvePoint.position + (Random.insideUnitSphere * .1f); //set the curvepoint randomly to make a new curve every time

        transform.LookAt(curvePoint); // look at the conrol point
        startRotation = transform.rotation; // save the new rotation
        curveDistance = calculateQuatraticBezierPointDistance(originPos, curvePointPos, controlPointPos);
        Debug.Log(curveDistance);
    }

    

    // Use this for initialization
    void Start () {
        
        
    }

    private Vector3 calculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return (Mathf.Pow(1 - t, 2) * p0) + (((2 * (1 - t)) * t) * p1) + ((Mathf.Pow(t, 2) * p2)); //calculates and returns a quadratic bezier point

    }

    private double calculateQuatraticBezierPointDistance(Vector3 p0, Vector3 p1, Vector3 p2)
    {
        double distance = 0;
        Vector3 tempVector0 = p0, tempVector1;
        for (int i = 0; i<=100; i++)
        {
            float t = i / 100;
            tempVector1 = calculateQuadraticBezierPoint(t, p0, p1, p2);
            distance = distance + Vector3.Distance(tempVector0, tempVector1);
            tempVector0 = tempVector1;
        }
        return distance;
    }

    // Update is called once per frame
    void Update () {

        if (curveTime < 1)
        {
            curveTime += Time.deltaTime / 0.1f;
            transform.position = calculateQuadraticBezierPoint(curveTime, originPos, curvePointPos, controlPointPos);
            transform.rotation = Quaternion.Slerp(startRotation, controlRotation, (curveTime));
        }
        else
        {
            if (curving == true)
            {
                transform.rotation = controlRotation;
                curving = false;
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        
        
	}
}
