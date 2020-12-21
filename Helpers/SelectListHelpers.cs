using System;
using System.Collections.Generic;
using System.Linq;
using ContractAndProjectManager.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContractAndProjectManager.Helpers
{
    public static class SelectListHelpers
    {
        public static SelectList CreateSelectList<T>(IEnumerable<T> list, Func<T,SelectListModel> process, int selectId = default, bool withEmptyElement = false)
        {
            var items = list.Select(process).ToList();
            if (withEmptyElement)
                items = items.Prepend(new SelectListModel
                {
                    Id = 0,
                    Title = "-"
                }).ToList();
            return new SelectList(items, "Id", "Title", selectId);
        }
    }
}