using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using UnityEngine;

public class load_scan : MonoBehaviour
{
    public String filename;
    private List<Vector3> point_list = new List<Vector3>();
    private List<Color> color_list = new List<Color>();
    private Mesh mesh;
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
        string path = @"C:\Users\shawlab\Documents\Git\SAL_FARO_Unity\SAL_Scan\Assets\Scans\" + filename;

        //Read the text from directly from the .xyz file
        StreamReader reader = new StreamReader(path);

        // Instantiate point cloud mesh
        mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;

        // Read the .xyz file
        while (!reader.EndOfStream)
        {
            string[] s = reader.ReadLine().Split(' ');
            if (s.Length == 8)
            {
                point_list.Add(new Vector3(float.Parse(s[2]), float.Parse(s[3]), float.Parse(s[4])));
                color_list.Add(new Color(float.Parse(s[5]), float.Parse(s[6]), float.Parse(s[7])));
            }
            else {
                point_list.Add(new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2])));
                color_list.Add(new Color(float.Parse(s[3]), float.Parse(s[4]), float.Parse(s[5])));
            }
        }

        // Close I/O
        reader.Close();

        CreateMesh();
    }

    void CreateMesh()
    {
        // Convert list to array for vertices and colors
        Vector3[] points = point_list.ToArray();
        Color[] colors = color_list.ToArray();

        // Create array of indices for the mesh
        int[] indices = new int[points.Length];
        for (int i = 0; i < indices.Length; ++i)
        {
            indices[i] = i;
        }

        mesh.vertices = points;
        mesh.colors = colors;
        mesh.SetIndices(indices, MeshTopology.Points, 0);

    }
}
