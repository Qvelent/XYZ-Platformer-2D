using System;
using System.Collections;
using UnityEngine;

namespace Assets.PlayerOption.Scripts.Model.Data.Properties
{
    [Serializable]
    public class IntProperty : PersistentProperty<int>
    {
        public IntProperty(int defaultValue) : base(defaultValue)
        {

        }
        protected override void Write(int value)
        {
            _value = value;
        }

        protected override int Read(int defaultValue)
        {
            return _value;
        }
    }
}