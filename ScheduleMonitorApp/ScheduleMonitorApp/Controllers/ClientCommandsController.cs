using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using ScheduleMonitorApp.ViewModels;
using SchedulerMonitorDataEntities.Entities;

namespace ScheduleMonitorApp.Controllers
{
    [Authorize]
    public class ClientCommandsController : Controller
    {
        private ScheduleMonitorDb db = new ScheduleMonitorDb();

        // GET: /ClientCommands/
        
        public async Task<ActionResult> Index(int clientId)
        {
            TempData["clientId"] = clientId;
            Session["ClientId"] = clientId;
            ViewData["ClientName"] = db.Clients.Find(clientId).ClientName;
            return View(await db.ClientCommands.Where(x=>x.ClientId == clientId && x.IsDeleted == false).ToListAsync());
        }

        // GET: /ClientCommands/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientCommand clientcommand = await db.ClientCommands.FindAsync(id);
            if (clientcommand == null)
            {
                return HttpNotFound();
            }
            return View(clientcommand);
        }

        // GET: /ClientCommands/Create
        public ActionResult Create()
        {
            ViewData["clientId"] = Session["ClientId"];
            return View(new NewCommandViewModel());
        }

        // POST: /ClientCommands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ClientCommandId,ClientId,Command,IsScheduled,IsExecuted")] NewCommandViewModel newCommandViewModel)
        {
            if (ModelState.IsValid)
            {
                ClientCommand clientcommand = JsonConvert.DeserializeObject<ClientCommand>(JsonConvert.SerializeObject(newCommandViewModel));
                clientcommand.IsExecuted = false;
                try
                {
                    clientcommand.ScheduledTime = new DateTime(newCommandViewModel.ScheduledDate.Value.Year, newCommandViewModel.ScheduledDate.Value.Month, newCommandViewModel.ScheduledDate.Value.Day, newCommandViewModel.ScheduledTime.Value.Hour, newCommandViewModel.ScheduledTime.Value.Minute, newCommandViewModel.ScheduledTime.Value.Second);
                }
                catch{}

                db.ClientCommands.Add(clientcommand);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { clientId = clientcommand.ClientId });
            }

            return View(newCommandViewModel);
        }

        // GET: /ClientCommands/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientCommand clientcommand = await db.ClientCommands.FindAsync(id);
            if (clientcommand == null)
            {
                return HttpNotFound();
            }
            return View(clientcommand);
        }

        // POST: /ClientCommands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClientCommandId,ClientId,Command,IsScheduled")] ClientCommand clientcommand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientcommand).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new{clientId = clientcommand.ClientId});
            }
            return View(clientcommand);
        }

        // GET: /ClientCommands/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientCommand clientcommand = await db.ClientCommands.FindAsync(id);
            if (clientcommand == null)
            {
                return HttpNotFound();
            }
            return View(clientcommand);
        }

        // POST: /ClientCommands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ClientCommand clientcommand = await db.ClientCommands.FindAsync(id);
            clientcommand.IsDeleted = true;
            //db.ClientCommands.Remove(clientcommand);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { clientId = clientcommand.ClientId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
