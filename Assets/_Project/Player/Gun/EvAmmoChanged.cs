using System.Collections;
using System.Collections.Generic;
using CodTools.Utilities;
using UnityEngine;

public class EvAmmoChanged : IGameEvent
{
   public int Ammo { get; }
   public int AmmoMax { get; }

   public EvAmmoChanged(int ammo, int ammoMax) {
      Ammo = ammo;
      AmmoMax = ammoMax;
   }
}
