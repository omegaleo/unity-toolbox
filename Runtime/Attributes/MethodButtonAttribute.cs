using System;
using System.Reflection;

namespace OmegaLeo.Toolbox.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodButtonAttribute : Attribute {
        public string methodName;
        public string buttonName;
        public bool useValue;
        public BindingFlags flags;

        public MethodButtonAttribute(string methodName, string buttonName, bool useValue, BindingFlags flags = BindingFlags.Public | BindingFlags.Instance) {
            this.methodName = methodName;
            this.buttonName = buttonName;
            this.useValue = useValue;
            this.flags = flags;
        }
        public MethodButtonAttribute(string methodName, bool useValue, BindingFlags flags) : this (methodName, methodName, useValue, flags) {}
        public MethodButtonAttribute(string methodName, bool useValue) : this (methodName, methodName, useValue) {}
        public MethodButtonAttribute(string methodName, string buttonName, BindingFlags flags) : this (methodName, buttonName, false, flags) {}
        public MethodButtonAttribute(string methodName, string buttonName) : this (methodName, buttonName, false) {}
        public MethodButtonAttribute(string methodName, BindingFlags flags) : this (methodName, methodName, false, flags) {}
        public MethodButtonAttribute(string methodName) : this (methodName, methodName, false) {}
    }
}