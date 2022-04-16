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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cbp
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        List<Request> Start = BaseClass.bd.Request.ToList(); 
        List<Request> Filter;
        public ListPage()
        {
            InitializeComponent();
            Listview.ItemsSource = Start;
            Combofilter.Items.Add("Все");
            List<RequestType> rt = BaseClass.bd.RequestType.ToList();
            for(int i = 0; i < rt.Count; i++)
            {
                Combofilter.Items.Add(rt[i].RequestTitle);
            }
            Combofilter.SelectedIndex = 0;
            Combosort.SelectedIndex = 0;
        }
        private void FilterSort()
        {
            if (SearchTB.Text != String.Empty)
            {
                Filter = Start.Where(x => x.Description.Contains(SearchTB.Text)).ToList();
            }
            else
            {
                Filter = Start;
            }

            if(Combofilter.SelectedIndex != 0)
            {
                Filter = Filter.Where(x => x.RequestTypeID == Combofilter.SelectedIndex).ToList();
            }

            Listview.ItemsSource = Filter;
            if (Combosort.SelectedIndex == 0)
            {
                Filter.Sort((x, y) => x.ID.CompareTo(y.ID));
                Listview.Items.Refresh();
            }
            if (Combosort.SelectedIndex == 1)
            {
                Filter.Sort((x, y) => x.Urgency.CompareTo(y.Urgency));
                Filter.Reverse();
                Listview.Items.Refresh();
            }
            if (Combosort.SelectedIndex == 2)
            {
                Filter.Sort((x, y) => x.Urgency.CompareTo(y.Urgency));
                Listview.Items.Refresh();
            }
            if (Combosort.SelectedIndex == 3)
            {
                Filter.Sort((x, y) => x.ID.CompareTo(y.ID));
                Filter.Reverse();
                Listview.Items.Refresh();
            }
        }
        private void Combosort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterSort();
        }

        private void Combofilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterSort();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterSort();
        }

        private void AddBT_Click(object sender, RoutedEventArgs e)
        {
            ChangeWin win = new ChangeWin();
            win.ShowDialog();
            FrameClass.frame.Navigate(new ListPage());
        }

        private void ChangeBT_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int id = Convert.ToInt32(b.Uid);
            Request req = BaseClass.bd.Request.FirstOrDefault(x => x.ID == id);
            ChangeWin win = new ChangeWin(req);
            win.ShowDialog();
            FrameClass.frame.Navigate(new ListPage());
        }
    }
}
