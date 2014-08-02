using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcPhotos.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString File(this HtmlHelper html, string name)
        {
            var tb = new TagBuilder("input");
            tb.MergeAttribute("type", "file");
            tb.MergeAttribute("name", name);
            tb.GenerateId(name);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString FileFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression)
        {
            string name = GetFullPropertyName<TModel, TProperty>(expression);
            return html.File(name);
        }

        public static MvcHtmlString Image(
            this HtmlHelper html,
            string id,
            string url,
            string alternateText = "",
            object htmlAttributes = null)
        {
            var tb = new TagBuilder("img");
            tb.GenerateId(id);
            tb.MergeAttribute("src", url);
            tb.MergeAttribute("alt", alternateText);
            tb.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        #region 获取表达式成员的字段或属性全名
        static string GetFullPropertyName<T, TProperty>(Expression<Func<T, TProperty>> exp)
        {
            MemberExpression memberExp;

            if (!TryFindMemberExpression(exp.Body, out memberExp))
                return string.Empty;

            var memberNames = new Stack<string>();

            do
            {
                memberNames.Push(memberExp.Member.Name);
            } while (TryFindMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }

        static bool TryFindMemberExpression(Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;

            if (memberExp != null)
                return true;

            if (IsConversion(exp) && exp is UnaryExpression)
            {
                memberExp = ((UnaryExpression)exp).Operand as MemberExpression;

                if (memberExp != null)
                    return true;
            }

            return false;
        }

        static bool IsConversion(Expression exp)
        {
            return (
                exp.NodeType == ExpressionType.Convert
                ||
                exp.NodeType == ExpressionType.ConvertChecked);
        }
        #endregion
    }
}