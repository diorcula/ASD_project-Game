﻿using Player.Model.ItemStats;

namespace Weapon
{
    public class AK_47 : Weapon
    {
        public AK_47()
        {
            Name = "AK-47";
            Type = WeaponType.Range;
            Rarity = Rarity.Rare;
            Speed = WeaponSpeed.Average;
            Distance = WeaponDistance.Far;
            Damage = WeaponDamage.Low;
        }
    }
}