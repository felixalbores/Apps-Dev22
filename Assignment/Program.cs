//Team Alpha
using System;

namespace Assignment
{
    public struct Student
    {
        public int id;
        public string firstName;
        public string lastName;
        public int quizScore;
    }

    public enum remark
    {
        Poor = 1,
        Fair  = 2,
        Good = 3,
        Excellent = 4
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student student;

            Console.Write("Enter id: ");
            student.id = int.Parse(Console.ReadLine());

            Console.Write("Enter firstName: ");
            student.firstName = Console.ReadLine();

            Console.Write("Enter lastName: ");
            student.lastName = Console.ReadLine();

            Console.Write("Enter quizScore: ");
            student.quizScore = int.Parse(Console.ReadLine());


            if (student.quizScore == (int)remark.Poor)
                Console.WriteLine(remark.Poor);
            else if (student.quizScore == (int)remark.Fair)
                Console.WriteLine(remark.Fair);
            else if (student.quizScore == (int)remark.Good)
                Console.WriteLine(remark.Good);
            else
                Console.WriteLine(remark.Excellent);

        }
    }
}
