using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguesDisplayer : MonoBehaviour
{
    static DialoguesDisplayer _object;
    public GameObject window;
    public Text personName;
    public Text dialog;
    public Transform player;
    Transform person;
    //  int currentPart;
 //   string[] parts; //parts of dialog

    public static void Display(Vector3 pos, TextAsset text, string personName, Transform person)
    {
        _object.window.transform.position = pos + new Vector3(0, 2, 0);
        _object.window.transform.LookAt(Camera.main.transform.position);

        _object.window.transform.Rotate(Vector3.right, 180, Space.Self);
        _object.window.transform.Rotate(Vector3.forward, 180, Space.Self);
        _object.window.SetActive(true);
        _object.personName.text = personName;
        _object.person = person;
        _object.dialog.text = text.text;
    }

    public static void Close()
    {
        _object.window.SetActive(false);
    }

    public static bool isOpen()
    {
        if (_object.window.activeInHierarchy)
            return true;
        return false;
    }

    public static string OpenedBy()
    {
        return _object.personName.text;
    }
    
    void Awake()
    {
        _object = this;
        window.SetActive(false);
    }
    
    void Update()
    {
        if(window.activeInHierarchy)
        {
            if(person == null)
            {
                Debug.Log("no onwer, close");
                window.SetActive(false);
                return;
            }
            if(Vector3.Distance(player.position, person.position) > InteractionHandler._object.Range)
            {
                Close();
                return;
            }

       //     window.transform.LookAt(Camera.main.transform.position);
      //      window.transform.Rotate(Vector3.right, 180, Space.Self);
       //     window.transform.Rotate(Vector3.forward, 180, Space.Self);
        }
    }
}
