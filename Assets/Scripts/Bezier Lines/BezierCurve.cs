using UnityEngine;

public class BezierCurve : UniversalFunctions {

	public Vector3[] points;
	
	public override Vector3 GetProgress (float t) // podobnie jak w linii - t przyjmuje wartości od 0 do 1. Gdzie wartość pomiędzy jest wyliczana 
    { 
		return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], points[3], t));
	}
	
	public Vector3 GetVelocity (float t) // jest powiązane z wielkością odchyłu i długością linii
    {  
		return transform.TransformPoint(Bezier.GetFirstDerivative(points[0], points[1], points[2], points[3], t)) - transform.position;
	}
	
	public override Vector3 GetDirection (float t)   // kierunek potrzebny do właściwego obracanie obiektów
    { 
        return GetVelocity(t).normalized;
	}
	
	public void Reset () {
		points = new Vector3[] {
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f),
			new Vector3(3f, 0f, 0f),
			new Vector3(4f, 0f, 0f)
		};
	}
}