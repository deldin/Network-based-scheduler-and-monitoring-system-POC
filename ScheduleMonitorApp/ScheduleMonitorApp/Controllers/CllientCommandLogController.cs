using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ScheduleMonitorApp.Entities;
using ScheduleMonitorApp.Models;
using ScheduleMonitorApp.ViewModels;

namespace ScheduleMonitorApp.Controllers
{
    [Authorize]
    public class CllientCommandLogController : Controller
    {
        private ScheduleMonitorDb db = new ScheduleMonitorDb();
        //private ScheduleMonitorModel _scheduleMonitorModel = new ScheduleMonitorModel(new ScheduleMonitorDb());

        // GET: /CllientCommandLog/
        public async Task<ActionResult> Index(int clientCommandId)
        {
            return View(await db.ClientCommandLogs.Where(x=>x.ClientCommandId == clientCommandId).ToListAsync());
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
            return RedirectToAction("Index", new { clientCommandId = clientcommandlog.ClientCommandId });
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
