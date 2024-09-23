namespace Course_Enrollment_System
{
    internal class Program
    {
        // Global data structures
        static Dictionary<string, HashSet<string>> courses = new Dictionary<string, HashSet<string>>();
        static Dictionary<string, int> courseCapacities = new Dictionary<string, int>();
        static Dictionary<string, Queue<string>> waitlists = new Dictionary<string, Queue<string>>();
        static void Main(string[] args)
        {
            bool running = true;
            while (running) {
                Console.Clear();
                Console.WriteLine("Wellcome To Cource Enrollment System");
                Console.WriteLine("1. Add a new course");
                Console.WriteLine("2. Remove Course");
                Console.WriteLine("3. Enroll a student in a course");
                Console.WriteLine("4. Display all students in a course");
                Console.WriteLine("5. Display all courses and their students");
                Console.WriteLine("6. Find courses with common students");
                Console.WriteLine("7. Withdraw a Student from All Courses");
                Console.WriteLine("0. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCoursePrompt();
                        break;
                    case "2":
                        RemoveCoursePrompt();
                        break;
                    case "3":
                        EnrollStudentPrompt();
                        break;
                    case "4":
                        DisplayStudentsInCoursePrompt();
                        break;
                    case "5":
                        DisplayAllCourses();
                        break;
                    case "6":
                        FindCommonStudentsPrompt();
                        break;
                    case "7":
                        WithdrawStudentPrompt();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Exiting the program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please enter a number between 0 and 7.");
                        break;

                }
                if (running)
                {
                    Console.WriteLine("Press Enter to return to the main menu.");
                    Console.ReadLine();
                }


                static void AddCourse(string courseCode, int capacity)
                {
                    if (!courses.ContainsKey(courseCode))
                    {
                        courses[courseCode] = new HashSet<string>();
                        courseCapacities[courseCode] = capacity;
                        waitlists[courseCode] = new Queue<string>();
                        Console.WriteLine($"Course {courseCode} added with capacity {capacity}.");
                    }
                    else
                    {
                        Console.WriteLine($"Course {courseCode} already exists.");
                    }
                }

                // Function to remove a course
                static void RemoveCourse(string courseCode)
                {
                    if (courses.ContainsKey(courseCode) && courses[courseCode].Count == 0)
                    {
                        courses.Remove(courseCode);
                        courseCapacities.Remove(courseCode);
                        waitlists.Remove(courseCode);
                        Console.WriteLine($"Course {courseCode} removed.");
                    }
                    else
                    {
                        Console.WriteLine($"Cannot remove course {courseCode}. It may have enrolled students.");
                    }
                }

                // Function to enroll a student in a course
                static void EnrollStudent(string studentName, string courseCode)
                {
                    if (!courses.ContainsKey(courseCode))
                    {
                        Console.WriteLine($"Course {courseCode} does not exist.");
                        return;
                    }

                    if (courses[courseCode].Contains(studentName))
                    {
                        Console.WriteLine($"{studentName} is already enrolled in {courseCode}.");
                        return;
                    }

                    if (courses[courseCode].Count < courseCapacities[courseCode])
                    {
                        courses[courseCode].Add(studentName);
                        Console.WriteLine($"{studentName} enrolled in {courseCode}.");
                    }
                    else
                    {
                        waitlists[courseCode].Enqueue(studentName);
                        Console.WriteLine($"{studentName} added to the waitlist for {courseCode}.");
                    }
                }

                // Function to remove a student from a course
                static void RemoveStudent(string studentName, string courseCode)
                {
                    if (courses.ContainsKey(courseCode) && courses[courseCode].Remove(studentName))
                    {
                        Console.WriteLine($"{studentName} removed from {courseCode}.");
                        // Check waitlist
                        if (waitlists[courseCode].Count > 0)
                        {
                            string nextStudent = waitlists[courseCode].Dequeue();
                            courses[courseCode].Add(nextStudent);
                            Console.WriteLine($"{nextStudent} enrolled in {courseCode} from the waitlist.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{studentName} is not enrolled in {courseCode}.");
                    }
                }

                // Function to display all students in a course
                static void DisplayStudentsInCourse(string courseCode)
                {
                    if (courses.ContainsKey(courseCode))
                    {
                        Console.WriteLine($"Students enrolled in {courseCode}: {string.Join(", ", courses[courseCode])}");
                    }
                    else
                    {
                        Console.WriteLine($"Course {courseCode} does not exist.");
                    }
                }

                // Function to display all courses and their students
                static void DisplayAllCourses()
                {
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"Course: {course.Key}, Students: {string.Join(", ", course.Value)}");
                    }
                }

                // Function to find common students in two courses
                static void FindCommonStudents(string courseCode1, string courseCode2)
                {
                    if (courses.ContainsKey(courseCode1) && courses.ContainsKey(courseCode2))
                    {
                        var commonStudents = new HashSet<string>(courses[courseCode1]);
                        commonStudents.IntersectWith(courses[courseCode2]);

                        Console.WriteLine($"Common students in {courseCode1} and {courseCode2}: {string.Join(", ", commonStudents)}");
                    }
                    else
                    {
                        Console.WriteLine("One or both courses do not exist.");
                    }
                }

                // Function to withdraw a student from all courses
                static void WithdrawStudent(string studentName)
                {
                    foreach (var courseCode in courses.Keys)
                    {
                        if (courses[courseCode].Remove(studentName))
                        {
                            Console.WriteLine($"{studentName} withdrawn from {courseCode}.");
                            // Check waitlist
                            if (waitlists[courseCode].Count > 0)
                            {
                                string nextStudent = waitlists[courseCode].Dequeue();
                                courses[courseCode].Add(nextStudent);
                                Console.WriteLine($"{nextStudent} enrolled in {courseCode} from the waitlist.");
                            }
                        }
                    }
                }

                // User input prompts
                static void AddCoursePrompt()
                {
                    Console.Write("Enter course code: ");
                    string courseCode = Console.ReadLine();
                    Console.Write("Enter course capacity: ");
                    int capacity = int.Parse(Console.ReadLine());
                    AddCourse(courseCode, capacity);
                }

                static void RemoveCoursePrompt()
                {
                    Console.Write("Enter course code to remove: ");
                    string courseCode = Console.ReadLine();
                    RemoveCourse(courseCode);
                }

                static void EnrollStudentPrompt()
                {
                    Console.Write("Enter student name: ");
                    string studentName = Console.ReadLine();
                    Console.Write("Enter course code to enroll in: ");
                    string courseCode = Console.ReadLine();
                    EnrollStudent(studentName, courseCode);
                }

                static void DisplayStudentsInCoursePrompt()
                {
                    Console.Write("Enter course code to display students: ");
                    string courseCode = Console.ReadLine();
                    DisplayStudentsInCourse(courseCode);
                }

                static void FindCommonStudentsPrompt()
                {
                    Console.Write("Enter first course code: ");
                    string courseCode1 = Console.ReadLine();
                    Console.Write("Enter second course code: ");
                    string courseCode2 = Console.ReadLine();
                    FindCommonStudents(courseCode1, courseCode2);
                }

                static void WithdrawStudentPrompt()
                {
                    Console.Write("Enter student name to withdraw: ");
                    string studentName = Console.ReadLine();
                    WithdrawStudent(studentName);
                }
            }
        }

    }
        
    
}
