using System;
using UnityEngine;

namespace Assets.Scripts.Stats
{
    [Serializable]
    public sealed class Stat
    {
        [SerializeField]
        private StatType statType;

        [SerializeField, Range(-1, 100)]
        private float value;

        [SerializeField, Min(100)]
        private float maxValue;

        public event Action<float> ValueChanged;

        public Stat(StatType statType, float value, float maxValue)
        {
            this.statType = statType;
            this.value = value;
            this.maxValue = maxValue;
        }

        public StatType StatType
        {
            get => statType;
            set => statType = value;
        }
        public float Value
        {
            get => value;
            set
            {
                this.value = value;
                ValueChanged?.Invoke(this.value);
            }

        }
        public float MaxValue
        {
            get => maxValue;
            set => maxValue = value;
        }
    }
}