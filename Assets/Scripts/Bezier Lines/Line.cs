using UnityEngine;

public class Line : UniversalFunctions {

	public Vector3 p0, p1;

    GameObject baseCube;

    public void Start() // wyświetlanie linii w trybie gry
    {
        // tworzenie linii
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = GetProgress(0.5f);
        cube.transform.localScale = new Vector3( 0.01f, 0.01f, Vector3.Distance(GetProgress(0), GetProgress(1)));
        cube.transform.LookAt( GetProgress(1)) ;
        cube.name = "LineDisplay";
        //Tworzenie punktów początku linii
        GameObject baseCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        baseCube.transform.position = GetProgress(0);
        baseCube.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        baseCube.name = "BaseOfLine";

    }

    public Line(Vector3 tp0, Vector3 tp1)
    {
        p0 = tp0;
        p1 = tp1;
        
    }


    public override Vector3 GetProgress(float t) // zmienna t przyjmuje wartość od 0 do 1 gdzie 0 to p0 a 1 to p1
    {
        if (t >= 1f)
        {
            t = 1f;
        }
        return (p0 + (p1 - p0) * t) + gameObject.transform.position;
    }

    public override Vector3 GetDirection(float t) // kierunek obrotu figury
    {
        return Vector3.Normalize(p1 - p0);
    }


}