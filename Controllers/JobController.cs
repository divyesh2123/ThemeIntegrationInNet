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

        [HttpGet]
        public IActionResult EditJobFromJquery(int id)
        {
            var p = jobService.GetJob(id);
           
            p.JobTypeData = jobTypeService.GetJobType();
           
            
            return View("AddJobFromJquery", p);
        }

        [HttpPost]
        public IActionResult AddJobFromJson(JobInfomration jobInfomration)
        {
            if (jobInfomration.JobId > 0)
            {
                var p = jobService.UpdateJob(jobInfomration);

                return Json(new
                {
                    message = "Data Updated...",
                    result = p
                });
            }
            else
            {
               var k = jobService.AddJob(jobInfomration);

                return Json(new
                {
                    message = "Data Added...",
                    result = k
                });
            }


          
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
           var d = jobService.DeleteJob(id);

            return Json(new
            {
                message = "Data Deleted...",
                result = d
            });
        }



    }
}
