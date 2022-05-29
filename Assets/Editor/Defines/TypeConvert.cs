using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;

namespace Luban.Editor
{
    internal static class TypeConvert
    {
        static TypeConvert()
        {
            var list = TypeCache.GetTypesDerivedFrom<IBeforeGen>();

            foreach(var type in list)
            {
                var label_text = type.GetCustomAttribute<LabelTextAttribute>();

                string name = type.Name;

                BEFORE_GENS.Add("无", "");

                if(label_text is not null)
                {
                    name = label_text.Text;
                }

                BEFORE_GENS.Add(name, type.FullName);
                BEFORE_TYPES.Add(type.FullName, type);
            }

            list = TypeCache.GetTypesDerivedFrom<IAfterGen>();

            foreach(var type in list)
            {
                var label_text = type.GetCustomAttribute<LabelTextAttribute>();

                string name = type.Name;

                AFTER_GENS.Add("无", "");

                if(label_text is not null)
                {
                    name = label_text.Text;
                }

                AFTER_GENS.Add(name, type.FullName);
                AFTER_TYPES.Add(type.FullName, type);
            }
        }

        public static readonly ValueDropdownList<string> BEFORE_GENS = new();

        public static readonly Dictionary<string, Type> BEFORE_TYPES = new();

        public static readonly ValueDropdownList<string> AFTER_GENS = new();

        public static readonly Dictionary<string, Type> AFTER_TYPES = new();
    }
}