using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElementsLib;
using Unity.VisualScripting;
using JetBrains.Annotations;


public class M1ProjectTest : MonoBehaviour
{
    [SerializeField] Hero a = new Hero("Banfi", 100, new Stats(20, 12, 5, 8, 10, 75, 5), ELEMENT.VOID, ELEMENT.ICE, new Weapon("VoidGun", Weapon.DAMAGE_TYPE.MAGICAL, ELEMENT.VOID, new(2, 1, 5, 3, 1, 5, 2)));
    [SerializeField] Hero b = new Hero("Boldi", 100, new Stats(19, 20, 7, 3, 11, 70, 5), ELEMENT.LIGHTNING, ELEMENT.FIRE, new Weapon("TATATATA", Weapon.DAMAGE_TYPE.PHYSICAL, ELEMENT.LIGHTNING, new(2, 5, 1, 3, 5, 1, 2)));
    
    private bool gameEnded = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) return;

        if (!a.IsAlive())
        {
            if (!gameEnded) Debug.Log("GIOCATORE " + a.Name + " È STATO SCONFITTO!");
            gameEnded = true;
            return;
        }
        else if (!b.IsAlive())
        {
            if (!gameEnded) Debug.Log("GIOCATORE " + b.Name + " È STATO SCONFITTO!");
            gameEnded = true;
            return;
        }

        Stats weaponStatsA = a.Weapon != null ? a.Weapon.BonusStats : new Stats(); // Scontrollo se non ci sono armi
        Stats TotalA = Stats.Sum(a.BaseStats, weaponStatsA);

        Stats weaponStatsB = b.Weapon != null ? b.Weapon.BonusStats : new Stats();
        Stats TotalB = Stats.Sum(b.BaseStats, weaponStatsB);

        Hero firstAttacker, firstDefender;
        Stats firstAttackerStats, firstDefenderStats;

        if (TotalA.spd > TotalB.spd)
        {
            firstAttacker = a; firstDefender = b;
            firstAttackerStats = TotalA; firstDefenderStats = TotalB;
            
        }
        else if (TotalB.spd > TotalA.spd)
        {
            firstAttacker = b; firstDefender = a;
            firstAttackerStats = TotalB; firstDefenderStats = TotalA;
        }
        else
        {
            firstAttacker = a; firstDefender = b; // facciamo attaccare a se c'è un pareggio
            firstAttackerStats = TotalA; firstDefenderStats = TotalB;
            Debug.Log("Pareggio di velocità! " + firstAttacker.Name + " attacca per primo.");
        }

        bool defenderSconfitto = EseguiTurno(firstAttacker, firstDefender, firstAttackerStats, firstDefenderStats);
        if (defenderSconfitto)
        {
            gameEnded = true;
            // enabled = false; // Disabilita lo script per fermare Update
            return;
        }
        if (firstDefender.IsAlive() && firstAttacker.IsAlive())
        {
            
            bool attaccanteOriginaleSconfitto = EseguiTurno(firstDefender, firstAttacker, firstDefenderStats, firstAttackerStats);
            if (attaccanteOriginaleSconfitto)
            {
                gameEnded = true;
                // enabled = false;
                return;
            }
        }
    }

    public bool EseguiTurno(Hero attacker, Hero defender, Stats attackerStats, Stats defenderStats)
    {
        if (!attacker.IsAlive()) return !defender.IsAlive(); 
        if (!defender.IsAlive()) return true; 

        if (attacker.Weapon == null)
        {
            Debug.Log("l'attaccante non ha armi, non può attaccare");
            return false;
        }
        Debug.Log("Il giocatore " + attacker.Name + " attacca!!");
        if (GameFormulas.HasHit(attackerStats, defenderStats))
        {
            if (attacker.Weapon.Elem == defender.Weakness && defender.Weakness != ELEMENT.NONE)
            {
                Debug.Log("WEAKNESS!");
            }
            else if (attacker.Weapon.Elem == defender.Resistance && defender.Resistance != ELEMENT.NONE)
            {
                Debug.Log("RESIST!");
            }

            int damageDealt = GameFormulas.CalculateDamage(attacker, defender);
            Debug.Log("Il giocatore " + attacker.Name + " infligge " + damageDealt + " Danni al giocatore " + defender.Name);

            defender.TakeDamage(damageDealt); 

            if (!defender.IsAlive()) 
            {
                Debug.Log("!!! " + defender.Name + " è stato sconfitto da " + attacker.Name + " !!!");
                return true; 
            }
        }
        else
        {
            Debug.Log(attacker.Name + " ha MANCATO l'attacco contro " + defender.Name + "!");
        }
        return false; // Difensore ancora vivo
    }



}

    //
    // ----------------------------------------------------------------------------------  Lascio la mia prima logica che non mi convinceva --------------------------------------------------------------------
    //
    //public void turnoGiocatore(Hero Attacker, Hero Defender, Stats modA, Stats modB)
    //{
    //    Debug.Log("Il giocatore " + Attacker.Name + " attacca!!");
    //    if (GameFormulas.HasHit(modA, modB))
    //    {
    //        if (Attacker.Weapon.Elem == Defender.Weakness)
    //        {
    //            Debug.Log("WEAKNESS");
    //        }
    //        else if (Attacker.Weapon.Elem == Defender.Resistance)
    //        {
    //            Debug.Log("Resist");
    //        }
    //        DamagetoDo = GameFormulas.CalculateDamage(Attacker, Defender);
    //        Debug.Log("Il giocatore " + Attacker.Name + " infligge " + DamagetoDo + " Danni al giocatore " + Defender.Name);
    //        Defender.TakeDamage(DamagetoDo);
    //        if (!Defender.IsAlive())
    //        {
    //            Debug.Log("Il giocatore " + Attacker.Name + " ha vinto!");
    //        }
    //        else
    //        {
    //            controAttacco(Defender, Attacker, modB, modA);
    //        }
            
    //    }
        

    //}

    //public void controAttacco(Hero Attacker, Hero Defender, Stats modA, Stats modB)
    //{
    //    Debug.Log("Il giocatore " + Attacker.Name + " effettua un contro attacco!!");
    //    if (GameFormulas.HasHit(modA, modB))
    //    {
    //        if (Attacker.Weapon.Elem == Defender.Weakness)
    //        {
    //            Debug.Log("WEAKNESS");
    //        }
    //        else if (Attacker.Weapon.Elem == Defender.Resistance)
    //        {
    //            Debug.Log("Resist");
    //        }
    //        DamagetoDo = GameFormulas.CalculateDamage(Attacker, Defender);
    //        Debug.Log("Il giocatore " + Attacker.Name + " infligge " + DamagetoDo + " Danni al giocatore " + Defender.Name);
    //        Defender.TakeDamage(DamagetoDo);
    //        if (!Defender.IsAlive())
    //        {
    //            Debug.Log("Il giocatore " + Attacker.Name + " ha vinto!");
    //        }
    //        else
    //        {
    //            return;
    //        }

    //    }
    //}
    //}

