using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cbp
{
    /// <summary>
    /// Логика взаимодействия для ChangeWin.xaml
    /// </summary>
    public partial class ChangeWin : Window
    {
        bool Flag = false;
        public ChangeWin()
        {
            InitializeComponent();
            Comboboxcompany.ItemsSource = BaseClass.bd.Company.ToList();
            Comboboxcompany.SelectedValuePath = "ID";
            Comboboxcompany.DisplayMemberPath = "Title";

            Comboboxtype.ItemsSource = BaseClass.bd.RequestType.ToList();
            Comboboxtype.SelectedValuePath = "ID";
            Comboboxtype.DisplayMemberPath = "RequestTitle";

            ComboboxExecutor.ItemsSource = BaseClass.bd.Executor.ToList();
            ComboboxExecutor.SelectedValuePath = "ID";
            ComboboxExecutor.DisplayMemberPath = "Name";

            Flag = true;
        }
        Request newRequest = new Request();
        public ChangeWin(Request req)
        {
            InitializeComponent();
            newRequest = req;

            Comboboxcompany.ItemsSource = BaseClass.bd.Company.ToList();
            Comboboxcompany.SelectedValuePath = "ID";
            Comboboxcompany.DisplayMemberPath = "Title";

            Comboboxtype.ItemsSource = BaseClass.bd.RequestType.ToList();
            Comboboxtype.SelectedValuePath = "ID";
            Comboboxtype.DisplayMemberPath = "RequestTitle";

            ComboboxExecutor.ItemsSource = BaseClass.bd.Executor.ToList();
            ComboboxExecutor.SelectedValuePath = "ID";
            ComboboxExecutor.DisplayMemberPath = "Name";

            Comboboxcompany.SelectedIndex = newRequest.CompanyID - 1;
            Comboboxtype.SelectedIndex = newRequest.RequestTypeID - 1;
            ComboboxExecutor.SelectedIndex = newRequest.ExecutorID - 1;

            UrgencyTB.Text = newRequest.Urgency;
            ContactTB.Text = newRequest.Contact;
            DescriptionTB.Text = newRequest.Description;

        }

        private void AddBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newRequest.CompanyID = Comboboxcompany.SelectedIndex + 1;
                newRequest.RequestTypeID = Comboboxtype.SelectedIndex + 1;
                newRequest.ExecutorID = ComboboxExecutor.SelectedIndex + 1;

                newRequest.Urgency = UrgencyTB.Text;
                newRequest.Contact = ContactTB.Text;
                newRequest.Description = DescriptionTB.Text;

                if(Flag == true)
                {
                    BaseClass.bd.Request.Add(newRequest);
                }
                BaseClass.bd.SaveChanges();
                MessageBox.Show("Готово!");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void DelBT_Click(object sender, RoutedEventArgs e)
        {
            if(Flag == false)
            {
                BaseClass.bd.Request.Remove(newRequest);
                BaseClass.bd.SaveChanges();
                MessageBox.Show("Удалено!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Записи нет");
            }
        }
    }
}
