using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXC_Project.Models;

namespace DXC_Project.Controllers
{
    public class DisplayQuestionsController : Controller
    {
        Questions dbcontext = new Questions();
        double correct = 0, skipped = 0, wrong = 0,correctaptitude=0,skippedaptitude=0,wrongaptitude=0, correcttechnical = 0, skippedtechnical = 0, wrongtechnical = 0,marks = 0,marksaptitude=0,markstechnical=0;
        // GET: DisplayQuestions

       
        public ActionResult Index12()
        {
            //Timer
            

            int numberapti = 20;
            int numberjava = 10;
            int numberpython = 10;
            int numbercsharp = 10;
            int experience = 2;
            List<string> Technology = new List<string>
            { 
                "java","csharp"
            };

            string querypython = String.Format("select top {0} * from Java_tbl1 where TECHST=2 order by newid()  ", numberpython);
            IEnumerable<Java_tbl1> listpython = dbcontext.Java_tbl1.SqlQuery(querypython).ToList();
            string queryjava = String.Format("select top {0} * from Java_tbl1  where TECHST=1 order by newid()", numberjava);
            IEnumerable<Java_tbl1> listjava = dbcontext.Java_tbl1.SqlQuery(queryjava).ToList();
            string querycsharp = String.Format("select top {0} * from Java_tbl1 where TECHST=3 order by newid()  ", numbercsharp);
            IEnumerable<Java_tbl1> listcsharp = dbcontext.Java_tbl1.SqlQuery(querycsharp).ToList();
            string queryaptitude = String.Format("select top {0} * from Java_tbl1 where TECHST=4 order by newid()  ", numberapti);
            IEnumerable<Java_tbl1> listaptitude = dbcontext.Java_tbl1.SqlQuery(queryaptitude).ToList();
            //IEnumerable<Java_tbl1> listall = listapti.Concat(listjava);
            //TempData["all"] = listall;

            TempData["python"] = listpython;
            TempData["java"] = listjava;
            TempData["csharp"] = listcsharp;
            TempData["apti"] = listaptitude;

            var model = new QuestionsContext
            {
                Questions_python = listpython,
                Questions_java = listjava,
                Questions_csharp=listcsharp,
                Questions_apti=listaptitude
            };
            if (Session["Rem_Time"] == null)
            {
                Session["Rem_Time"] = DateTime.Now.AddMinutes(1).ToString("dd-MM-yyyy h:mm:ss tt");
            }
            ViewBag.Rem_Time = Session["Rem_Time"];
            return View(model);
        }
        [HttpPost]
        //[ActionName("Index12")]
        public ActionResult Index12(FormCollection form)
        {
            int k = 0;
            IEnumerable<Java_tbl1> listpython = TempData["python"] as IEnumerable<Java_tbl1>;
            IEnumerable<Java_tbl1> listjava = TempData["java"] as IEnumerable<Java_tbl1>;
            IEnumerable<Java_tbl1> listcsharp = TempData["csharp"] as IEnumerable<Java_tbl1>;
            IEnumerable<Java_tbl1> listaptitude = TempData["apti"] as IEnumerable<Java_tbl1>;

            foreach (var item in listaptitude)
            {
                if (item.java_ans == form[k])
                {
                    ++correctaptitude;
                }
                else if (form[k] == "Not Attempted")
                {
                    ++skippedaptitude;
                }
                else
                {
                    ++wrongaptitude;
                }
                ++k;
            }
            

            foreach (var item in listjava)
            {
                if (item.java_ans == form[k])
                {
                    ++correcttechnical;
                }
                else if (form[k] == "Not Attempted")
                {
                    ++skippedtechnical;
                }
                else
                {
                    ++wrongtechnical;
                }
                ++k;
            }
            foreach (var item in listpython)
            {
                if (item.java_ans == form[k])
                {
                    ++correcttechnical;
                }
                else if (form[k] == "Not Attempted")
                {
                    ++skippedtechnical;
                }
                else
                {
                    ++wrongtechnical;
                }
                ++k;
            }
            foreach (var item in listcsharp)
            {
                if (item.java_ans == form[k])
                {
                    ++correcttechnical;
                }
                else if (form[k] == "Not Attempted")
                {
                    ++skippedtechnical;
                }
                else
                {
                    ++wrongtechnical;
                }
                ++k;
            }
            
            marksaptitude= correctaptitude - (wrongaptitude * 0.25);
            markstechnical = correcttechnical - (wrongtechnical * 0.25);
            marks = marksaptitude+markstechnical;
            correct = correctaptitude + correcttechnical;
            wrong = wrongaptitude + wrongtechnical;
            skipped = skippedaptitude + skippedtechnical;
            Dictionary<string, double> result = new Dictionary<string, double>() { { "Marks", marks }, { "Correct", correct }, { "Wrong", wrong }, { "Skipped", skipped },
            { "AptitudeMarks", marksaptitude }, { "AptitudeCorrect", correctaptitude }, { "AptitudeWrong", wrongaptitude }, { "AptitudeSkipped", skippedaptitude },
            { "TechnicalMarks", markstechnical }, { "TechnicalCorrect", correcttechnical }, { "TechnicalWrong", wrongtechnical }, { "TechnicalSkipped", skippedtechnical }};
            TempData["result"] = result;
            return RedirectToAction("Submit12");
        }
        public ActionResult Submit12()
        {
            ViewBag.total = TempData["result"];
            return View();
        }
       
    }
}