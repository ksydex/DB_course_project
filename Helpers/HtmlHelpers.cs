using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContractAndProjectManager.Helpers
{
    public static class HtmlHelpers
    {

        #region [ EntityIndexHeader ]

        public static IHtmlContent EntityIndexHeader<TModel>(
            this IHtmlHelper<TModel> htmlHelper, string header, string searchString = default)
        {
            var type = typeof(TModel).GetInterface(nameof(IEnumerable)) == null
                ? typeof(TModel)
                : typeof(TModel).GetGenericArguments()[0];

            var controllerName = type.Name.Replace("Proxy", "");

            var result = @$"
                <div class='row d-flex justify-content-between align-items-center mb-3'>
                 <h1 class='m-0 col-md-6 mb-2 mb-md-0'>{header}</h1> 
                 <div class='d-flex col-md-6 justify-content-md-end'>
                         <form method='get' class='w-100'>
                <div class='input-group'>
                <input type='text' class='form-control' placeholder='Поиск' aria-label='Поиск' value='{searchString}' name='searchString'>"
                         +
                         (searchString == default
                             ? ""
                             : "<div class='input-group-append'>" +
                               ActionLink(htmlHelper, "✖", "Index", controllerName, null,
                                   new { @class = "btn btn-delete" })
                               + "</div>"
                         ) +
                         @$"<div class='input-group-append'>
                <input value='Найти' type='submit' class='btn btn-primary'>
                </div></div></form>"
                         +
                         ActionLink(htmlHelper, "Добавить", "Create", controllerName, null,
                             new { @class = "btn btn-primary ml-2" })
                         +
                         @"</div>
                </div>";

            return new HtmlString(result);
        }

        #endregion

        #region [ TableRowActions ]

        public static IHtmlContent TableRowActions<TModel>(
            this IHtmlHelper htmlHelper, TModel model, Func<TModel, List<TableActionButton>> appendButtons = null,
            bool editAction = true, bool deleteAction = true)
        {
            var id = model.GetType().GetProperty("Id")?.GetValue(model, null);
            var controllerName = model.GetType().Name.Replace("Proxy", "");

            var appended = appendButtons?.Invoke(model).Aggregate("", (acc, x) => acc +
                ActionLink(htmlHelper, x.Text,
                    x.Action, x.Controller, x.Arguments,
                    new { @class = "btn " + (x.Class ?? "btn-outline-primary") })) ?? "";

            var result = "<div class='btn-group btn-group-sm'>" +
                         (editAction
                             ? ActionLink(htmlHelper, "Подробнее", "Edit", controllerName, new { id },
                                 new { @class = "btn btn-outline-primary" })
                             : "") +
                         (deleteAction
                             ? ActionLink(htmlHelper, "Удалить", "Delete", controllerName, new { id },
                                 new { @class = "btn btn-outline-primary" })
                             : "") +
                         appended
                         + "</div>";
            return new HtmlString(result);
        }

        #endregion

        #region [ Table ]

        public static IHtmlContent Table<TModel>(
            this IHtmlHelper htmlHelper, IEnumerable<TModel> lst,
            Func<TModel, Dictionary<string, object>> func, Func<TModel, List<TableActionButton>> appendButtons = null,
            bool editAction = true, bool deleteAction = true, bool header = true)
            where TModel : new()
        {
            var list = lst.ToList();
            var showButtons = appendButtons != null || editAction || deleteAction;

            var headerDict = func(list.FirstOrDefault() ?? new TModel());
            var result = @$"
            <table class='table'>
                <thead>" 
                + (header ? @$"<tr>
                {headerDict.Aggregate("", (acc2, y) => acc2 + @$"
                        <th class='{(y.Key == headerDict.FirstOrDefault().Key ? "pl-3" : "")}'>{y.Key}</th>
                        ")
                             }" +
                         (showButtons ? "<th></th>" : "") +
                         "</tr>" : "") +
                @"</thead>
                <tbody>"
                         + list.Aggregate("", (acc, x) => acc + @$"
                <tr>
                   {func(x).Aggregate("", (acc2, y) => acc2 + @$"
                        <td class='{(y.Key == headerDict.FirstOrDefault().Key ? "pl-3" : "")}'>{y.Value?.ToString()}</td>
                        ")
                                                                  }"
                                                              + (showButtons
                                                                  ? "<td>" + HtmlContentToString(
                                                                      htmlHelper.TableRowActions(x, appendButtons,
                                                                          editAction, deleteAction)) + "</td>"
                                                                  : "") +
                                                              "</tr>"
                         )
                         + @$"</tbody>
                </table>
            ";

            return new HtmlString(result);
        }

        #endregion

        #region [ Helper methods ]

        private static string ActionLink(IHtmlHelper helper, string linkText, string actionName, string controllerName,
            object routeValues, object htmlAttributes = null)
            => HtmlContentToString(helper.ActionLink(linkText, actionName, controllerName, routeValues,
                htmlAttributes));

        private static string HtmlContentToString(IHtmlContent content)
        {
            using var writer = new StringWriter();
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }

        #endregion
    }
}