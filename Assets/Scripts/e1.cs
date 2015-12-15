using UnityEngine;
using System.Collections;

public class e1 : MonoBehaviour {

    [HideInInspector]
    int _ownerId; // To ensure they have a unique mesh
    MeshFilter _mf;
    MeshFilter MeshFilter
    { // Tries to find a mesh filter, adds one if it doesn't exist yet
        get
        {
            _mf = _mf ?? GetComponent<MeshFilter>();
            _mf = _mf ?? gameObject.AddComponent<MeshFilter>();
            return _mf;
        }
    }
    Mesh _mesh;
    protected Mesh Mesh
    { // The mesh to edit
        get
        {
            if (_mesh == null)
            {
                //bool isOwner = _ownerId == gameObject.GetInstanceID();
                if (MeshFilter.sharedMesh == null /* || !isOwner*/)
                {
                    MeshFilter.sharedMesh = _mesh = new Mesh();
                    _ownerId = gameObject.GetInstanceID();
                    _mesh.name = "Mesh [" + _ownerId + "]";
                }
                else
                {
                    _mesh = MeshFilter.sharedMesh;
                }
            }
            return _mesh;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3[] vertices = Mesh.vertices;
        Vector3[] normals = Mesh.normals;
        int i = 0;
        while (i < vertices.Length)
        {
            vertices[i] += normals[i] * Mathf.Sin(Time.time);
            i++;
        }
        Mesh.vertices = vertices;
    }
}
