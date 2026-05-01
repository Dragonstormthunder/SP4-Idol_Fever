using UnityEngine;

namespace IdolFever
{
    public class EnumNamedArrayAttribute : PropertyAttribute
    {
        // serialize an array to a name
        public string[] names;
        public EnumNamedArrayAttribute(System.Type names_enum_type)
        {
            names = System.Enum.GetNames(names_enum_type);
        }

        // credit to https://answers.unity.com/questions/1589226/showing-an-array-with-enum-as-keys-in-the-property.html
    }
}