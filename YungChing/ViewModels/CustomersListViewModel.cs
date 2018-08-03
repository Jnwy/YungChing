using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YungChing.ViewModels
{
	public class CustomersListViewModel
	{
		public int PageIndex { get; set; }
		public IPagedList<YungChing.Models.Customers> Customers { get; set; }
		public CustomersSearchModel SearchParameter { get; set; }

	}
}