using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct Stats
{
    //dichiaro le variabile pubbliche della struct
    public int atk;
    public int def;
    public int res;
    public int spd;
    public int crt;
    public int aim;
    public int eva;
    



    public Stats(int atk, int def, int res, int spd, int crt, int aim, int eva)
    {
        // creo costruttore per valorizzazione delle statistiche sopra create
        this.atk = atk;
        this.def = def;
        this.res = res;
        this.spd = spd;
        this.crt = crt;
        this.aim = aim;
        this.eva = eva;

    }

    public static Stats Sum(Stats stats1, Stats stats2)
    {
        //sommo due structs diverse, immagino serve ad esempio a sommare i valori dell'equip direttamente sopra un pg

        Stats result = new Stats();
        result.atk = stats1.atk + stats2.atk;
        result.def = stats1.def + stats2.def;
        result.res = stats1.res + stats2.res;
        result.spd = stats1.spd + stats2.spd;
        result.crt = stats1.crt + stats2.crt;
        result.aim = stats1.aim + stats2.aim;
        result.eva = stats1.eva + stats2.eva;
        return result;

    }




}

