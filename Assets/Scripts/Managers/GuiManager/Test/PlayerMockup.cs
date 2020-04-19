using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMockup : MonoBehaviour
{
    //This class is generate only for testing purpose of the GUIManager
    //and should be replaced soon

    float maxHpPossibleInGame = 700;

    [SerializeField]
    float currentHp;
    [SerializeField]
    float maxHp;

    public float CurrentHp {
        get { return currentHp; }
        set {
            currentHp = value;
            if (currentHp > maxHp)
                currentHp = maxHp;
            else if (currentHp < 0)
                currentHp = 0;
        }
    }

    public float MaxHp {
        get { return maxHp; }
        set {
            maxHp = value;
            if (currentHp > maxHp)
                currentHp = maxHp;
            if (maxHp < 1) {
                currentHp = 1;
                maxHp = 1;
            }
            if (maxHp > maxHpPossibleInGame)
                maxHp = maxHpPossibleInGame;
        }
    }

}
