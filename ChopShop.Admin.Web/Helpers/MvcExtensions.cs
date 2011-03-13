using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel,TEnum>> expression)
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

      

    }
}
