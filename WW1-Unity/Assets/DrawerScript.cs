using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour {

    Vector3 vectorBegin = Vector3.zero;
    Vector3 vectorEnd = new Vector3(14, 3, 3);

    // Use this for initialization
    void Start () {
		Debug.DrawLine(Vector3.zero, new Vector3(1, 0, 0), Color.red);

    }

	// Update is called once per frame
	void Update () {
        DrawLine(vectorBegin, vectorEnd, new Color(40, 10, 200), 0.2f);
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
