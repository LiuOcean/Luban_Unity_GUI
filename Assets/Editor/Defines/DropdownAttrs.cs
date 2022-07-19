using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Luban.Editor
{
    [IncludeMyAttributes]
    [ValueDropdown("@BeforeGenSelectorAttribute.Get()")]
    public class BeforeGenSelectorAttribute : Attribute
    {
        private static IEnumerable<ValueDropdownItem> Get()
        {
            var list = TypeCache.GetTypesDerivedFrom<IBeforeGen>();

            yield return new ValueDropdownItem("无", "");

            foreach(var type in list)
            {
                var label_text = type.GetCustomAttribute<LabelTextAttribute>();

                string name = type.Name;

                if(label_text is not null)
                {
                    name = label_text.Text;
                }

                yield return new ValueDropdownItem(name, type.FullName);
            }
        }
    }

    [IncludeMyAttributes]
    [ValueDropdown("@AfterGenSelectorAttribute.Get()")]
    public class AfterGenSelectorAttribute : Attribute
    {
        private static IEnumerable<ValueDropdownItem> Get()
        {
            var list = TypeCache.GetTypesDerivedFrom<IAfterGen>();

            yield return new ValueDropdownItem("无", "");

            foreach(var type in list)
            {
                var label_text = type.GetCustomAttribute<LabelTextAttribute>();

                string name = type.Name;


                if(label_text is not null)
                {
                    name = label_text.Text;
                }

                yield return new ValueDropdownItem(name, type.FullName);
            }
        }
    }
}