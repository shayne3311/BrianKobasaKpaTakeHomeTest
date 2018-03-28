using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KpaTakeHomeTest.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using KpaTakeHomeTest.Services;
using System.Text.RegularExpressions;

namespace KpaTakeHomeTest.Controllers
{
    public class HomeController : Controller
    {
        private IKpaTakeHomeTestRepository _kpaTakeHomeTestRepository;
        public HomeController(IKpaTakeHomeTestRepository kpaTakeHomeTestRepository)
        {
            _kpaTakeHomeTestRepository = kpaTakeHomeTestRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Part1FileUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FileUploaded(IFormFile file)
        {
            // full path to file in temp location
            if (file.Length > 0)
            {
                try
                {
                    using (var stream = new StreamReader(file.OpenReadStream()))
                    {
                        //Skip the first line 
                        stream.ReadLine();

                        while (stream.Peek() >= 0)
                        {
                            // extract the fields
                            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                            String[] ThisRow = CSVParser.Split(await stream.ReadLineAsync());

                            User NewUser = new User()
                            {
                                EmailAddress = ThisRow[0].Replace("\"", ""),
                                FirstName = ThisRow[1].Replace("\"", ""),
                                LastName = ThisRow[2].Replace("\"", "")
                            };

                            _kpaTakeHomeTestRepository.AddUser(NewUser);
                        }

                        if (!_kpaTakeHomeTestRepository.Save())
                        {
                            ViewBag.StatusMessage = "There was an error processing your request.";
                        return RedirectToAction("Part1FileUpload");
                        }
                        return RedirectToAction("Part1ViewUploadedData");
                    }
                }
                catch
                {
                    //Return any errors
                    ViewBag.StatusMessage = "There was an error in the file format, please check file and reupload";
                        return RedirectToAction("Part1FileUpload");
                }
            }

            //Return any errors
            ViewBag.StatusMessage = "No File was selected";
                        return RedirectToAction("Part1FileUpload");
        }


        public IActionResult Part1ViewUploadedData()
        {
            var model = _kpaTakeHomeTestRepository.GetUsers();
            return View(model);
        }

        public IActionResult Part2WeatherApp()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
