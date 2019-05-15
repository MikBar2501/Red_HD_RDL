using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRadiationSettings : MonoBehaviour
{
    
    public int shieldCount;
    public float shieldValue;
    public float maxShieldValue;
    public int nonShieldInZoneCounter;
    public int maxNonShieldInZoneCounter;
    public bool inZone;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        shieldValue = maxShieldValue;
        isDead = false;
        //shieldCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(nonShieldInZoneCounter == 0 && !inZone) {
            CancelInvoke("Regeneration");
        }
    }

    public void SetInZone(bool imInZone) {
        inZone = imInZone;
    }

    public void SetDamage(float distance, float damage) {
        //Debug.Log("Damage: " + damage + ", distance: " + distance);
        if(shieldValue <= 0 && shieldCount <= 0) {
            shieldValue = 0;
            shieldCount = 0;
            if(!isDead){
                nonShieldInZoneCounter++;
                if(nonShieldInZoneCounter >= maxNonShieldInZoneCounter) {
                    Death();
                    isDead = true;
                }
            }
        } else {
            if(shieldCount > 0) {
                shieldValue -= damage;
                if(shieldValue <= 0) {
                    shieldCount--;
                    shieldValue = maxShieldValue + shieldCount;
                }
            } else {
                shieldValue -= damage;
            }
        }
        Debug.Log("Shield value: " + shieldValue + ", Shield count: " + shieldCount);
    }

    public void AddResistance(int value) {
         
        shieldValue += value;
        if(shieldValue > maxShieldValue) {
            shieldCount++;
            shieldValue = shieldValue - maxShieldValue;
        }
    }

    public void Death() {
        Debug.Log("You died, bitch");
    }

    public void Regeneration() {
        nonShieldInZoneCounter -= 1;
        if(0 > nonShieldInZoneCounter) {
            nonShieldInZoneCounter = 0;
        }
    }

    public void StartRegeneration() {
        InvokeRepeating("Regeneration",1f,1f);
    }

}
