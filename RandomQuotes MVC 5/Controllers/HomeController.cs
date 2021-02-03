using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RandomQuotes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Coba()
        {
            return View();
        }

        public ActionResult QuoteRequest()
        {
            return View();
        }

        public ActionResult Generate(string[] indexQuotes)
        {
            string message = "";
            string quote = "";
            int index = 0;
            int sourceCount = 0;
            // use empty.txt for file contains empty and use random file name for check file not exist
            string pathFile = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/quotes.txt");

            if (System.IO.File.Exists(pathFile))
            {
                List<string> source = System.IO.File.ReadAllLines(pathFile).ToList();
                List<int> randIndex = new List<int>();

                sourceCount = source.Count;
                bool start = true;

                if (sourceCount > 0)
                {
                    if (indexQuotes != null)
                    {
                        foreach (string value in indexQuotes)
                        {
                            int result;
                            if (int.TryParse(value, out result))
                            {
                                if (result >= 0 && result < sourceCount)
                                {
                                    if (!randIndex.Contains(result))
                                        randIndex.Add(result);
                                }
                            }
                        }
                    }

                    // pass randIndex if null 
                    randIndex = randIndex ?? new List<int>();

                    // generate random start
                    while (start)
                    {
                        Random random = new Random();
                        index = random.Next(sourceCount);

                        if (!randIndex.Contains(index))
                            start = false;
                    }
                    quote = source[index];
                }
                else
                    message = "Not Found";
            }

            else
                message = "Not Found";

            // string date with format ex: Mon, 18 Jan 2021 09:20:45 GMT+07:00 (SE Asia Standard Time)
            string date = DateTime.Now.ToString("r") + DateTime.Now.ToString("zzz") + " (" + TimeZoneInfo.Local.StandardName + ")";

            return Json(new { index, quote, date, sourceCount, message }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Submit(string number)
        {
            string status = "";
            int result;

            if (int.TryParse(number, out result))
            {
                if (result > 0 && result <= 100000)
                    status = "valid";
                else
                    status = "novalid";
            }

            return Content(status, "text/plain");
        }

        public ActionResult Requested(string number, string[] resultQuotes)
        {
            string status = "";
            string quote = "";
            int sourceCount = 0;
            int input = 0;

            // use empty.txt for file contains empty and use random file name for check file not exist
            string pathFile = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/quotes.txt");

            if (System.IO.File.Exists(pathFile))
            {
                List<string> source = System.IO.File.ReadAllLines(pathFile).ToList();
                List<int> listIndex = new List<int>();

                sourceCount = source.Count;

                if (sourceCount > 0)
                {
                    if (resultQuotes != null)
                    {
                        foreach (string value in resultQuotes)
                        {
                            int result;
                            if (int.TryParse(value, out result))
                            {
                                if (result > 0 && result <= sourceCount)
                                {
                                    if (!listIndex.Contains(result))
                                        listIndex.Add(result);
                                }
                            }
                        }
                    }

                    // pass randIndex if null 
                    listIndex = listIndex ?? new List<int>();

                    if (number.Length < 5)
                    {
                        if (int.TryParse(number, out input))
                        {
                            // number valid and quotes exist
                            if (input > 0 && input <= sourceCount)
                            {
                                status = "valid";
                                quote = source[input - 1];

                                if (listIndex.Contains(input))
                                {
                                    status = "duplicate";
                                    if (listIndex.Count == sourceCount)
                                        status = "reset";
                                }

                            }
                            // number valid but quotes doesn't exit
                            else if (input > 0 && input > sourceCount)
                                status = "semivalid";
                            else
                                status = "novalid";
                        }                  
                    }
                    else
                        status = "novalid";
                }
                else
                    status = "error";
            }
            else
                status = "error";

            // string date with format ex: Mon, 18 Jan 2021 09:20:45 GMT+07:00 (SE Asia Standard Time)
            string date = DateTime.Now.ToString("r") + DateTime.Now.ToString("zzz") + " (" + TimeZoneInfo.Local.StandardName + ")";

            return Json(new { input, quote, date, sourceCount, status }, JsonRequestBehavior.AllowGet);

        }
    }
}