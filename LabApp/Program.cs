using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

class JumpData
{
    private string surname;
    private double[] results;
    private double level;
    public double total { get; set; }

    public JumpData(string _surname, double _level=2.5)
    {
        surname = _surname;
        level = _level;
        Random rand = new Random();
        double avg = rand.NextDouble() * 4 + 1; // 1..5
        results = new double[4];
        for (int i = 0; i < 4; i++)
        {
            results[i] = avg + (rand.NextDouble() * 3 - 1.5);
            results[i] = Math.Min(Math.Max(results[i], 0), 6); // Чтобы не перешёл за границы 0..6
        }
        total = this.getTotalResult();
    }

    public double getTotalResult()
    {
        double sum = results.Sum() - results.Max() - results.Min();
        sum *= level;
        return sum;
    }

    public void show()
    {
        Console.WriteLine("Sportsmen {0, 10}: {1, 2:f0}", surname, this.getTotalResult());
        Console.Write("-> Jumps: ");
        for (int i = 0; i < 4; i++)
        {
            Console.Write("{0, 2:f1} ", results[i]);
        }
        Console.WriteLine("\n");
    }
}

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

    static void Task1()
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

    static void Task2()
    {
        /*
         В соревнованиях по прыжкам в воду оценивают 7 судей. Каждый спортсмен выполняет 4 прыжка. 
        Каждый прыжок имеет одну из шести категорий сложности, оцениваемую коэффициентом (от 2,5 до 3,5). 
        Качество прыжка оценивается судьями по 6-балльной шкале. Далее лучшая и худшая оценки отбрасываются, остальные складываются, 
        и сумма умножается на коэффициент сложности. 
        Получить итоговую таблицу, содержащую фамилии спортсменов и итоговую оценку (сумму оценок по 4 прыжкам) в порядке занятых мест.
         */

        int N = 5;
        Random rand = new Random();
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
        JumpData[] results = new JumpData[N];
        for (int i = 0; i < N; i++)
        {
            int surnameIndex = rand.Next(surnames.Length);
            double level = 2.5 + rand.NextDouble();
            JumpData jumpData = new JumpData(surnames[surnameIndex], level);
            results[i] = jumpData;
        }

        var query = results.OrderBy(x => x.total).Reverse();

        foreach (JumpData data in query)
        {
            data.show();
        }

    }

    static void Main(string[] args)
    {
        Task2();
    }
}