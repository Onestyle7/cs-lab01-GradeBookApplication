using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double avarageGrade)
        {
            if(Students.Count < 5) 
            { 
                throw new InvalidOperationException("Ranked grading requires at least 5 students.");
            }
            int gradeGroupSize = Students.Count / 5;

            var sortedStudents = Students.OrderByDescending(s => s.AverageGrade).ToList();

            int index = sortedStudents.FindIndex(s => s.AverageGrade == avarageGrade);

            if (index >= 0 && index < gradeGroupSize)
                return 'A';
            else if (index < gradeGroupSize * 2)
                return 'B';
            else if (index < gradeGroupSize * 3)
                return 'C';
            else if (index < gradeGroupSize * 4)
                return 'D';
            else
                return 'F';
        }
        public override void CalculateStatistics()
        {
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
            }
            base.CalculateStatistics();
        }
        public override void CalculateStudentStatistics(string name)
        {
            if(Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.");
            }
            base.CalculateStudentStatistics(name);
        }
    }
    
}
