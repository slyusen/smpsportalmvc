using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using SmpsPortal.Core;
using SmpsPortal.Core.Models;
using SmpsPortal.Core.ViewModels;
using SmpsPortal.Persistence;
using SmpsPortal.Persistence.Repository;
using Microsoft.AspNet.Identity;
using Syncfusion.JavaScript;
using Syncfusion.JavaScript.DataSources;
using Syncfusion.Linq;

namespace SmpsPortal.Controllers.Academics
{
    public class AcademicResultsController : Controller
    {
        private IUnitofWork _unitofWork;
        string url;
        private MenuRepository menuRepo;

        private List<AcademicResultList> myResults;
        public AcademicResultsController()
        {
            _unitofWork = new UnitofWork(ApplicationDbContext.Create());
            myResults = new List<AcademicResultList>();
            menuRepo = new MenuRepository();
        }
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var roleList = ((ClaimsIdentity)User.Identity).Claims
                .Where(ck => ck.Type == ClaimTypes.Role)
                .Select(c => c.Value).ToList();


            var menus = menuRepo.GetCapableMenusWithMenuItems(roleList);
            var cclass = _unitofWork._SchoolClassRepository.GetAllSchoolClass();


            var pModel = new AcademicResultsViewModel()
            {
                Menus = menus,
                Heading = "Compute Results",
                ClassList = cclass,


            };
            ViewBag.datasource = null;
            return View(pModel);
        }

        public ActionResult GetDataSource(DataManager dm)
        {
            var userId = User.Identity.GetUserId();
            var scclass = _unitofWork._SchoolClassRepository.GetAllSchoolClass().First();
            var DataSource = _unitofWork._GradeRepository.GetAllGradesByClassYearTerm(scclass.Id).ToList();
            foreach (var da in DataSource)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);
                var student = _unitofWork._StudentRepository.GetStudentById(da.StudentId);
               
                var course = _unitofWork._CourseRepository.GetCourseById(da.CourseId);
                var gSys = _unitofWork._GradeRepository.GetGradeSystemById(da.GradeSystemId);

                myResults.Add(new AcademicResultList(da.Id, student.FullName, da.ExamScore, da.TotalCAScore, course.Code, scclass.Code, gSys.Grade));
            }
            DataResult result = new DataResult();
            DataOperations operation = new DataOperations();
            result.result = myResults;
            result.count = result.result.AsQueryable().Count();
            if (dm.Skip > 0)
                result.result = operation.PerformSkip(result.result, dm.Skip);
            if (dm.Take > 0)
                result.result = operation.PerformTake(result.result, dm.Take);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public string GetSiteUrl()
        {
            string siteUrl = string.Empty;
            var request = HttpContext.Request;
            if (request.IsSecureConnection)
                siteUrl = "https://";
            else
                siteUrl = "http://";
            siteUrl += request.Url.Host + "/";
            return siteUrl;
        }

        public JsonResult ComputeResult(int classId)
        {
            url = GetSiteUrl();
            var school = _unitofWork._SchoolClassRepository.GetSchoolConfigByWebAddress("http://localhost");
            var gradesChck = _unitofWork._GradeRepository.CheckGradesByClassYearTerm(classId);
            if (gradesChck)
            {

            }
            else
            {
                var courses = _unitofWork._CourseRepository.GetAllCoursesByClass(classId);

                foreach (var course in courses)
                {
                    double caHighScore = 0;
                    double totalScore = 0;
                    double totalCAPercent = 0;

                    var Cas = _unitofWork._AssessmentRepository.GetAssessmentsByCourseClassChosen(course.Id, classId);
                    var students = _unitofWork._StudentRepository.GetAllActiveStudentsByClass(classId);
                    foreach (var c in Cas)
                    {
                        totalCAPercent += c.PercentofCa;
                    }
                    if (totalCAPercent != 100)
                    {

                    }
                    else if (totalCAPercent == 100)
                    {
                        foreach (var student in students)
                        {
                            var cReg = _unitofWork._CourseRegisterRepository.CheckCourseByStudentClass(course.Id, student.Id, classId);

                            if (course.Category == CourseCategory.Compulsory || cReg)
                            {
                                double caScore = 0;
                                double examScore = 0;
                                var exams = _unitofWork._ExamRepository.GetExamsByCourseClass(course.Id, classId);
                                foreach (var c in Cas)
                                {
                                    caHighScore = (c.PercentofCa / 100) * school.CAPercent;
                                    var cares = _unitofWork._CaResultRepository.GetCaResultByCaStudent(c.Id, student.Id);
                                    var caresExists = _unitofWork._CaResultRepository.CaResultByStudentCaIdExists(c.Id, student.Id);
                                    if (caresExists)
                                        caScore += (cares.Score * caHighScore) / c.HighestScore;
                                } // calculate total score for chosen CAs

                                foreach (var exam in exams)
                                {
                                    var examResult = _unitofWork._ExamResultRepository.GetExamResultByExamStudent(exam.Id, student.Id);
                                    var exres = _unitofWork._ExamResultRepository.ExamResultByStudentExamIdExists(exam.Id, student.Id);
                                    if (exres)
                                        examScore += (examResult.Score * school.ExamPercent) / exam.HighestScore;
                                }//Calculate Total Score of Exams

                                totalScore = caScore + examScore;
                                var gradSys = _unitofWork._GradeRepository.GetGradeSystemByScoreBetween(totalScore);
                                var grade = new Grade();
                                grade.SchoolClassId = classId;
                                grade.CourseId = course.Id;
                                grade.StudentId = student.Id;
                                grade.GradeSystemId = gradSys.Id;
                                grade.YearTermId = school.CurrentYearTermId;
                                grade.ExamScore = Math.Round(examScore, 2);
                                grade.TotalCAScore = Math.Round(caScore, 2);

                                _unitofWork._GradeRepository.AddGrade(grade);
                                _unitofWork.Complete();


                            }//check to see if student is registered for the elective course
                            else
                            {

                            }
                        }// For each student under each course

                    }//if Total CA Percent equals 100

                }//courses

                var acadas = _unitofWork._AcademicRecordRepository.GetAcademicRecordByClassYearTerm(classId);

                if (acadas.Count() > 0)
                {

                }
                else
                {
                    var students2 = _unitofWork._StudentRepository.GetAllActiveStudentsByClass(classId);

                    foreach (var student in students2)
                    {
                        double cummPoints2 = 0;
                        var gradesRec = _unitofWork._GradeRepository.GetAllGradesByStudentClassYearTerm(student.Id, classId);
                        foreach (var rec in gradesRec)
                        {
                            var gradest = _unitofWork._GradeRepository.GetGradeSystemById(rec.GradeSystemId);
                            cummPoints2 += gradest.Points;
                        }

                        var acada = new AcademicRecord();
                        acada.CummulativePoints = cummPoints2;
                        acada.SchoolClassId = classId;
                        acada.StudentId = student.Id;
                        acada.YearTermId = school.CurrentYearTermId;

                        _unitofWork._AcademicRecordRepository.AddRecord(acada);
                        _unitofWork.Complete();


                    }
                }

                var cpos = _unitofWork._AcademicRecordRepository.CheckByClassYearTerm(classId);

                if (cpos)
                {

                }
                else
                {
                    _unitofWork._AcademicRecordRepository.AcademicRecordInsertIntoClassPosition(classId);
                    _unitofWork.Complete();
                }

            }//if it has not been graded

            var DataSource = _unitofWork._GradeRepository.GetAllGradesByClassYearTerm(classId).ToList();
            var scclass = _unitofWork._SchoolClassRepository.GetSchoolClassById(classId);
            foreach (var da in DataSource)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);
                var student = _unitofWork._StudentRepository.GetStudentById(da.StudentId);
              
                var course = _unitofWork._CourseRepository.GetCourseById(da.CourseId);
                var gSys = _unitofWork._GradeRepository.GetGradeSystemById(da.GradeSystemId);

                myResults.Add(new AcademicResultList(da.Id, student.FullName, da.ExamScore, da.TotalCAScore, course.Code, scclass.Code, gSys.Grade));
            }
            return Json(myResults, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClearResult(int classId)
        {
            _unitofWork._GradeRepository.ClearGradesForClass(classId);
            _unitofWork._GradeRepository.ClearAcademicRecordsForClass(classId);
            _unitofWork._GradeRepository.ClearClassPositionsForClass(classId);


            var DataSource = _unitofWork._GradeRepository.GetAllGradesByClassYearTerm(classId).ToList();
            var scclass = _unitofWork._SchoolClassRepository.GetSchoolClassById(classId);
            foreach (var da in DataSource)
            {
                var clsName = _unitofWork._SchoolClassRepository.GetSchoolClassById(da.SchoolClassId);
                var student = _unitofWork._StudentRepository.GetStudentById(da.StudentId);
               
                var course = _unitofWork._CourseRepository.GetCourseById(da.CourseId);
                var gSys = _unitofWork._GradeRepository.GetGradeSystemById(da.GradeSystemId);

                myResults.Add(new AcademicResultList(da.Id, student.FullName, da.ExamScore, da.TotalCAScore, course.Code, scclass.Code, gSys.Grade));
            }
            return Json(myResults, JsonRequestBehavior.AllowGet);
        }
    }
}