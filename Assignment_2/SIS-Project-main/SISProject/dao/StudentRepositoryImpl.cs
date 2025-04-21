using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using assignment_2.entity;
using assignment_2.util;

namespace assignment_2.dao
{
    public class StudentRepositoryImpl : IStudentRepository
    {
        private string connStr;

        public StudentRepositoryImpl()
        {
            connStr = DBPropertyUtil.GetConnectionString("SISDB");
        }

        public void AddStudent(Student student)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Students (first_name, last_name, date_of_birth, email, phone_number) " +
                               "VALUES (@FirstName, @LastName, @DOB, @Email, @Phone)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DOB", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Phone", student.PhoneNumber);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE Students SET first_name=@FirstName, last_name=@LastName, date_of_birth=@DOB, " +
                               "email=@Email, phone_number=@Phone WHERE student_id=@StudentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DOB", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Phone", student.PhoneNumber);
                cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Students WHERE student_id=@StudentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.ExecuteNonQuery();
            }
        }

        public Student GetStudentById(int studentId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Students WHERE student_id=@StudentId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Student(
                        Convert.ToInt32(reader["student_id"]),
                        reader["first_name"].ToString(),
                        reader["last_name"].ToString(),
                        Convert.ToDateTime(reader["date_of_birth"]),
                        reader["email"].ToString(),
                        reader["phone_number"].ToString()
                    );
                }
            }
            return null;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = DBConnUtil.GetConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    students.Add(new Student(
                        Convert.ToInt32(reader["student_id"]),
                        reader["first_name"].ToString(),
                        reader["last_name"].ToString(),
                        Convert.ToDateTime(reader["date_of_birth"]),
                        reader["email"].ToString(),
                        reader["phone_number"].ToString()
                    ));
                }
            }

            return students;
        }
    }
}
