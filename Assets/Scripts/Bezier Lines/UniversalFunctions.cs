using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UniversalFunctions : MonoBehaviour
{                                                   //Dzięki dziedziczeniu tej funkcji moge używać zamiennie linii i bezier splajnów by rozwijać/ciągnać dana figurę

    public abstract Vector3 GetProgress(float t);  //odpowiedzalne za podawanie punktu na linii/bezier splajnie. Przyjmuje wartości od 0 do 1

    public abstract Vector3 GetDirection(float t); //odpowiedzalne za poprawne ustawienie rotacjii obiektu . Przyjmuje wartości od 0 do 1
}
