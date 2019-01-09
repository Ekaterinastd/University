using System;
using System.IO;
using System.Windows.Forms;

namespace University2
{
    abstract class Person
    {
        public string full_name;
        public int year_of_birth;

        public Person()     // конструктор без параметров класса Person (задаёт значения по умолчанию)
        {
            full_name = "Иванов Иван Иванович";
            year_of_birth = 1990;
        }

        public Person(string name, int year) // конструктор с параметрами класса Person
        {
            full_name = name;
            year_of_birth = year;
        }

        abstract public string Write();     // абстрактный метод без параметров
        abstract public void Read(string s);    // абстрактный метод со входным параметром
    }

    class Student:Person
    {
        public string faculty;
        public string specialty;
        public int year_of_study;
        public string form_of_study;        
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Student() : base()
        {
            faculty = "ИВТ";
            specialty = "с03-361-1";
            year_of_study = 2;
            form_of_study = "Очная";
        }

        /// <summary>
        /// Контруктор с параметрами
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="year">Год рождения</param>
        /// <param name="form">Форма обучения</param>
        /// <param name="fac">Факультет</param>
        /// <param name="spec">Специальность</param>
        /// <param name="stud_year">Год обучения</param>
        public Student(string name, int year, string form, string fac, string spec, int stud_year) : base(name, year)
        {
            form_of_study = form;
            faculty = fac;
            specialty = spec;
            year_of_study = stud_year;
        }

        /// <summary>
        /// Инициализация элементов строки файла
        /// </summary>
        /// <param name="s"></param>
        override public void Read(string s)
        {
            var array = s.Split();
            full_name = array[0] + " " + array[1] + " " + array[2];
            year_of_birth = Convert.ToInt32(array[3]);
            form_of_study = array[4];
            faculty = array[5];
            specialty = array[6];            
            year_of_study = Convert.ToInt32(array[7]);
        }
        /// <summary>
        /// Считывание списка студетнтов из файла в массив
        /// </summary>
        /// <returns></returns>
        public static Student[] ReadAll()
        {
            var str = File.ReadAllLines("students.txt");    // считывание всех строк из файла
            var students = new Student[str.Length];     // создание массива студентов (размер массива = кол-ву строк в файле)
            for(int i = 0; i < students.Length;i++)
            {
                students[i] = new Student();    //  вызов конструктора без параметров, создание студента
                students[i].Read(str[i]);   // считывание строки файла
            }
            return students;
        }
        /// <summary>
        /// Задание формата выходной строки
        /// </summary>
        /// <returns></returns>
        override public string Write()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", full_name, year_of_birth, form_of_study, faculty, specialty, year_of_study);
        }
        /// <summary>
        /// Запись массива студентов в файл
        /// </summary>
        /// <param name="students"></param>
        public static void WriteAll(Student[] students)
        {
            var line = new string[students.Length]; //создание одномерного массива типа string
            for (int i = 0; i < students.Length; i++)
            {
                line[i] = students[i].Write();  // форматирование строки
            }
            File.WriteAllLines("students.txt", line);   // запись мтрокового массива в файл
        }
    }

    class Teacher : Person
    {
        public int department_number;
        public int year_of_employment;
        public string academic_degree;
        public string status;
        public double rate;
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Teacher() : base()
        {
            department_number = 01;
            year_of_employment = 2010;
            academic_degree = "no";
            status = "no";
            rate = 0.5;
        }
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="yearOfBirth">Год рождения</param>
        /// <param name="depNumber">Номер кафедры</param>
        /// <param name="yearOfEmpl">Год принятия на работу</param>
        /// <param name="acDegr">Учёная степень</param>
        /// <param name="stat">Звание</param>
        /// <param name="r">Ставка</param>
        public Teacher(string name, int yearOfBirth, int depNumber, int yearOfEmpl, string acDegr, string stat, double r) : base(name, yearOfBirth)
        {
            department_number = depNumber;
            year_of_employment = yearOfEmpl;
            academic_degree = acDegr;
            status = stat;
            rate = r;
        }

        /// <summary>
        /// Инициализирует элементы строки файла.
        /// </summary>
        /// <param name="s">Строка файла.</param>
        override public void Read(string s)
        {
            var array = s.Split();
            full_name = array[0] + " " + array[1] + " " + array[2];
            year_of_birth = Convert.ToInt32(array[3]);
            department_number = Convert.ToInt32(array[4]);
            year_of_employment = Convert.ToInt32(array[5]);
            academic_degree = array[6];
            status = array[7];
            rate = Convert.ToDouble(array[8]);
        }
        /// <summary>
        /// Считывание списка преподавателей из файла в массив
        /// </summary>
        /// <returns></returns>
        public static Teacher[] ReadAll()
        {
            var teach = File.ReadAllLines("teachers.txt");  //считывание всего файла
            var teachers = new Teacher[teach.Length];
            for (int i = 0; i < teachers.Length; i++)
            {
                teachers[i] = new Teacher();    // вызов конструктора без параметров, создание преподавателя
                teachers[i].Read(teach[i]);
            }
            return teachers;
        }
        /// <summary>
        /// Задание формата строки выходного файла.
        /// </summary>
        /// <returns></returns>
        override public string Write()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6}", full_name, year_of_birth, department_number, year_of_employment,academic_degree, status, rate);
        }
        /// <summary>
        /// Запись массива преподавателей в файл.
        /// </summary>
        /// <param name="teachers"></param>
        public static void WriteAll(Teacher[] teachers)
        {
            var line = new string[teachers.Length];
            for (int i = 0; i < teachers.Length; i++)
            {
                line[i] = teachers[i].Write();
            }
            File.WriteAllLines("teachers.txt", line);
        }

    }
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
