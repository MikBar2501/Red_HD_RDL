using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : MonoBehaviour
{
    public bool inZone;
    public int radiationStrengthMax;
    float distance;
    Collider player;
    float radius;
    
    void Start()
    {
        radius = GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Wszedłem");
            player = other;
            inZone = true;
            other.GetComponent<PlayerRadiationSettings>().SetInZone(inZone);
            InvokeRepeating("SetDamage",1f,1f);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            player = null;
            inZone = false; 
            other.GetComponent<PlayerRadiationSettings>().SetInZone(inZone); 
            other.GetComponent<PlayerRadiationSettings>().StartRegeneration();
            CancelInvoke("SetDamage");
        } 
    }

    void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player")) {
            distance = Vector3.Distance(transform.position,other.GetComponent<Transform>().transform.position);
        }
    }

    void Print() {
        print(Time.time);
        Debug.Log("COś");
    }

    void SetDamage() {
        float x = distance / radius;
        float damage = (1-x) * radiationStrengthMax;
        if(damage < 0) {
            damage *= -1;
        }
        player.GetComponent<PlayerRadiationSettings>().SetDamage(distance,damage);
    }
}
