﻿using System.Reflection;

namespace TesteNumbeo.Core
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class CustomAttribute : Attribute
    {
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Translations resource file
        /// </summary>
        public Type ResourceType { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MarketCustomAttribute : CustomAttribute
    {
        public double Proportion { get; set; } = 1;
        public double Convert { get; set; } = 1;
    }

    public static class CustomAttributeHelper
    {
        public static string GetName(this Enum value, bool translate = true)
        {
            return value.GetCustomAttribute(translate).Name;
        }

        public static string GetDescription(this Enum value, bool translate = true)
        {
            return value.GetCustomAttribute(translate).Description;
        }

        public static CustomAttribute GetCustomAttribute(this Enum value, bool translate = true)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null) throw new NullReferenceException("fieldInfo null");

            var attr = fieldInfo.GetCustomAttribute(typeof(CustomAttribute)) as CustomAttribute;

            if (attr == null) throw new NullReferenceException("attr null");

            //if (translate && attr.ResourceType != null) //translations
            //{
            //    var rm = new ResourceManager(attr.ResourceType.FullName ?? "", attr.ResourceType.Assembly);

            //    if (rm == null) throw new NullReferenceException("ResourceManager null");

            //    if (!string.IsNullOrEmpty(attr.Name)) attr.Name = rm.GetString(attr.Name);
            //    if (!string.IsNullOrEmpty(attr.Description)) attr.Description = rm.GetString(attr.Description);
            //}

            return attr;
        }

        public static MarketCustomAttribute GetMarketCustomAttribute(this Enum value, bool translate = true)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null) throw new NullReferenceException("fieldInfo null");

            var attr = fieldInfo.GetCustomAttribute(typeof(MarketCustomAttribute)) as MarketCustomAttribute;

            if (attr == null) throw new NullReferenceException("attr null");

            //if (translate && attr.ResourceType != null) //translations
            //{
            //    var rm = new ResourceManager(attr.ResourceType.FullName ?? "", attr.ResourceType.Assembly);

            //    if (rm == null) throw new NullReferenceException("ResourceManager null");

            //    if (!string.IsNullOrEmpty(attr.Name)) attr.Name = rm.GetString(attr.Name);
            //    if (!string.IsNullOrEmpty(attr.Description)) attr.Description = rm.GetString(attr.Description);
            //}

            return attr;
        }

        public static CustomAttribute GetCustomAttribute(this Type type, string? name = null)
        {
            if (type == null) throw new NullReferenceException("attr null");

            CustomAttribute attr;

            if (string.IsNullOrEmpty(name))
            {
                attr = type.GetCustomAttribute(typeof(CustomAttribute)) as CustomAttribute;
            }
            else
            {
                var property = type.GetProperty(name) as MemberInfo;
                attr = property.GetCustomAttribute(typeof(CustomAttribute)) as CustomAttribute;
            }

            //if (attr.ResourceType != null) //translations
            //{
            //    var rm = new ResourceManager(attr.ResourceType.FullName, attr.ResourceType.Assembly);

            //    if (!string.IsNullOrEmpty(attr.Name)) attr.Name = rm.GetString(attr.Name);
            //    if (!string.IsNullOrEmpty(attr.Description)) attr.Description = rm.GetString(attr.Description);
            //}

            return attr;
        }
    }
}