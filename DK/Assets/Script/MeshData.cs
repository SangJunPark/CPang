using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData {

    private List<Vector3> verts = new List<Vector3>();
    private List<int> tris = new List<int>();
    private List<Vector2> uvs = new List<Vector2>();

    public MeshData(List<Vector3> v, List<int> i, List<Vector2> uv)
    {
        verts = v;
        tris = i;
        uvs = uv;
    }

    public MeshData()
    {

    }

    public void AddPos(Vector3 pos)
    {
        for (int i = 0; i < verts.Count; ++i)
            verts[i] += pos;
    }

    public void Merge(MeshData mesh)
    {
        if (mesh.verts.Count <= 0)
            return;

        if (verts.Count <= 0)
        {
            verts = mesh.verts;
            tris = mesh.tris;
            uvs = mesh.uvs;
            return;
        }
        int count = verts.Count;

        verts.AddRange(mesh.verts);
        for (int i = 0; i < mesh.tris.Count; ++i)
            tris.Add(mesh.tris[i] + count);
        uvs.AddRange(mesh.uvs);
    }

    public Mesh ToMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        //UnityEditor.MeshUtility.Optimize(mesh); // only in editor
        return mesh;
    }
}
