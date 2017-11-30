using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using WPFSQLiteExample.Models;

namespace WPFSQLiteExample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private string DBPath = "../../Assets/db.sqlite";

        private SQLiteConnection Connection;

        public MainWindow()
        {
            InitializeComponent();

            Connection = new SQLiteConnection(DBPath);
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            NameTable name = new NameTable
            {
                Name = InsertBox.Text
            };

            Connection.Insert(name);

            Debug.WriteLine("Success Data Insert");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var query = Connection.Table<NameTable>();

            foreach (var data in query)
            {
                if (data.Name == UpdateBeforeBox.Text)
                {
                    Connection.RunInTransaction(() =>
                    {
                        Connection.Delete(data);
                    });
                }
            }

            Debug.WriteLine("Success Data Delete");
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var query = Connection.Table<NameTable>();

            foreach (var data in query)
            {
                if (data.Name == UpdateBeforeBox.Text)
                {
                    data.Name = UpdateAfterBox.Text;

                    Connection.RunInTransaction(() =>
                    {
                        Connection.Update(data);
                    });
                }
            }

            Debug.WriteLine("Success Data Update");
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            var query = Connection.Table<NameTable>();

            ObservableCollection<NameTable> observableCollection = new ObservableCollection<NameTable>();

            foreach (var data in query)
            {
                observableCollection.Add(new NameTable { NameId = data.NameId, Name = data.Name });
            }

            SelectedList.ItemsSource = observableCollection;

            Debug.WriteLine("Success Data Select");
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Connection.CreateTable<NameTable>();

            Debug.WriteLine("Success Create Table");
        }

        private void DropButton_Click(object sender, RoutedEventArgs e)
        {
            Connection.DropTable<NameTable>();

            Debug.WriteLine("Success Drop Table");
        }
    }
}
