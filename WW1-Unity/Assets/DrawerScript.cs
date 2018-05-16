using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour {

    Vector3 vectorBegin = Vector3.zero;
    Vector3 vectorEnd = new Vector3(14, 3, 3);

    // Use this for initialization
    void Start () {

        var fileData = System.IO.File.ReadAllText("Assets/places_gicentre.csv");
        var lines  = fileData.Split("\n"[0]);
        var lineData  = lines[1].Split(","[0]);
        string x = lineData[0];
        Debug.Log(fileData);
        Debug.Log(lines);
        Debug.Log(lineData);
        Debug.Log(x);
    }

    // Update is called once per frame
    void Update () {
        var valRed = Random.value;
        var valGreen = Random.value;
        var valBlue= Random.value;
        DrawLine(vectorBegin, vectorEnd, new Color(valRed, valGreen, valBlue), 0.1f);
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
}
