using ElementsLib; // anche quì aggiunto namespace utilizzando ELEMENT
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameFormulas
{
    public static bool HasElementAdvantage(ELEMENT attackElement, Hero defender)
    {
        if (attackElement == defender.Weakness)
        {
            return true;
        }
        return false;
    }
    public static bool HasElementDisadvantage(ELEMENT attackElement, Hero defender)
    {
        if (attackElement == defender.Resistance)
        {
            return true;
        }
        return false;
    }
    public static float EvaluateElementalModifier(ELEMENT attackElement, Hero defender)
    {
        if (HasElementAdvantage(attackElement, defender) == true)
        {
            return 1.5f;
        }
        else if (HasElementDisadvantage(attackElement,defender) == true)
        {
            return 0.5f;
        }
        return 1.0f;
    }

    public static bool HasHit(Stats attacker, Stats defender)
    {
        int hitChance = attacker.aim - defender.eva;
        int randomRoll = Random.Range(0, 100);
        if (randomRoll > hitChance)
        {
            //Debug.Log("Rolled " + randomRoll); <---- non necessari al momento
            //Debug.Log("hitChance is " + hitChance); <---- non necessari al momento
            Debug.Log( "MISS"); // <--- valutare successivamente se esiste un modo di stampare a schermo e non in console
            return false;
        }
        return true;
    }

    public static bool IsCrit(int critValue)
    {
        int randomRoll = Random.Range(0, 100);
        if (randomRoll < critValue)
        {
            //Debug.Log("Rolled " + randomRoll);  <---- non necessari al momento
            //Debug.Log("citValue is " + critValue); <---- non necessari al momento
            Debug.Log("CRIT"); // <--- valutare successivamente se esiste un modo di stampare a schermo e non in console
            return true;
        }
        return false;
    }

    public static int CalculateDamage(Hero attacker, Hero defender)
    {
        float modifiedDmg;
        Stats attackerTotalStats = Stats.Sum(attacker.BaseStats, attacker.Weapon.BonusStats );
        Stats defenderTotalStats = Stats.Sum(defender.BaseStats, defender.Weapon.BonusStats);
        switch (attacker.Weapon.DmgType)
        {
            case Weapon.DAMAGE_TYPE.PHYSICAL:
                int basePDamage = attackerTotalStats.atk - defenderTotalStats.def;
                float elementalModifierP = EvaluateElementalModifier(attacker.Weapon.Elem, defender);
                modifiedDmg = basePDamage * elementalModifierP;
                if (IsCrit(attackerTotalStats.crt))
                {
                    modifiedDmg *=  2;
                }
                break;
            case Weapon.DAMAGE_TYPE.MAGICAL:
                int baseMDamage = attackerTotalStats.atk - defenderTotalStats.res;
                float elementalModifierM = EvaluateElementalModifier(attacker.Weapon.Elem, defender);
                modifiedDmg = baseMDamage * elementalModifierM;
                if (IsCrit(attackerTotalStats.crt))
                {
                    modifiedDmg *= 2;
                }
                break;

            default:
                return 0;
        }
        if (modifiedDmg < 0)
        {
            modifiedDmg = 0;
        }
        return Mathf.FloorToInt(modifiedDmg);

    }

}