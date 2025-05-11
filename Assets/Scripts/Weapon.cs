using ElementsLib; // richiamo namespace di ELEMENT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon 
{
    [SerializeField] private string name;
    [SerializeField] private DAMAGE_TYPE dmgType;
    [SerializeField] private ELEMENT elem;
    [SerializeField] private Stats bonusStats;


public enum DAMAGE_TYPE { PHYSICAL, MAGICAL}

public Weapon (string name, DAMAGE_TYPE dmgType, ELEMENT elem, Stats bonusStats)
    {
        this.name = name;
        this.dmgType = dmgType;
        this.elem = elem;
        this.bonusStats = bonusStats;
    }

public Weapon()
    {
        name = "Mani nude";
        dmgType = DAMAGE_TYPE.PHYSICAL;
        elem = ELEMENT.NONE;
        bonusStats = new Stats();
    }

    // uso properties
    public string Name {
        get {return name;}
        set 
        {
            if (!string.IsNullOrEmpty(name))
            {
                name = value;
            }
        }
    }
    public DAMAGE_TYPE DmgType
    {
        get { return dmgType; }
        set { dmgType = value; }
    }
    public ELEMENT Elem
    {
        get { return elem; }
        set { elem = value; }
    }

    public Stats BonusStats
    {
        get { return bonusStats; }
        set { bonusStats = value; }
    }















}
