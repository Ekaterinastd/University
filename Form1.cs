using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace University2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadStudent();
            LoadTeachers();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Загрузка списка студентов в dataGridView1.
        /// </summary>
        private void LoadStudent()
        {
            try
            {
                var stud = Student.ReadAll();   //считывание файла
                var stud2 = stud.ToList();  
                foreach (Student st in stud2)
                {
                    dataGridView1.Rows.Add(st.full_name, st.year_of_birth, st.form_of_study, st.faculty, st.specialty, st.year_of_study);   //добавление студента в таблицу
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Произошла ошибка при считывании входного файла " + ex);
            }
        }
        /// <summary>
        /// Проверка верности введёных данных для текстовых полей.
        /// </summary>
        /// <param name="str">Текстовое поле.</param>
        /// <returns></returns>
        private int CorrectEnter(string str)
        {
            string[] array = str.Split();   // получение из строки массива слов
            return array.Length;    // получение длинны массива
        }
        private void addStudent_Click(object sender, EventArgs e)
        {
            if (FullName.Text == "" || DateOfBirth.Text == "" || FormOfStudy.Text == "" || Faculity.Text == "" || Specialty.Text == "" || YearOfEducation.Text == "")   //проверка заполенности тексбоксов
               MessageBox.Show("Пожалуйста, заполните все поля, необходимые для добавления студента.");
            else
            {
                var fio = FullName.Text.Split();
                if (fio.Length != 3)    // проверка верной формы ФИО
                    MessageBox.Show("Поле \"ФИО\" должно содержать 3 слова, если отчество отсутствует, поставьте \"-\" или один пробел.");
                else
                {
                    if(CorrectEnter(FormOfStudy.Text)!=1 || CorrectEnter(Faculity.Text) != 1 || CorrectEnter(Specialty.Text) != 1)  //проверка верности заполнения текстовых полей Форма обучения, Факультет, Специальность
                        MessageBox.Show("Поля \"Форма обучения\",\"Факультет\",\"Специальность\" должны содержать одно слово, при необходимости используйте нижнее подчёркивание.");
                    else
                    {
                        bool isInt = int.TryParse(DateOfBirth.Text, out int res);
                        if (isInt == false) //проверка, являются ли данные поля целочисленными
                        {
                            MessageBox.Show("В полях \"год рождения\" и \"курс\" должны быть целочисленные значения.");
                        }
                        else
                        {
                            var students = Student.ReadAll();   // чтение списка студентов из файла
                            var st = new Student(FullName.Text, Convert.ToInt32(DateOfBirth.Text), FormOfStudy.Text, Faculity.Text, Specialty.Text, Convert.ToInt32(YearOfEducation.Text)); //вызов конструктора с параметрами и создание студента
                            var studList = students.ToList();
                            studList.Add(st);   // добавление студента в коллекцию studList
                            students = studList.ToArray();  // преобразование коллекции в массив
                            Student.WriteAll(students);     //запись массива студентов в файл
                            MessageBox.Show("Студент добавлен в список!");
                        }
                    }                    
                }
            }
        }

        static int index_row, index_col;
        string col_name;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string s = dataGridView1.CurrentCell.Value.ToString(); //получение значения выделенной ячейки
            textBox12.Text = s;     // перенос полученного значения в текстовое поле для внесения изменений
            index_row = dataGridView1.CurrentCell.RowIndex;     //получение индекса строки выделенной ячейки
            index_col = dataGridView1.CurrentCell.ColumnIndex;  //получение индекса столбца выделенной ячейки    
            col_name = dataGridView1.Columns[index_col].HeaderText;  //получение названия столбца          
            label17.Text = col_name;    //вывод названия изменяемого столбца            
        }
        /// <summary>
        /// Обновление dataGridView1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void update_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear(); // отчистка таблицы
            LoadStudent();      // загрузка списка студентов 
            textBox12.Text = "";
            label17.Text = "";
        }
        /// <summary>
        /// Считывание студентов из таблицы  dataGridView1 в массив.
        /// </summary>
        /// <returns>Массив студентов.</returns>
        private Student[] GetStudents()
        {
            List<Student> stud = new List<Student>();   // создание списка студентов
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                var name = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                var yearOfBirth = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                var formEd = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);
                var fac = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value);
                var spes = Convert.ToString(dataGridView1.Rows[i].Cells[4].Value);
                var yearOfSt = Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
                Student s2 = new Student(name, yearOfBirth, formEd, fac, spes, yearOfSt);   // вызов конструктора с параметрами, создание студента
                stud.Add(s2);   // добавление студента в список
            }
            var s3 = stud.ToArray();    // преобразование списка студентов в массив
            return s3;
        }
        /// <summary>
        /// Сохранение внесённых изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveChanges_Click(object sender, EventArgs e)
        {                      
            bool isInt = int.TryParse(textBox12.Text, out int res);
            if ((col_name == "Год рождения" || col_name == "Курс") && isInt == false)
                MessageBox.Show("В полях \"год рождения\" и \"курс\" должны быть целочисленные значения.");
            else
            {
                int t = CorrectEnter(textBox12.Text);
                if (col_name == "ФИО" && t != 1)
                    MessageBox.Show("Поле \"ФИО\" должно содержать 3 слова, если отчество отсутствует, поставьте \"-\" или один пробел.");
                else
                {
                    if ((col_name == "Форма обучения" || col_name == "Факультет" || col_name == "Специальность") && t != 1)
                        MessageBox.Show("Поля \"Форма обучения\",\"Факультет\",\"Специальность\" должны содержать одно слово, при необходимости используйте нижнее подчёркивание.");
                    else
                    {
                        dataGridView1[index_col, index_row].Value = textBox12.Text;     // присвоение выделенной ячейки таблицы нового значения 
                        var studentList = GetStudents();    // считывание студентов из таблицы в массив
                        Student.WriteAll(studentList);      // запись массива студентов в файл
                        MessageBox.Show("Изменения сохранены!");
                    }
                }
            }
            
        }

        /// <summary>
        /// Загрузка списка преподавателей в dataGridView2
        /// </summary>
        private void LoadTeachers()
        {
            try
            {
                var teach = Teacher.ReadAll();   // считывание файла
                var teach2 = teach.ToList();    // преобразование массива в список (коллекцию List)
                foreach (Teacher t in teach2)
                {
                    dataGridView2.Rows.Add(t.full_name, t.year_of_birth, t.department_number, t.year_of_employment, t.academic_degree, t.status, t.rate);   // вывод преподавателя в таблицу dataGridView2
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Произошла ошибка при считывании входного файла " + ex);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        static int index_row2, index_col2;
        string col_name2;        

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string s = dataGridView2.CurrentCell.Value.ToString(); //получение значения выделенной ячейки
            textBox1.Text = s;
            index_row2 = dataGridView2.CurrentCell.RowIndex;
            index_col2 = dataGridView2.CurrentCell.ColumnIndex;
            col_name2 = dataGridView2.Columns[index_col2].HeaderText;
            label20.Text = col_name2;
        }

        private void addTeacher_Click_1(object sender, EventArgs e)
        {
            if (FullName.Text == "" || DateOfBirth.Text == "" || DepNumb.Text == "" || YearOfDepl.Text == "" || AcadDegr.Text == "" || Status.Text == "" || Rate.Text == "")    //провека заполненности полей
            {
                MessageBox.Show("Пожалуйста, заполните все поля, необходимые для добавления преподавателя.");
            }
            else
            {
                var fio = FullName.Text.Split();
                if (fio.Length !=3) // проверка верной формы ввода ФИО
                {
                    MessageBox.Show("Поле \"ФИО\" должно содержать 3 слова, если отчество отсутствует, поставьте \"-\" или один пробел.");
                }
                else
                {
                    if (CorrectEnter(AcadDegr.Text) != 1 || CorrectEnter(Status.Text) != 1)
                        MessageBox.Show("Поля \"Ученая степень\",\"Звание\" должны содержать одно слово, при необходимости используйте нижнее подчёркивание.");
                    else
                    {
                        bool isInt2 = int.TryParse(textBox1.Text, out int res);
                        if ((col_name2 == "Год рождения" || col_name2 == "Номер кафедры" || col_name2 == "Год принятия на работу") && isInt2 == false)  //проверка верности заполения числовых полей
                            MessageBox.Show("В полях \"год рождения\" и \"Номер кафедры\" должны быть целочисленные значения.");
                        else
                        {
                            bool isDouble = Double.TryParse(textBox1.Text, out double res2);
                            if (col_name2 == "Ставка" && isDouble == false)     // проверка, является ли тип числа double
                                MessageBox.Show("В поле \"Ставка\" значение может быть десятичным или целым числом.");
                            else
                            {
                                var teachers = Teacher.ReadAll();   // считывание списка преподавателей в массив
                                var t = new Teacher(FullName.Text, Convert.ToInt32(DateOfBirth.Text), Convert.ToInt32(DepNumb.Text), Convert.ToInt32(YearOfDepl.Text), AcadDegr.Text, Status.Text, Convert.ToDouble(Rate.Text));   // вызов конствруктора с параметрами, создание преподавателя
                                var teachList = teachers.ToList();  
                                teachList.Add(t);   // добавления преподавателя в список
                                teachers = teachList.ToArray(); // преобразование списка в массив
                                Teacher.WriteAll(teachers); //запись массива преподавателей в файл
                                MessageBox.Show("Преподаватель добавлен в список!");
                            }
                        }
                    }
                }                
            }
        }

        private void update2_Click_1(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear(); // отчистка таблицы
            LoadTeachers();     // загрузка перподавателей в таблицу dataGridView2
            textBox1.Text = "";
            label20.Text = "";
        }

        private void saveChanges2_Click_1(object sender, EventArgs e)
        {
            
            bool isInt2 = int.TryParse(textBox1.Text, out int res);
            if ((col_name2 == "Год рождения" || col_name2 == "Номер кафедры" || col_name2 == "Год принятия на работу") && isInt2 == false)
                MessageBox.Show("В полях \"год рождения\" и \"Номер кафедры\" должны быть целочисленные значения.");
            else
            {
                bool isDouble = Double.TryParse(textBox1.Text, out double res2);
                if (col_name2 == "Ставка" && isDouble == false)
                    MessageBox.Show("В поле \"Ставка\" значение может быть десятичным или целым числом.");
                else
                {
                    int t2 = CorrectEnter(textBox1.Text);
                    if (col_name2 == "ФИО" && t2 != 1)
                        MessageBox.Show("Поле \"ФИО\" должно содержать 3 слова, если отчество отсутствует, поставьте \"-\" или один пробел.");
                    else
                    {
                        if ((col_name2 == "Звание" || col_name2 == "Учёная степень") && t2 != 1)
                            MessageBox.Show("Поля \"Звание\",\"Учёная степень\" должны содержать одно слово, при необходимости используйте нижнее подчёркивание.");
                        else
                        {
                            dataGridView2[index_col2, index_row2].Value = textBox1.Text;    // запись нового значения в выделенную ячейку
                            var teacherList = GetTeachers();    //  считывание списка преподавателе из таблицы dataGridView2 в массив
                            Teacher.WriteAll(teacherList);  // запись массива преподавателей в файл
                            MessageBox.Show("Изменения сохранены!");
                        }
                    }
                }
                
            }
            
        }
        /// <summary>
        /// Считывание списка преподавателей из таблицы dataGridView2 в массив.
        /// </summary>
        /// <returns></returns>
        private Teacher[] GetTeachers()
        {
            List<Teacher> teach = new List<Teacher>();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                var name = Convert.ToString(dataGridView2.Rows[i].Cells[0].Value);
                var yearOfB = Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);
                var depN = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value);
                var yearOfEmpl = Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value);
                var acDeg = Convert.ToString(dataGridView2.Rows[i].Cells[4].Value);
                var st = Convert.ToString(dataGridView2.Rows[i].Cells[5].Value);
                var r = Convert.ToDouble(dataGridView2.Rows[i].Cells[6].Value);

                Teacher t2 = new Teacher(name, yearOfB, depN, yearOfEmpl, acDeg, st, r);    // вызов конструктора с параметрами и создание преподавателя
                teach.Add(t2);  // добавление преподавателя в список
            }
            return teach.ToArray();     // преобразование списка в массив
        }
    }
}
