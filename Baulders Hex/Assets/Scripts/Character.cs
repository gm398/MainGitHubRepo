using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ScriptableObject, IHealth
{
   private string CharacterName;

   public float Health { get; set; }
   public float Armour { get; set; }
}
