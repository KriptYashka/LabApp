class Program
{
    /*
     Составить программу для обработки результатов кросса на 500 м для женщин. 
    Для каждой участницы ввести фамилию, группу, фамилию преподавателя, результат. 
    Получить результирующую таблицу, упорядоченную по результатам, в которой содержится также информация 
    о выполнении норматива. 
    Определить суммарное количество участниц, выполнивших норматив.
     */

    class StudentCrossData
    {
        private string surname;
        private string group;
        private string mentorSurname;
        public double deltaTime { get; set; }

        public StudentCrossData(string _surname, string _group, string _mentorSurname, double _deltaTime)
        {
            surname = _surname;
            group = _group;
            mentorSurname = _mentorSurname;
            deltaTime = _deltaTime;
        }

        public StudentCrossData(){}

        public void read()
        {
            Console.Write("Student's surname: ");
            surname = Console.ReadLine();
            Console.Write("Student's group: ");
            group = Console.ReadLine();
            Console.Write("Metnor's surname: ");
            mentorSurname = Console.ReadLine();
            Console.Write("Result in seconds: ");
            deltaTime = Convert.ToDouble(Console.ReadLine());
        }

        public void show()
        {
            Console.WriteLine("Teacher {2, -10}: {1, 3} group, {0, -10} | {3}m {4, 2:f0}s", surname, group, mentorSurname, Math.Floor(deltaTime / 60), 60 * (deltaTime / 60 - Math.Floor(deltaTime / 60)));
        }

    }

    static void Main(string[] args)
    {

        int N = 10;
        int require_second = 180; // 3 минуты => норматив сдан

        Random rand = new Random(); 
        StudentCrossData[] data = new StudentCrossData[N];
        string[] surnames =
        {
            "Ivanova",
            "Petrova",
            "Rudenko",
            "Klochay",
            "Vasnecova",
            "Kan",
            "Romanova",
            "Smolina",
            "Darmograi",
        };
        string[] groups =
        {
            "IU",
            "IBM",
            "SGN",
            "MT",
            "SM",
        };
        for (int i = 0; i < N; i++)
        {
            int surnameIndex = rand.Next(surnames.Length);
            int groupsIndex = rand.Next(groups.Length);
            int surnameTeacherIndex = rand.Next(surnames.Length);
            double deltaTime = (150 + rand.Next(50));
            StudentCrossData student_data = new StudentCrossData(surnames[surnameIndex], groups[groupsIndex], surnames[surnameTeacherIndex], deltaTime);
            data[i] = student_data;
        }

        var query = data.OrderBy(x => x.deltaTime);
        bool flag = true;
        int cnt = 0;
        Console.WriteLine("Passed standard\n------------");
        foreach (StudentCrossData curr_student in query)
        {
            if (flag && curr_student.deltaTime > require_second)
            {
                Console.WriteLine("\nNot passed standard\n------------");
                flag = false;
            }
            if (flag) cnt++;
            curr_student.show();
        }
        Console.WriteLine("\nTotal passed: {0}", cnt);
    }
}