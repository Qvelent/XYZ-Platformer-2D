using UnityEngine;

namespace Assets.PlayerOption.Scripts.Model.Data.Properties
{
    public abstract class PersistentProperty<TPropertyType> // abstract class означает, что у него будет абстрактные члены.
    {
        [SerializeField] private TPropertyType _value;
        private TPropertyType _defaultValue;
        private TPropertyType _stored;

        public PersistentProperty(TPropertyType defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public delegate void OnPropertyChanged(TPropertyType newValue,
                                                TPropertyType oldValue);

        public event OnPropertyChanged OnChanged;

        public TPropertyType Value
        {
            get => _stored;
            set
            {
                var isEquals = _stored.Equals(value);
                if (isEquals) return;

                var oldValue = _value;
                Write(value);
                _stored = _value = value;

                OnChanged?.Invoke(value, oldValue);
            }
        }

        protected void Init()
        {
            _stored = _value = Read(_defaultValue);
        }

        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType defaultValue);

        public void Validate()
        {
            if (!_stored.Equals(_value))
                Value = _value;
        }
    }
}