using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int magicPower;
    //For enemies, how much experience are they worth.  For the player, how much exp total have we gained?
    public int experienceGained;
    public int expForNextLevel;

    //return true if dies, false otherwise
    public bool TakeDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            currentHP = 0;
            return true;
        }

        return false;
    }

    public void HealDamage(int healHP)
    {
        currentHP = Mathf.Clamp(currentHP + healHP, 0, maxHP);        
    }

    public string GrantExperience(int exp)
    {
        experienceGained += exp;

        return CheckForLevelUp();
    }

    private string CheckForLevelUp()
    {
        if(experienceGained >= expForNextLevel)
        {
            //Update our level
            this.unitLevel++;

            //Bonus stats!
            //20% boost to HP, truncate down
            int addtlHP = (int)(this.maxHP * 0.2);
            this.maxHP += addtlHP;

            //10% bost to damage, truncate date
            int addtlDmg = (int)(this.damage * 0.1);
            this.damage += addtlDmg;

            //+1 to magic
            this.magicPower++;

            //Free heal
            this.currentHP = this.maxHP;



            //Next level is 50% more exp
            this.expForNextLevel = (int)(this.expForNextLevel * 1.5);


            //Return a string explaining it
            return string.Format("\r\n\r\nYou have gained a level!  Stats have been increased:  HP +{0}, Dmg +{0}, Mag +1", addtlHP, addtlDmg);
        }

        //Normal, no changes to report
        return "";
    }
}
