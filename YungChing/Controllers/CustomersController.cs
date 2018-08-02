using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YungChing.Models;
using YungChing.Repositories;
using PagedList;

namespace YungChing.Controllers
{
	public class CustomersController : Controller
	{
		//private NorthwindEntities db = new NorthwindEntities();
		private IRepository<Customers> repo;

		private int pageSize = 5;

		public CustomersController()
			: this(new EFGenericRepository<Customers>(new NorthwindEntities()))
		{
		}

		public CustomersController(IRepository<Customers> inRepo)
		{
			repo = inRepo;
		}

		// GET: Customers
		public ActionResult Index(int page = 1)
		{
			int currentPage = page < 1 ? 1 : page;		

			var customers = repo.Reads().OrderBy(x => x.CustomerID);

			var result = customers.ToPagedList(currentPage, pageSize);

			//return View(db.Customers.ToList());
			return View(result);
		}

		// GET: Customers/Details/5
		public ActionResult Details(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			//Customers customers = db.Customers.Find(id);
			Customers customers = repo.Read(x => x.CustomerID == id);
			if (customers == null)
			{
				return HttpNotFound();
			}
			return View(customers);
		}

		// GET: Customers/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Customers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customers customers)
		{
			if (ModelState.IsValid)
			{
				repo.Create(customers);
				repo.SaveChanges();

				return RedirectToAction("Index");
			}

			return View(customers);
		}

		// GET: Customers/Edit/5
		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Customers customers = repo.Read(x => x.CustomerID == id);
			if (customers == null)
			{
				return HttpNotFound();
			}
			return View(customers);
		}

		// POST: Customers/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customers customers)
		{
			if (ModelState.IsValid)
			{
				repo.Update(customers);
				repo.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(customers);
		}

		// GET: Customers/Delete/5
		public ActionResult Delete(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Customers customers = repo.Read(x => x.CustomerID == id);
			if (customers == null)
			{
				return HttpNotFound();
			}
			return View(customers);
		}

		// POST: Customers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(string id)
		{
			Customers customers = repo.Read(x => x.CustomerID == id);
			repo.Delete(customers);
			repo.SaveChanges();

			return RedirectToAction("Index");
		}

	}
}
