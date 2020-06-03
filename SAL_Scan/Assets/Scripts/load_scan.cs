using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using UnityEngine;

public class load_scan : MonoBehaviour
{
    public GameObject clone;
    private List<Vector3> point_list = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        string path = @"C:\Users\dakil\Documents\Unity\SAL_Scan\Assets\Scans\faro_scan_005.xyz";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            string[] s = reader.ReadLine().Split(' ');
            point_list.Add(new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2])));
        }

        for (int i = 0; i < point_list.Count; i++) {
            Instantiate(clone, point_list[i], transform.rotation);
        }

        reader.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
