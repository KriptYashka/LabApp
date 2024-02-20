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
        private string mentor_surname;
        private double delta_time;

        public StudentCrossData(string _surname, string _group, string _mentor_surname, double _delta_time)
        {
            surname = _surname;
            group = _group;
            mentor_surname = _mentor_surname;
            delta_time = _delta_time;
        }

        public void show()
        {
            Console.WriteLine("Teacher {2}: {0} from {1} | {3}", surname, group, mentor_surname, delta_time);
        }

    }

    static void Main(string[] args)
    {
        Random rand = new Random(); 
        int N = 10;
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
            double deltaTime = (170.0 + rand.Next(50)) / 60;
            StudentCrossData student_data = new StudentCrossData(surnames[surnameIndex], groups[groupsIndex], surnames[surnameTeacherIndex], deltaTime);
            data[i] = student_data;
        }

        for (int i = 0; i < N; i++)
        {
            data[i].show();
        }
    }
}