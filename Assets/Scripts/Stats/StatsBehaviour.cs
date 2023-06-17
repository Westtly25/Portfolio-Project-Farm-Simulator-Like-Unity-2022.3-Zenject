using System;
using UnityEngine;
using Unity.Collections;

namespace Assets.Scripts.Stats
{
    [Serializable]
    public class StatsBehaviour
    {
        [SerializeField, ReadOnly]
        private Stat[] stats;

        public Stat Stats => Stats;

        public StatsBehaviour()
        {
            stats = new Stat[3]
            {
                new Stat(StatType.Health, 100, 100),
                new Stat(StatType.Energy, 100, 100),
                new Stat(StatType.Hydration, 100, 100),
            };
        }

        public void UpdateStatValue(StatType statType, float value)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                if (stats[i].StatType == statType)
                {
                    stats[i].Value += value;
                    return;
                }
            }
        }

        public void Reset()
        {
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i].Value = 100;
                stats[i].MaxValue = 100;
            }
        }
    }
}