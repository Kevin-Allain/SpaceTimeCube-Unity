using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour {

    Vector3 vectorBegin = Vector3.zero;
    Vector3 vectorEnd = new Vector3(14, 3, 3);

    static float wrongTimeUp = 0;

    // Use this for initialization
    void Start () {

        var fileData = System.IO.File.ReadAllText("Assets/places_gicentre.csv");
        var lines  = fileData.Split("\n"[0]);
        Debug.Log("lines.Length: "); Debug.Log(lines.Length);
        var lineData  = lines[1].Split(","[0]);
        Debug.Log(fileData);
        Debug.Log(lines); Debug.Log(lineData);

        float minLatitude = float.Parse(lines[1].Split(","[0])[8]);
        float minLongitude = float.Parse(lines[1].Split(","[0])[8]);
        float maxLatitude = minLatitude;
        float maxLongitude = minLongitude;


        for ( var i = 1; i < lines.Length -2; i++)
        {
            var lineIndex0 = lines[i].Split(","[0]);
            var futureLine= lines[i+1].Split(","[0]);
            Debug.Log("lineIndex0: "); Debug.Log(lineIndex0);
            Debug.Log("i: " + i);
            float longitude = float.Parse( lineIndex0[7] );
            float latitude = float.Parse( lineIndex0[8] );
            float futureLongitude = float.Parse(futureLine[7]);
            float futureLatitude = float.Parse(futureLine[8]);

            Vector3 currentVec = new Vector3(longitude, wrongTimeUp, latitude);
            wrongTimeUp +=.1f;
            Vector3 futureVec = new Vector3(futureLongitude, wrongTimeUp, futureLatitude);

            DrawLine(currentVec, futureVec, new Color(1, 0, 0, .4f));

            if (minLatitude > latitude) minLatitude = latitude;
            if (maxLatitude < latitude) maxLatitude = latitude;
            if (minLongitude> longitude) minLongitude = longitude;
            if (maxLongitude < longitude) maxLongitude = longitude;


        }
        Debug.Log("minLatitude: " + minLatitude + ", maxLatitude: " + maxLatitude);
        Debug.Log("minLongitude: " + minLongitude + ", maxLongitude: " + maxLongitude);
    }

    // Update is called once per frame
    void Update () {

    }


    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = -1f)
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
        if (duration != -1f) { GameObject.Destroy(myLine, duration); }
    }
}
