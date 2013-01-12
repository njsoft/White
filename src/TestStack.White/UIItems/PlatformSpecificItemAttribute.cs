using System;

namespace White.Core.UIItems
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PlatformSpecificItemAttribute : Attribute
    {
        private Type referAsType;

        public virtual Type ReferAsType
        {
            get { return referAsType; }
            set { referAsType = value; }
        }

        public static Type BaseType(Type type)
        {
            if (type == null) return null;
            object[] platformSpecificAttributes = type.GetCustomAttributes(typeof (PlatformSpecificItemAttribute), false);
            if (platformSpecificAttributes.Length == 0) return type;

            var platformSpecificItemAttribute = (PlatformSpecificItemAttribute) platformSpecificAttributes[0];
            return platformSpecificItemAttribute.referAsType ?? BaseType(type.BaseType);
        }
    }
}