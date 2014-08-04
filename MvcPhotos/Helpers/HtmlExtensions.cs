using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// html扩展方法 生成img标签
        /// </summary>
        /// <param name="html"></param>
        /// <param name="id">img标签的id</param>
        /// <param name="src">img标签的src</param>
        /// <param name="alternateText">img标签的提示文本</param>
        /// <param name="htmlAttributes">img标签的属性</param>
        /// <returns></returns>
        public static MvcHtmlString Image(
            this HtmlHelper html,
            string id,
            string src,
            string alternateText = "",
            object htmlAttributes = null)
        {
            //将相对路径转换为绝对路径
            string filePath = HttpContext.Current.Server.MapPath(src);
            if (!System.IO.File.Exists(filePath))
                src = "/Content/images/nophoto.png";

            var tb = new TagBuilder("img");
            tb.GenerateId(id);
            tb.MergeAttribute("src", src);
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