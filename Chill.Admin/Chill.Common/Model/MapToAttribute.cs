using System;

namespace Chill.Common
{
    public class MapToAttribute : Attribute
    {
        public MapToAttribute(Type targetType)
        {
            TargetType = targetType;
        }
        public Type TargetType { get; }
    }
}
