using System;
using UnityEngine;
using UnityFlow.DocumentationHelper.Library.Documentation;

namespace OmegaLeo.Toolbox.Attributes
{
    [Documentation(nameof(ConditionalFieldAttribute), "Attribute to show/hide properties based on a condition")]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ConditionalFieldAttribute : PropertyAttribute
    {
        public Type ValueType;
        public string ConditionalSourceField = "";
        public object Value;

        public ConditionalFieldAttribute(string conditionalSourceField, object value, Type valueType)
        {
            ValueType = valueType;
            ConditionalSourceField = conditionalSourceField;
            Value = value;
        }
    }
}