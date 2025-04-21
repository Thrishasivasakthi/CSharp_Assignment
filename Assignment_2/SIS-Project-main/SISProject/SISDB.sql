-- STEP 1: Drop existing tables if they exist (in correct order due to FK constraints)
USE SISDB;
GO

IF OBJECT_ID('Enrollments', 'U') IS NOT NULL DROP TABLE Enrollments;
IF OBJECT_ID('Payments', 'U') IS NOT NULL DROP TABLE Payments;
IF OBJECT_ID('Courses', 'U') IS NOT NULL DROP TABLE Courses;
IF OBJECT_ID('Teacher', 'U') IS NOT NULL DROP TABLE Teacher;
IF OBJECT_ID('Students', 'U') IS NOT NULL DROP TABLE Students;

-- STEP 2: Create Students table
CREATE TABLE Students (
    student_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    date_of_birth DATE,
    email VARCHAR(100),
    phone_number VARCHAR(20)
);

-- STEP 3: Create Teacher table
CREATE TABLE Teacher (
    teacher_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    email VARCHAR(100)
);


-- Drop the Courses table fully
DROP TABLE IF EXISTS Courses;
GO

-- Recreate Courses with teacher_id
CREATE TABLE Courses (
    course_id INT IDENTITY(1,1) PRIMARY KEY,
    course_name VARCHAR(100),
    course_code VARCHAR(20),
    teacher_id INT FOREIGN KEY REFERENCES Teacher(teacher_id)
);
GO

-- Confirm it's now present
SELECT * FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Courses';




-- STEP 5: Create Enrollments table
CREATE TABLE Enrollments (
    enrollment_id INT IDENTITY(1,1) PRIMARY KEY,
    student_id INT FOREIGN KEY REFERENCES Students(student_id),
    course_id INT FOREIGN KEY REFERENCES Courses(course_id),
    enrollment_date DATE
);

-- STEP 6: Create Payments table
CREATE TABLE Payments (
    payment_id INT IDENTITY(1,1) PRIMARY KEY,
    student_id INT FOREIGN KEY REFERENCES Students(student_id),
    amount DECIMAL(10,2),
    payment_date DATE
);

-- STEP 7: Insert Students
INSERT INTO Students (first_name, last_name, date_of_birth, email, phone_number) VALUES
('Alice', 'Walker', '2000-03-11', 'alice@example.com', '7777777777'),
('Bob', 'Johnson', '2003-06-22', 'bob@example.com', '6666666666'),
('Charlie', 'Lee', '2001-12-01', 'charlie@example.com', '5555555555'),
('Diana', 'Prince', '2002-01-15', 'diana@example.com', '4444444444'),
('Edward', 'Stone', '2003-04-10', 'edward@example.com', '3333333333'),
('Fiona', 'Granger', '2002-11-23', 'fiona@example.com', '2222222222'),
('George', 'Martin', '2001-09-19', 'george@example.com', '1111111111'),
('Hannah', 'Brown', '2000-07-30', 'hannah@example.com', '0000000000'),
('Jane', 'Johnson', '2002-08-21', 'janej@example.com', '8888888888'),
('John', 'Doe', '2001-05-12', 'john@example.com', '9999999999');

-- STEP 8: Insert Teachers
INSERT INTO Teacher (first_name, last_name, email) VALUES
('Emily', 'Clark', 'emily@uni.com'),
('Daniel', 'Lewis', 'daniel@uni.com'),
('Sarah', 'Smith', 'sarah.smith@example.com'),
('Michael', 'Brown', 'michael@uni.com'),
('Laura', 'Adams', 'laura@uni.com'),
('James', 'Green', 'james@uni.com'),
('Nina', 'Scott', 'nina@uni.com'),
('Tom', 'White', 'tom@uni.com');

INSERT INTO Courses (course_name, course_code, teacher_id) VALUES
('Computer Science 101', 'CS101', 1),
('Advanced Database Management', 'CS302', 2),
('Chemistry 101', 'CHEM101', 3),
('English Literature', 'ENG101', 4),
('History of Art', 'ART101', 5),
('Introduction to Programming', 'IT101', 6),
('Data Structures', 'CS201', 7),
('Operating Systems', 'CS301', 8);


-- STEP 10: Insert Enrollments
INSERT INTO Enrollments (student_id, course_id, enrollment_date) VALUES
(1, 1, '2023-12-27'), (1, 5, '2023-12-13'),
(2, 3, '2023-12-01'), (2, 2, '2023-01-25'),
(3, 4, '2023-03-27'), (3, 5, '2023-02-09'),
(4, 5, '2023-04-27'), (4, 7, '2023-04-19'),
(5, 8, '2023-09-08'), (5, 3, '2023-11-05'),
(6, 7, '2023-04-22'), (6, 3, '2023-05-07'),
(7, 8, '2023-07-04'), (7, 7, '2023-01-06'),
(8, 4, '2023-11-06'), (8, 8, '2023-02-02'),
(9, 6, '2023-09-09'), (9, 3, '2023-07-17'),
(10, 5, '2023-10-01'), (10, 4, '2023-10-02');

-- STEP 11: Insert Payments
INSERT INTO Payments (student_id, amount, payment_date) VALUES
(1, 589.99, '2023-03-26'),
(2, 616.99, '2023-05-17'),
(3, 323.99, '2023-07-03'),
(4, 649.99, '2023-10-20'),
(5, 423.99, '2023-01-11'),
(6, 439.99, '2023-06-26'), (6, 616.99, '2023-04-07'),
(7, 451.99, '2023-06-18'), (7, 589.99, '2023-02-10'),
(8, 643.99, '2023-10-23'), (8, 337.99, '2023-02-11'),
(9, 349.99, '2023-09-02'), (9, 518.99, '2023-12-03'),
(10, 583.99, '2023-10-01'), (10, 335.99, '2023-02-11');

-- STEP 13: Task 9 – Assign Sarah Smith to teach CS302 (already inserted above)
UPDATE Courses
SET teacher_id = (SELECT teacher_id FROM Teacher WHERE email = 'sarah.smith@example.com')
WHERE course_code = 'CS302';

-- STEP 14: Task 10 – Jane Johnson makes a payment (student_id = 9)
INSERT INTO Payments (student_id, amount, payment_date)
VALUES (9, 500.00, '2023-04-10');

-- STEP 15: Task 11 – Enrollment Report for 'Computer Science 101'
SELECT s.student_id, s.first_name, s.last_name, c.course_name, e.enrollment_date
FROM Students s
JOIN Enrollments e ON s.student_id = e.student_id
JOIN Courses c ON e.course_id = c.course_id
WHERE c.course_name = 'Computer Science 101';
