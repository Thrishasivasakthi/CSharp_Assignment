using System;
using assignment_2.entity;
using assignment_2.dao;
using System.Collections.Generic;

namespace assignment_2.main
{
    public class MainModule
    {
        public static void Run()
        {
            IStudentRepository studentRepo = new StudentRepositoryImpl();
            ICourseRepository courseRepo = new CourseRepositoryImpl();
            IEnrollmentRepository enrollmentRepo = new EnrollmentRepositoryImpl();
            ITeacherRepository teacherRepo = new TeacherRepositoryImpl();
            IPaymentRepository paymentRepo = new PaymentRepositoryImpl();

            while (true)
            {
                Console.WriteLine("===== STUDENT INFORMATION SYSTEM =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Add Course");
                Console.WriteLine("4. View All Courses");
                Console.WriteLine("5. Enroll Student");
                Console.WriteLine("6. Record Payment");
                Console.WriteLine("7. Add Teacher");
                Console.WriteLine("8. View Teachers");
                Console.WriteLine("9. Exit");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("First Name: ");
                    string fname = Console.ReadLine();
                    Console.Write("Last Name: ");
                    string lname = Console.ReadLine();
                    Console.Write("DOB (yyyy-mm-dd): ");
                    DateTime dob = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Phone: ");
                    string phone = Console.ReadLine();

                    Student s = new Student(0, fname, lname, dob, email, phone);
                    studentRepo.AddStudent(s);
                    Console.WriteLine("Student added.");
                }

                else if (choice == "2")
                {
                    List<Student> students = studentRepo.GetAllStudents();
                    foreach (var s in students)
                    {
                        s.DisplayStudentInfo();
                    }
                }

                else if (choice == "3")
                {
                    Console.Write("Course Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Course Code: ");
                    string code = Console.ReadLine();

                    Course c = new Course(0, name, code, "");
                    courseRepo.AddCourse(c);
                    Console.WriteLine("Course added.");
                }

                else if (choice == "4")
                {
                    List<Course> courses = courseRepo.GetAllCourses();
                    foreach (var c in courses)
                    {
                        c.DisplayCourseInfo();
                    }
                }

                else if (choice == "5")
                {
                    Console.Write("Student ID: ");
                    int sid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Course ID: ");
                    int cid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Date (yyyy-mm-dd): ");
                    DateTime date = Convert.ToDateTime(Console.ReadLine());

                    Student s = studentRepo.GetStudentById(sid);
                    Course c = courseRepo.GetCourseById(cid);
                    Enrollment e = new Enrollment(0, s, c, date);
                    enrollmentRepo.AddEnrollment(e);
                    Console.WriteLine("Enrollment created.");
                }

                else if (choice == "6")
                {
                    Console.Write("Student ID: ");
                    int sid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Amount: ");
                    decimal amount = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Date (yyyy-mm-dd): ");
                    DateTime date = Convert.ToDateTime(Console.ReadLine());

                    Student s = studentRepo.GetStudentById(sid);
                    Payment p = new Payment(0, s, amount, date);
                    paymentRepo.AddPayment(p);
                    Console.WriteLine("Payment recorded.");
                }

                else if (choice == "7")
                {
                    Console.Write("First Name: ");
                    string fname = Console.ReadLine();
                    Console.Write("Last Name: ");
                    string lname = Console.ReadLine();
                    Console.Write("Email: ");
                    string email = Console.ReadLine();

                    Teacher t = new Teacher(0, fname, lname, email);
                    teacherRepo.AddTeacher(t);
                    Console.WriteLine("Teacher added.");
                }

                else if (choice == "8")
                {
                    List<Teacher> teachers = teacherRepo.GetAllTeachers();
                    foreach (var t in teachers)
                    {
                        t.DisplayTeacherInfo();
                    }
                }

                else if (choice == "9")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid option. Try again.");
                }

                Console.WriteLine();
            }
        }
    }
}
