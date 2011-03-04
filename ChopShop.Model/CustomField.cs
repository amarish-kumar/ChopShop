using System;

namespace ChopShop.Model
{
    public struct CustomField
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public CustomFieldType CustomFieldType { get; set; }
    }
}   