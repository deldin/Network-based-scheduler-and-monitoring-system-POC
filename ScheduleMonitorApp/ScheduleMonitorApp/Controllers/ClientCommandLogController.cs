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
    public class ClientCommandLogController : Controller
    {
        private ScheduleMonitorDb db = new ScheduleMonitorDb();
        //private ScheduleMonitorModel _scheduleMonitorModel = new ScheduleMonitorModel(new ScheduleMonitorDb());

        // GET: /CllientCommandLog/
        public async Task<ActionResult> Index(int clientCommandId)
        {
            Session["clientCommandId"] = clientCommandId;
            ViewData["ClientId"] = db.ClientCommands.Find(clientCommandId).ClientId;
            return View(await db.ClientCommandLogs.Where(x=>x.ClientCommandId == clientCommandId).ToListAsync());
        }

        public async Task<ActionResult> Logs()
        {
            Session["clientCommandId"] = null;

           //// using (var dbContext = new ScheduleMonitorDb())
           // {
           //     var query = (from e in db.ClientCommands
           //                 join d in db.ClientCommandLogs on e.ClientCommandId equals d.ClientCommandId
           //                 select new { d.LogType, e.Command, d.LogText }).ToList();
           //     return View(query);
           // }
           
            return View(await db.ClientCommandLogs.ToListAsync());
        }

        // GET: /CllientCommandLog/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientCommandLog clientcommandlog = await db.ClientCommandLogs.FindAsync(id);
            if (clientcommandlog == null)
            {
                return HttpNotFound();
            }
            return View(clientcommandlog);
        }

        // GET: /CllientCommandLog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CllientCommandLog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ClientCommandLogId,LogType,LogText,ClientCommandId")] ClientCommandLog clientcommandlog)
        {
            if (ModelState.IsValid)
            {
                db.ClientCommandLogs.Add(clientcommandlog);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new{clientCommandId = clientcommandlog.ClientCommandId});
            }

            return View(clientcommandlog);
        }

        // GET: /CllientCommandLog/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientCommandLog clientcommandlog = await db.ClientCommandLogs.FindAsync(id);
            if (clientcommandlog == null)
            {
                return HttpNotFound();
            }
            var serializedString = JsonConvert.SerializeObject(clientcommandlog);
            var outputLogViewModel = JsonConvert.DeserializeObject<OutputLogViewModel>(serializedString);
            //outputLogViewModel.ddlLogType = new SelectList(_scheduleMonitorModel.getAllLogTypes(), "LogType", "LogType", "Output Log");

            return View(outputLogViewModel);
        }

        //// POST: /CllientCommandLog/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include="ClientCommandLogId,LogType,LogText,ClientCommandId")] ClientCommandLog clientcommandlog)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(clientcommandlog).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index", new { clientCommandId = clientcommandlog.ClientCommandId });
        //    }
        //    return View(clientcommandlog);
        //}

        // POST: /CllientCommandLog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClientCommandLogId,LogType,LogText,ClientCommandId")] OutputLogViewModel output)
        {
            if (ModelState.IsValid)
            {
                var clientcommandlog = JsonConvert.DeserializeObject<ClientCommandLog>(JsonConvert.SerializeObject(output));
                db.Entry(clientcommandlog).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { clientCommandId = clientcommandlog.ClientCommandId });
            }
            return View(output);
        }

        // GET: /CllientCommandLog/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientCommandLog clientcommandlog = await db.ClientCommandLogs.FindAsync(id);
            if (clientcommandlog == null)
            {
                return HttpNotFound();
            }
            return View(clientcommandlog);
        }

        // POST: /CllientCommandLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ClientCommandLog clientcommandlog = await db.ClientCommandLogs.FindAsync(id);
            db.ClientCommandLogs.Remove(clientcommandlog);
            await db.SaveChangesAsync();
            if (Session["clientCommandId"] == null)
                return RedirectToAction("Logs");

            return RedirectToAction("Index", new { clientCommandId = Session["clientCommandId"] });
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
