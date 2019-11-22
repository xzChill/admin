using System;

namespace Chill.Common
{
    public class MapFromAttribute : Attribute
    {
        public MapFromAttribute(Type fromType)
        {
            FromType = fromType;
        }
        public Type FromType { get; }
    }
}
