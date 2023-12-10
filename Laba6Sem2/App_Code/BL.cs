using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Laba6Sem2.App_Code
{
    public class BL
    {
        public ObservableCollection<Pacient> Allpacients { get; set; }
        public ObservableCollection<Pacient> Govactina { get; set; } // Список на вакцинацию
        public ObservableCollection<Pacient> Sertificate { get; set; } // Список с сертифицированными
    public Pacient selectedItem;
        public Pacient SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }
        public BL() 
        {
            Allpacients = new ObservableCollection<Pacient>()
            {
                new Pacient("Дарбинян Давид Арманович",Vacitation.One),
                new Pacient("Красноборов Евгений Евгеньевич",Vacitation.Zero),
                new Pacient("Бельтюков Александр Николаевич",Vacitation.Zero),
                new Pacient("Рыбин Максим Андреевич",Vacitation.Zero),
                new Pacient("Савченко Александр Алексеевич",Vacitation.Zero),
                new Pacient("Павленко Артур",Vacitation.Zero),
                new Pacient("Сурин Никита Константинович",Vacitation.Zero)
            };
            Govactina = new ObservableCollection<Pacient>();
            Sertificate = new ObservableCollection<Pacient>();
        }
        public RelayCommand AllToVactina => new RelayCommand(execute => AllToVactination()); // Всех на вакцинацию
        public RelayCommand SelectedToVactina => new RelayCommand(execute => SelectedVactina()); // Выбранного на вакцинацию
        public RelayCommand AllToFirst => new RelayCommand(execute => _Alltofirst());
        public RelayCommand AllToSecond => new RelayCommand(execute => _Alltosecond());
        public RelayCommand DoPrivivka => new RelayCommand(execute => _DoPrivivka());
        public void AllToVactination()
        {
            foreach (var item in Allpacients)
            {
                Govactina.Add(item);
            }
            Allpacients.Clear();
        }
        public void SelectedVactina()
        {
            if (selectedItem != null)
            {
                Govactina.Add(selectedItem);
                Allpacients.Remove(selectedItem);
            }
            else
            {
                MessageBox.Show("Ничего не выбрано!", "Пустой выбор");
            }
        }
        public void _Alltofirst()
        {
            if(Allpacients.Count > 0) 
            {
                foreach (var item in Allpacients)
                {
                    if (item.Vacitation == Vacitation.Zero)
                    {
                        Govactina.Add(item);
                    }
                }
                List<Pacient> BUFER = Allpacients.Where(x=>x.Vacitation == Vacitation.Zero).ToList();
                foreach(var item in BUFER)
                {
                    Allpacients.Remove(item);
                }
            }
            else
            {
                MessageBox.Show("Пациентов не осталось");
            }   
        }
        public void _Alltosecond()
        {
            if (Allpacients.Where(x => x.Vacitation == Vacitation.One).ToList().Count>0)
            {
                foreach (var item in Allpacients)
                {
                    if (item.Vacitation == Vacitation.One)
                    {
                        Govactina.Add(item);
                    }
                }
                List<Pacient> BUFER = Allpacients.Where(x => x.Vacitation == Vacitation.One).ToList();
                foreach (var item in BUFER)
                {
                    Allpacients.Remove(item);
                }
            }
            else
            {
                MessageBox.Show("Пациентов с первичной вакцинацией не осталось");
            }
        }
        Random random = new Random();
        Random strah = new Random();
        public void _DoPrivivka()
        {
            List<Pacient> BUFER = new List<Pacient>(); // Нужен для удаления сбежавших и прошедших первичную вакцинацию пациентов
            List<Pacient> GETSERTIFICATE = new List<Pacient>(); // Нужен для удаления пациентов получивших сертификат
            List<Pacient> UBEZHALI = new List<Pacient>(); // Список убежавших :)
            bool Ubezhal = false;

            if (Govactina.Count != 0)
            {
                int vactina = random.Next(0, 2);
                if (vactina == 1) // Вакцина присутствует
                {
                    foreach (Pacient pacient in Govactina)
                    {
                        int strashilka = strah.Next(0, 2);
                        if (strashilka == 0)
                        {
                            if (pacient.Vacitation == Vacitation.Zero)
                            {
                                pacient.Vacitation = pacient.Vacitation + 1;
                                Allpacients.Add(pacient);
                                BUFER.Add(pacient);
                            }
                            else
                            {
                                pacient.Vacitation = pacient.Vacitation + 1;
                                Sertificate.Add(pacient);
                                GETSERTIFICATE.Add(pacient);
                            }
                        }
                        else
                        {
                            Ubezhal = true;
                            Allpacients.Add(pacient);
                            BUFER.Add(pacient);
                            UBEZHALI.Add(pacient);
                        }
                    }
                    foreach (Pacient aboba in BUFER) // Удаление сбежавших и прошедших первичную вакцинацию пациентов
                    {
                        Govactina.Remove(aboba);
                    }
                    foreach (Pacient aboba1 in GETSERTIFICATE) // Удаление пациентов получивших сертификат
                    {
                        Govactina.Remove(aboba1);
                    }
                    string ubezhavhie = "";
                    foreach (Pacient aboba2 in UBEZHALI) // Данная функция бесполезна и сделана для прикола, можно удалить
                    {
                        ubezhavhie += aboba2.FullName + Environment.NewLine;
                    }


                    if (Ubezhal) // Проверка убежал ли кто то??
                    {
                        MessageBox.Show("ОДИН/ИЛИ НЕСКОЛЬКО ПАЦИЕНТОВ ИСПУГАЛИСЬ И УБЕЖАЛИ В СТРАХЕ!!! \nСписок трусов:\n" + ubezhavhie + "\n\nА в целом всё успешно )", "СТРАШНО СТРАШНО ОЧЕНЬ СТРАШНО");
                    }
                    else
                    {
                        MessageBox.Show("Вакцинация прошла успешно", "Успех");
                    }
                }
                else
                {
                    MessageBox.Show("Вакцин нет, их украл Шрек.");
                    foreach(Pacient item in Govactina)
                    {
                        Allpacients.Add(item);
                    }
                    Govactina.Clear();
                }

            }
            else
            {
                MessageBox.Show("Список пациентов на вакцинацию пуст", "Пусто");
            }
        }
    }
}