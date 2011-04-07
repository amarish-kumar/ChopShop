using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;

namespace ChopShop.Admin.Web.Helpers
{
    public static class MvcExtensions
    {
        public static string ToJson(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJson(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer { RecursionLimit = recursionDepth };
            return serializer.Serialize(obj);
        }

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            Type baseEnumType = Enum.GetUnderlyingType(typeof(TEnum));
            IEnumerable<SelectListItem> items = values.Select(x => new SelectListItem
                                                                       {
                                                                           Text = x.ToString(),
                                                                           Value = Convert.ChangeType(x, baseEnumType).ToString(),
                                                                           Selected = x.Equals(metadata.Model)
                                                                       });

            return htmlHelper.DropDownListFor(expression, items);
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, int> key, string defaultOption)
        {
            var items = enumerable.Select(x => new SelectListItem
            {
                Text = key(x).ToString(),
                Value = key(x).ToString(),
                Selected = false
            }).ToList();

            items.Insert(0, new SelectListItem
            {
                Text = defaultOption,
                Value = "-1",
                Selected = true
            });

            return items;
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, string> value, string defaultOption)
        {
            var items = enumerable.Select(x => new SelectListItem
            {
                Text = text(x),
                Value = value(x),
                Selected = false
            }).ToList();

            items.Insert(0, new SelectListItem
            {
                Text = defaultOption,
                Value = "-1",
                Selected = true
            });

            return items;
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> enumerable, Func<T, string> text, Func<T, int> value, string defaultOption)
        {
            var items = enumerable.Select(x => new SelectListItem
            {
                Text = text(x),
                Value = value(x).ToString(),
                Selected = false
            }).ToList();

            items.Insert(0, new SelectListItem
            {
                Text = defaultOption,
                Value = "-1",
                Selected = true
            });

            return items;
        }

        public static string GetDisplayName<TModel>(Expression<Func<TModel, object>> expression)
        {
            //http://stackoverflow.com/questions/5474460/get-displayname-attribute-of-a-property-in-strongly-typed-way
            Type type = typeof(TModel);

            IEnumerable<string> propertyList;
            //unless it's a root property the expression NodeType will always be Convert
            switch (expression.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = expression.Body as UnaryExpression;
                    propertyList = (ue != null ? ue.Operand : null).ToString().Split(".".ToCharArray()).Skip(1); //don't use the root property
                    break;
                default:
                    propertyList = expression.Body.ToString().Split(".".ToCharArray()).Skip(1);
                    break;
            }

            //the propert name is what we're after
            string propertyName = propertyList.Last();
            //list of properties - the last property name
            string[] properties = propertyList.Take(propertyList.Count() - 1).ToArray();

            Expression expr = null;
            foreach (string property in properties)
            {
                PropertyInfo propertyInfo = type.GetProperty(property);
                expr = Expression.Property(expr, type.GetProperty(property));
                type = propertyInfo.PropertyType;
            }

            var attr = (DisplayNameAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();

            // Look for [MetadataType] attribute in type hierarchy
            // http://stackoverflow.com/questions/1910532/attribute-isdefined-doesnt-see-attributes-applied-with-metadatatype-class
            if (attr == null)
            {
                var metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(propertyName);
                    if (property != null)
                    {
                        attr = (DisplayNameAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
                    }
                }
            }
            return (attr != null) ? attr.DisplayName : string.Empty;
        }


    }
}
