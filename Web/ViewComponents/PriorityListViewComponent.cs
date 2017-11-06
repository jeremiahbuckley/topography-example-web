using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewComponentSample.Models;

namespace ViewComponentSample.ViewComponents
{
	public class PriorityListViewComponent : ViewComponent
	{

		public PriorityListViewComponent()
		{
		}

		public async Task<IViewComponentResult> InvokeAsync(
		int maxPriority, bool isDone)
		{
			var items = await GetItemsAsync(maxPriority, isDone);
			return View<IEnumerable<TodoItem>>(items);
		}
		private Task<IEnumerable<TodoItem>> GetItemsAsync(int maxPriority, bool isDone)
		{
			var task = Task<IEnumerable<TodoItem>>.Run(() =>
			{
				var list = new List<TodoItem>();
				list.Add(new TodoItem { Id = 1, Name = "A", Priority = 3, IsDone = false });
				IEnumerable<TodoItem> x = list;
				return x;
			});
			return task;
		}
	}
}