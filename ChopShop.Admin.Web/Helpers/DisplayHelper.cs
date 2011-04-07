using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ChopShop.Admin.Web.Models.ViewModel;
using ChopShop.Model;
using System.Web.Mvc.Html;

namespace ChopShop.Admin.Web.Helpers
{
    public static class DisplayHelper
    {
        public static string ProductPriceForList(IEnumerable<Price> prices)
        {
            return string.Empty;
        }

        public static string ProductCategoriesForList(IEnumerable<string> categories)
        {
            var builder = new StringBuilder();
            if(categories != null && categories.Any())
                {
                    foreach (var category in categories)
                    {
                        builder.Append(category).Append(", ");
                    }
                }

            var result = builder.ToString();
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 2);
            }

            return result;
        }

         public static MvcHtmlString HeaderLink<TModel>(this HtmlHelper helper, Expression<Func<TModel, object>> expression, IDictionary<string, object> htmlAttributes)
         {
             var displayName = MvcExtensions.GetDisplayName<TModel>(expression);
             bool ascending = false;
             if(!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["asc"]))
             {
                 ascending = (HttpContext.Current.Request.QueryString["asc"]).ToUpper() == "TRUE";
             }

             string propertyName = GetPropertyName(expression);

             var currentAction = (string) helper.ViewContext.RouteData.Values["action"];

             var routeValueDictionary = new RouteValueDictionary(helper.ViewContext.RouteData.Values);
             var queryStrings = helper.ViewContext.HttpContext.Request.QueryString;

             foreach (string queryString in queryStrings)
             {
                 routeValueDictionary.Add(queryString,queryStrings[queryString]);
             }

             if (routeValueDictionary.ContainsKey("orderBy"))
             {
                 routeValueDictionary["orderBy"] = propertyName;
             }
             else
             {
                 routeValueDictionary.Add("orderBy", propertyName);
             }

             if (routeValueDictionary.ContainsKey("asc"))
             {
                 routeValueDictionary["asc"] = !ascending;
             }
             else
             {
                 routeValueDictionary.Add("asc", !ascending);
             }

             return helper.ActionLink(displayName, currentAction,routeValueDictionary, htmlAttributes);
         }

        private static string GetPropertyName<TModel>(Expression<Func<TModel, object>> expression)
        {
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

            //the property name is what we're after
            return propertyList.Last();
        }

        public static MvcHtmlString PagingLink(this HtmlHelper helper, string displayName, int page, int perPage, IDictionary<string, object> htmlAttributes)
        {
            var currentAction = (string)helper.ViewContext.RouteData.Values["action"];

            var routeValueDictionary = new RouteValueDictionary(helper.ViewContext.RouteData.Values);
            var queryStrings = helper.ViewContext.HttpContext.Request.QueryString;

            foreach (string queryString in queryStrings)
            {
                routeValueDictionary.Add(queryString, queryStrings[queryString]);
            }

            if (routeValueDictionary.ContainsKey("page"))
            {
                routeValueDictionary["page"] = page;
            }
            else
            {
                routeValueDictionary.Add("page", page);
            }

            if (routeValueDictionary.ContainsKey("perPage"))
            {
                routeValueDictionary["perPage"] = perPage;
            }
            else
            {
                routeValueDictionary.Add("perPage", perPage);
            }

            return helper.ActionLink(displayName, currentAction, routeValueDictionary, htmlAttributes);
        }
    }
}