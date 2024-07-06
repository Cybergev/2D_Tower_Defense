using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EnergyShield : Destructible
    {
        [SerializeField] private int decaySpeed;
        private void Update()
        {
            ApplyDamage(decaySpeed * (int)Time.deltaTime);
        }
    }
}