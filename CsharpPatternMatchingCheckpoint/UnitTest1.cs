using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsharpPatternMatchingCheckpoint
{
    [TestClass]
    public class TestsBefore
    {

        [TestMethod]
        public void Test1()
        {
            //Arrange
            var passingStudent = new Student("Jane", "Doe", true, 'B');
            var failingStudent = new Student("Jane", "Doe", true, 'D');

            //Act
            bool passResult = getPassOrFail_ByPropertyPatternMatching(passingStudent);
            bool failResult = getPassOrFail_ByPropertyPatternMatching(failingStudent);

            //Assert
            Assert.AreEqual(true, passResult && !failResult);
        }

        [TestMethod]
        public void Test2()
        {
            //Arrange
            var passingStudent = new Student("Jane", "Doe", true, 'B');
            var failingStudent = new Student("Jane", "Doe", true, 'D');

            //Act
            bool passResult = getPassOrFail_ByTuplePatternMatching(passingStudent);
            bool failResult = getPassOrFail_ByTuplePatternMatching(failingStudent);

            //Assert
            Assert.AreEqual(true, passResult && !failResult);
        }

        [TestMethod]
        public void Test3()
        {
            //Arrange
            var passingStudent = new Student("Jane", "Doe", true, 'B');
            var failingStudent = new Student("Jane", "Doe", true, 'D');

            //Act
            bool passResult = getPassOrFail_ByPositionalPatternMatching(passingStudent);
            bool failResult = getPassOrFail_ByPositionalPatternMatching(failingStudent);

            //Assert
            Assert.AreEqual(true, passResult && !failResult);
        }

        public static bool getPassOrFail_ByPropertyPatternMatching(Student student)
        {
            // Use property pattern matching to determine if Grade is pass (A,B,C) or fail (D,F) and TuitionPaid is true.
            return (
                student is { Grade: 'A' } || student is { Grade: 'B' } || student is { Grade: 'C' }
                &&
                student is { TuitionPaid: true });
        }

        public static bool getPassOrFail_ByTuplePatternMatching(Student student)
        {
            var (grade, paid) = ( student.Grade, student.TuitionPaid );
            return (grade, paid) switch
            {
                ('A', true) => true,
                ('B', true) => true,
                ('C', true) => true,
                _ => false,
            };
            // Use tuple pattern matching to determine if Grade is pass (A,B,C) or fail (D,F) and TuitionPaid is true.
        }

        public static bool getPassOrFail_ByPositionalPatternMatching(Student student)
        {
            return (student is Student('A', true) || student is Student('B', true) || student is Student('C', true));
            // Use positional pattern matching to determine if Grade is pass (A,B,C) or fail (D,F) and TuitionPaid is true.
            // Note: You will to define the Deconstruct method for Student
        }
    }

    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool TuitionPaid { get; set; }
        public char Grade { get; set; }

        public Student(string firstName, string lastName, bool tuitionPaid, char grade) =>
            (FirstName, LastName, TuitionPaid, Grade) = (firstName, lastName, tuitionPaid, grade);

        public Student(char grade, bool paid) => (Grade, TuitionPaid) = (grade, paid);
        public void Deconstruct(out char grade, out bool paid) => (grade, paid) = (Grade, TuitionPaid);
    }
}