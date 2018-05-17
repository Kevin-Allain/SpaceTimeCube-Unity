using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour {

    Vector3 vectorBegin = Vector3.zero;
    Vector3 vectorEnd = new Vector3(14, 3, 3);

    public GameObject regionPlane;

    static float wrongTimeUp = 0;

    // Use this for initialization
    void Start () {

        var picData = System.IO.File.ReadAllText("Assets/mapWW1-region.wld");
        var linesPic = picData.Split("\n"[0]);
        for ( var j = 0; j < linesPic.Length-1; j++)
        {
            Debug.Log("linesPic[j]: " + linesPic[j]);
        }
        Debug.Log("regionPlane.transform.position: "); Debug.Log(regionPlane.transform.position);

        var fileData = System.IO.File.ReadAllText("Assets/places_gicentre.csv");
        var lines  = fileData.Split("\n"[0]);
        Debug.Log("lines.Length: "); Debug.Log(lines.Length);
        var lineData  = lines[1].Split(","[0]);
        Debug.Log(fileData);
        Debug.Log(lines); Debug.Log(lineData);

        DrawLinesAtCenterOfScene();
        DrawLinesAtBordersOfCube();

        float minLatitude = float.Parse(lines[1].Split(","[0])[8]);
        float minLongitude = float.Parse(lines[1].Split(","[0])[7]);
        float maxLatitude = minLatitude;
        float maxLongitude = minLongitude;

        float longitudeOffset = -4.21600342f;
        float latitudeOffset = 54.97935281f;
        //var latitudeOffset = regionPlane.transform.position.x; var longitudeOffset = regionPlane.transform.position.z;
        Debug.Log("latitudeOffset: "+latitudeOffset+ ", longitudeOffset: " + longitudeOffset);

        for ( var i = 1; i < lines.Length -2; i++)
        {
            var lineIndex0 = lines[i].Split(","[0]);
            var futureLine= lines[i+1].Split(","[0]);

            float longitude = float.Parse(lineIndex0[7]);// + longitudeOffset;
            float latitude = float.Parse(lineIndex0[8]);// + latitudeOffset ;
            float futureLongitude = float.Parse(futureLine[7]);// + longitudeOffset;
            float futureLatitude = float.Parse(futureLine[8]);// + latitudeOffset ;
            //latitude -= 51.61978808f; futureLatitude -= 51.61978808f;
            //longitude += 1.40350342f; futureLongitude+= 1.40350342f;

            // x axis in Unity opposite direction of x axis from data
            Vector3 currentVec = new Vector3(-longitude, wrongTimeUp,  latitude);
            wrongTimeUp +=.1f;
            Vector3 futureVec = new Vector3(-futureLongitude, wrongTimeUp,  futureLatitude);

            if (minLatitude > latitude) minLatitude = latitude; if (maxLatitude < latitude) maxLatitude = latitude;
            if (minLongitude> longitude) minLongitude = longitude; if (maxLongitude < longitude) maxLongitude = longitude;

            Vector3 posBegining = new Vector3( (currentVec.x +longitudeOffset )*(10.66667f) , currentVec.y, currentVec.z - latitudeOffset);
            Vector3 posFuture = new Vector3( (futureVec.x + longitudeOffset) * (10.66667f) , futureVec.y, futureVec.z - latitudeOffset);
            Debug.Log("longitude: " + longitude + ", latitude: "+ latitude);
            Debug.Log("posBegining: " + posBegining);
            Debug.Log("futureLongitude: " + futureLongitude + ", futureLatitude: " + futureLatitude);
            Debug.Log("posFuture: " + posFuture);

            DrawLine(posBegining, posFuture, new Color(1, 0, 0, .4f));


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

    void DrawLinesAtBordersOfCube()
    {
        Debug.Log("DrawLinesAtBordersOfCube");
        var pos = regionPlane.transform.position;
        var scale = regionPlane.transform.localScale;
        Debug.Log(pos);
        Debug.Log(scale);
        
        DrawLine(new Vector3(pos.x, pos.y, pos.z), new Vector3(pos.x, pos.y + 50, pos.z), new Color(1f,1f,0f));
        // Draw at the four corners
        DrawLine(new Vector3(pos.x - scale.x/2, pos.y, pos.z - scale.z / 2), new Vector3(pos.x - scale.x / 2, pos.y + 50, pos.z - scale.z / 2), new Color(1f, 1f, 0f));
        DrawLine(new Vector3(pos.x - scale.x / 2, pos.y, pos.z + scale.z / 2), new Vector3(pos.x - scale.x / 2, pos.y + 50, pos.z + scale.z / 2), new Color(1f, 1f, 0f));
        DrawLine(new Vector3(pos.x + scale.x / 2, pos.y, pos.z - scale.z / 2), new Vector3(pos.x + scale.x / 2, pos.y + 50, pos.z - scale.z / 2), new Color(1f, 1f, 0f));
        DrawLine(new Vector3(pos.x + scale.x / 2, pos.y, pos.z + scale.z / 2), new Vector3(pos.x + scale.x / 2, pos.y + 50, pos.z + scale.z / 2), new Color(1f, 1f, 0f));

    }

    void DrawLinesAtCenterOfScene()
    {
        DrawLine(new Vector3(0f, 0f, 0f), new Vector3(0f, 200, 0f), new Color(1f, 1f, 1f));

    }


}
