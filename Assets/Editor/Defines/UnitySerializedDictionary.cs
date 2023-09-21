using System;
using System.Collections.Generic;
using UnityEngine;

namespace Luban.Editor
{
    [Serializable]
    public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>,
                                                                    ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector]
        private List<TKey> _key_data = new();

        [SerializeField, HideInInspector]
        private List<TValue> _value_data = new();

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();
            for(int i = 0; i < _key_data.Count && i < _value_data.Count; i++)
            {
                this[_key_data[i]] = _value_data[i];
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            _key_data.Clear();
            _value_data.Clear();

            foreach(var item in this)
            {
                _key_data.Add(item.Key);
                _value_data.Add(item.Value);
            }
        }
    }
}