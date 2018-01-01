using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmpsPortal.Core.Models;

namespace SmpsPortal.Core.Repositories
{
    public interface IAcademicRecordRepository
    {
        AcademicRecord GetAcademicRecordById(int id);
        ClassPosition GetClassPositionById(int id);
        IEnumerable<ClassPosition> GetByClassYearTerm(int schoolClassId);
        IEnumerable<ClassPosition> GetByStudentClassYearTerm(string studentId, int schoolClassId);
        IEnumerable<ClassPosition> GetByStudentYearTerm(string studentId);
        IEnumerable<ClassPosition> GetByRank(int schoolClassId);
        IEnumerable<AcademicRecord> GetAllAcademicRecords();
        IEnumerable<AcademicRecord> GetAcademicRecordByClassYearTerm(int schoolClassId);
        IEnumerable<AcademicRecord> GetAcademicRecordByStudentClassYearTerm(string studentId, int schoolClassId);
        IEnumerable<SchoolClass> GetClassesInRecord();
        IEnumerable<YearTerm> GetYearTermsInRecord();
        IEnumerable<Student> GetStudentsInRecordByClassYearTerm(int classId, int yearTermId);
        IEnumerable<ClassPosition> GetByStudentClassYearTerm2(string studentId, int schoolClassId, int yTermId);
        IEnumerable<SchoolClass> GetStudentClassesInRecord(string studentId);
        IEnumerable<YearTerm> GetStudentYearTermsInRecord(string studentId);
        IEnumerable<SchoolClass> GetParentClassesInRecord(List<string> studentId);
        IEnumerable<YearTerm> GetParentYearTermsInRecord(List<string> studentId);
        SchoolClass GetFirstParentClassesInRecord(string studentId);
        YearTerm GetFirstParentYearTermsInRecord(string studentId);

        bool CheckByClassYearTerm(int schoolClassId);
        void AcademicRecordInsertIntoClassPosition(int schoolClassId);
        void AddRecord(AcademicRecord record);
        void RemoveRecord(AcademicRecord record);
        void AddClassPosition(ClassPosition cp);
        void RemoveClassPosition(ClassPosition cp);
    }
}
