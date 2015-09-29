using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;

namespace WPFTraining
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Person person = new Person("Hyuk", 28);

        public MainWindow()
        {
            InitializeComponent();

            /*tbox_name.Text = person.Name;
            tbox_age.Text = person.Age.ToString();

            tbox_name.TextChanged += Tbox_TextChanged;
            tbox_age.TextChanged += Tbox_TextChanged;

            person.PropertyChanged += Person_PropertyChanged;*/

            //아래 Binding을 하면 위와 아래 주석처리된 동작과 동일.
            grid.DataContext = person;

            btn_birthday.Click += Btn_birthday_Click;
        }

        /*private void Tbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tbox = sender as TextBox;
            if (tbox == null)
                return;

            switch (tbox.Name)
            {
                case "tbox_name":
                    person.Name = tbox.Text;
                    break;

                case "tbox_age":
                    int age = 0;
                    if (int.TryParse(tbox.Text, out age))
                        person.Age = age;
                    break;
            }
        }*/

        private void Btn_birthday_Click(object sender, RoutedEventArgs e)
        {
            person.Age++;

            MessageBox.Show(person.Name + " / " + person.Age);
        }

        private void Person_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name":
                    tbox_name.Text = person.Name;
                    break;

                case "Age":
                    tbox_age.Text = person.Age.ToString();
                    break;
            }
        }
    }

    public class Person : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Notify("Name");
            }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                Notify("Age");
            }
        }


        public Person() { }
        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }



    public class NumberTextbox : TextBox
    {
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (((e.Source as TextBox).Text.Contains(".") && e.Text.Equals(".")) || (!e.Text.Equals(".") && !Char.IsDigit(e.Text.ToCharArray()[0])))
                e.Handled = true;
            else
                e.Handled = !AreAllValidNumericChars(e.Text);

            base.OnPreviewTextInput(e);
        }

        bool AreAllValidNumericChars(string str)
        {
            bool ret = true;

            if (str == System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.CurrencySymbol |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.NegativeSign |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PercentDecimalSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PercentGroupSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PercentSymbol |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PerMilleSymbol |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PositiveSign)
                return ret;

            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                ret &= Char.IsDigit(ch);
            }

            return ret;
        }
    }
}
