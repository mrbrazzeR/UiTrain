using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;



namespace UiTrain.CharacterStats
{

    [Serializable]
    public class CharacterStat
    {
        public float BaseValue;

        public float Value
        {
            get
            {
                if (isDirty || BaseValue != lastBaseValue)
                {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }

                return _value;
            }
        }

        private bool isDirty = true;
        private float _value;
        private float lastBaseValue = float.MinValue;

        private readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        private int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0;
        }

        public bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }

            return false;
        }

        public bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;
            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        private float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;


            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier modifier = statModifiers[i];
                if (modifier.Type == StatModifierType.Flat)
                {
                    finalValue += statModifiers[i].Value;
                }
                else if (modifier.Type == StatModifierType.PercentAdd)
                {
                    sumPercentAdd += modifier.Value;
                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModifierType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                    }
                }
                else if (modifier.Type == StatModifierType.PercentMult)
                {
                    finalValue *= modifier.Value;
                }
            }

            return (float) Math.Round(finalValue, 4);
        }
    }
}
