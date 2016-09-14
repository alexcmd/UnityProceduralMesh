using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Extruder : MonoBehaviour {
    private BezierSpline _spline;
    public GameObject Obj;
    private int _stepsPerCurve = 10;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    void Awaik()
    {
        
    }

    // Use this for initialization
	void Start ()
	{
	    _meshFilter = Obj.GetComponent<MeshFilter>();
	    _meshRenderer = Obj.GetComponent<MeshRenderer>();
        _spline = GetComponent<BezierSpline>();

	    
        



        Vector3 point = _spline.GetPoint(0f);
	    Obj.transform.position = point;
        Obj.transform.LookAt(point + _spline.GetDirection(0f));
        var meshVertices = _meshFilter.mesh.vertices;
        var meshNormals = _meshFilter.mesh.normals;
        var meshTriangles = _meshFilter.mesh.triangles;
        var maxZ = _meshFilter.mesh.bounds.max.z;
	    var vector3S = new List<int>();
        var newVerticies = new List<Vector3>();
        var newNormals = new List<Vector3>();
        for (int i = 0; i < meshVertices.Length; i++)
	    {
	        if (meshNormals[i] != Vector3.forward)
	        {
                newVerticies.Add(meshVertices[i]);
                newNormals.Add(meshNormals[i]);
            }
	        else
	        {
                vector3S.Add(i);
            }
	        


        }
        var triags = new List<int>();
        for (int i = 0; i < meshTriangles.Length; i++)
	    {
	        if (vector3S.All(x => x != meshTriangles[i]))
                continue;
	        triags.Add(meshTriangles[i]);
	        i++;
	        triags.Add(meshTriangles[i]);
	        i++;
	        triags.Add(meshTriangles[i]);
	    }

	    _meshFilter.mesh.triangles =  meshTriangles.Except(triags).ToArray();
	    _meshFilter.mesh.vertices = newVerticies.ToArray();
        _meshFilter.mesh.normals = newNormals.ToArray();

        var steps = _stepsPerCurve * _spline.CurveCount;
        for (int i = 1; i <= steps; i++)
        {
            point = _spline.GetPoint(i / (float)steps);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}


