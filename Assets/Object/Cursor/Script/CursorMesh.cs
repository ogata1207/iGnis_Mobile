using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class CursorMesh : MonoBehaviour {

    [SerializeField]
    private Material mat;

    private void Start()
    {
        var mesh = new Mesh();

        //頂点生成
        mesh.vertices = new Vector3[] 
        {
        new Vector3(0, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(1, 1, 0),
        new Vector3(1, 0, 0)
        };

        //頂点をセット
        mesh.triangles = new int[] 
        {
         0, 1, 3,
         2, 3, 1
        };

        //生成したメッシュをセット
        var filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;

        //マテリアルをセット
        var renderer = GetComponent<MeshRenderer>();
        renderer.material = mat;
    }
}
