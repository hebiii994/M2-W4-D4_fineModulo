using System.Collections;
using System.Collections.Generic;
using ElementsLib; // richiamo namespace di ELEMENT
using UnityEngine;

[System.Serializable]
public class Hero
{
    [SerializeField] private string name;
    [SerializeField] private int hp;
    [SerializeField] private Stats baseStats;
    [SerializeField] private ELEMENT resistance;
    [SerializeField] private ELEMENT weakness;
    [SerializeField] Weapon weapon;

    public Hero (string name, int hp, Stats baseStats, ELEMENT resistance, ELEMENT weakness, Weapon weapon)
    {
        this.name = name;
        this.hp = hp;
        this.baseStats = baseStats;
        this.resistance = resistance;
        this.weakness = weakness;
        this.weapon = weapon;

    }

    //properties anche quì per i get e set 

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Hp 
    {
        get { return hp; }
        set                                         // <--- aggiunto controllo sul valore negativo, da capire se da rimuovere per l'utilizzo di takedamage (non lo so ancora) 
        {
            if (value < 0)
            {
                hp = 0;
            }
            else
            {
                hp = value;
            }

        }
    }
    public Stats BaseStats
    {
        get { return baseStats; }
        set {  baseStats = value; }
    }
    public ELEMENT Resistance
    {
        get { return resistance; }
        set { resistance = value; }
    }
    public ELEMENT Weakness
    {
        get { return weakness; }
        set { weakness = value; }
    }
    public Weapon Weapon
    {
        get { return weapon; }
        set { weapon = value; }
    }


    public void AddHp (int amount)
    {
        if (amount < 0)
        {
            this.hp = this.hp + amount;
            return;
        }
        this.hp = this.hp + amount;

    }

    public void TakeDamage(int damage)
    {
        
        AddHp(-damage);
    }

    public bool IsAlive()
    {
        if (this.hp > 0) 
        {  
            return true; 
        }
            return false;
    }

}