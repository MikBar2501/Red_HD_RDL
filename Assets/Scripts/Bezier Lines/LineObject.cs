using System.Collections.Generic;
using UnityEngine;

public class LineObject : MonoBehaviour
{

    public List<Line> listOfLines;
    public void Start()
    {
        listOfLines = new List<Line>();
        listOfLines.Add(new Line(new Vector3(0, 0, 0), new Vector3(1, 0, 1)));

    }
    /*
    public LineObject()
    {
        AddNewLine();
    }
    public void AddLine(Vector3 p0, Vector3 p1)
    {
        listOfLines.Add(new Line(p0, p1));
    }

    public void AddNewLine()
    {
        listOfLines.Add(new Line(new Vector3(0, 0, 0), new Vector3(1, 0, 1)));
    }

    public Vector3 GetSelectedPoint(int index)
    {
        if (index % 2 == 0)
            return listOfLines[(int)Mathf.Floor((float)index / 2)].p0;
        else
            return listOfLines[(int)Mathf.Floor((float)index / 2)].p1;
    }

    public void SetPoint(int index, Vector3 pos)
    {
        if (index % 2 == 0)
            listOfLines[(int)Mathf.Floor((float)index / 2)].p0 = pos;
        else
            listOfLines[(int)Mathf.Floor((float)index / 2)].p1 = pos;
    }
    */
    

}