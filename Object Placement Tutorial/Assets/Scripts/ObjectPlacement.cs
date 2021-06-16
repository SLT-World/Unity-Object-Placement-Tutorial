using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public Material Green;
    public Material[] OriMats;

    bool Follow;

    GameObject _camera;
    GameObject _go;

    float MouseWheelRotation;

    void Start()
    {
        _camera = Camera.main.gameObject;
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward), out hit, 10000))
        {
            //Draw a line
            Debug.DrawRay(_camera.transform.position, _camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
        }

        if (Follow)
        {
            _go.transform.position = hit.point;
            MouseWheelRotation = Input.mouseScrollDelta.y;
            _go.transform.Rotate(Vector3.up, MouseWheelRotation * 20f);
            if (Input.GetMouseButtonDown(0))
            {
                _go.GetComponent<MeshRenderer>().materials = OriMats;
                Follow = false;
                Mesh mesh = _go.GetComponent<MeshFilter>().mesh;

                if (mesh != null)
                {
                    MeshCollider meshCollider = _go.AddComponent<MeshCollider>();

                    meshCollider.sharedMesh = mesh;
                }
            }
        }
    }

    public void NewObject(GameObject ObjectPrefab)
    {
        GameObject go = Instantiate(ObjectPrefab);
        _go = go;
        Material[] materials = go.GetComponent<MeshRenderer>().materials;
        var mats = new Material[materials.Length];
        for (var j = 0; j < materials.Length; j++)
        {
            mats[j] = Green;
        }
        go.GetComponent<MeshRenderer>().materials = mats;
        Follow = true;
    }
}
