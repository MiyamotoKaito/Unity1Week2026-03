using UnityEngine;

public class Attack : ICardEffect
{
   public Attack()
   {

   }
   public void Exucute()
   {
       Debug.Log("攻撃の効果を発動");
   }
}

public class Block : ICardEffect
{
    public void Exucute()
    {
        Debug.Log("防御の効果を発動");
    }
}
