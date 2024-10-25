using Microsoft.AspNetCore.Mvc;
using ThemeIntegrationInNet.BussinessEntity;
using ThemeIntegrationInNet.BussinessService.Concreate;
using ThemeIntegrationInNet.BussinessService.Interface;

namespace ThemeIntegrationInNet.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService jobService;
        private readonly IJobTypeService jobTypeService;


        public JobController(IJobService _jobService, IJobTypeService _jobTypeService)

        {
            jobService = _jobService;

            jobTypeService = _jobTypeService;
        }

        public IActionResult Index()
        {
            var d = jobService.GetJobInformation();
            return View(d);
        }

        [HttpGet]
        public IActionResult AddJob()
        {
            JobInfomration jobInfomration = new JobInfomration();
            jobInfomration.JobTypeData = jobTypeService.GetJobType();
            return View(jobInfomration);
        }

        [HttpPost]
        public IActionResult AddJob(JobInfomration jobInfomration)
        {
            if (!ModelState.IsValid)
            {
                return View(jobInfomration);
            }

            if (jobInfomration.JobId > 0)
            {
                jobService.UpdateJob(jobInfomration);
            }
            else
            {
                jobService.AddJob(jobInfomration);
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteJobInfo(int jobId)
        {
            var p = jobService.DeleteJob(jobId);
            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id)
        {
            var p = jobService.GetJob(id);
            p.JobTypeData = jobTypeService.GetJobType();

            return View("AddJob", p);
        }

        public IActionResult DisplayData()
        {
            var d = jobService.GetJobInformation();
            return Json(new { data = d });
        }

        public IActionResult IndexJqueryData()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddJobFromJquery()
        {
            JobInfomration jobInfomration = new JobInfomration();
            jobInfomration.JobTypeData = jobTypeService.GetJobType();
            return View(jobInfomration);
        }

        [HttpPost]
        public IActionResult AddJobFromJson(JobInfomration jobInfomration)
        {
            var d = jobService.AddJob(jobInfomration);

            return Json(new
            {
                message = "Data Saved...",
                result = d
            });
        }

        
    }
}
