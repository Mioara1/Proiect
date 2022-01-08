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
using System.Data.Entity;
using SalaFitnessModel;
using System.Data;

namespace Proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        SalaFitnessEntitateModel ctx = new SalaFitnessEntitateModel();
        CollectionViewSource clientVSource;
        CollectionViewSource abonamentVSource;
        CollectionViewSource clientAchizitiesVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clientVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
            clientVSource.Source = ctx.Client.Local;
            ctx.Client.Load();

            abonamentVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("abonamentViewSource")));
            abonamentVSource.Source = ctx.Abonament.Local;
            ctx.Abonament.Load();

            clientAchizitiesVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientAchizitiesViewSource")));

            clientAchizitiesVSource.Source = ctx.Achizitie.Local;
            ctx.Achizitie.Load();
            ctx.Abonament.Load();
            cmbClient.ItemsSource = ctx.Client.Local;
            //cmbClient.DisplayMemberPath = "Nume";
            cmbClient.SelectedValuePath = "ClientId";
            cmbAbonament.ItemsSource = ctx.Abonament.Local;
            //cmbAbonament.DisplayMemberPath = "TipAbonament";
            cmbAbonament.SelectedValuePath = "AbonamentId";


            //clientAchizitieVSource.Source = ctx.Achizitie.Local;
            ctx.Achizitie.Load();
            ctx.Abonament.Load();
             BindDataGrid();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
           
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            clientVSource.View.MoveCurrentToNext();
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            clientVSource.View.MoveCurrentToPrevious();
        }
        private void SaveClient()
        {
            Client client = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    client = new Client()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Client.Add(client);
                    clientVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    client = (Client)clientDataGrid.SelectedItem;
                    client.Nume = numeTextBox.Text.Trim();
                    client.Prenume = prenumeTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    client = (Client)clientDataGrid.SelectedItem;
                    ctx.Client.Remove(client);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clientVSource.View.Refresh();
            }

        }
        private void btnPrev1_Click(object sender, RoutedEventArgs e)
        {
            abonamentVSource.View.MoveCurrentToPrevious();

        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            abonamentVSource.View.MoveCurrentToNext();
        }
        private void SaveAbonament()
        {
            Abonament abonament = null;
            if (action == ActionState.New)
            {
                try
                {

                    abonament = new Abonament()
                    {
                        TipAbonament = tipAbonamentTextBox.Text.Trim(),
                        Pret = int.Parse(pretTextBox.Text)
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Abonament.Add(abonament);
                    abonamentVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    abonament = (Abonament)abonamentDataGrid.SelectedItem;
                    abonament.TipAbonament = tipAbonamentTextBox.Text.Trim();
                    abonament.Pret = int.Parse(pretTextBox.Text);
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    abonament = (Abonament)abonamentDataGrid.SelectedItem;
                    ctx.Abonament.Remove(abonament);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                abonamentVSource.View.Refresh();
            }

        }

        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrlSalaFitness.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Client":
                    SaveClient();
                    break;
                case "Abonament":
                    SaveAbonament();
                    break;
                case "Achizitie":
                    break;
            }
            ReInitialize();
        }
        private void SaveAchizitie()
        {
            Achizitie achizitie = null;
            if (action == ActionState.New)
            {
                try
                {
                    Client client = (Client)cmbClient.SelectedItem;
                    Abonament abonament = (Abonament)cmbAbonament.SelectedItem;
                    //instantiem Order entity
                    achizitie = new Achizitie()
                    {

                        ClientId = client.ClientId,
                        AbonamentId = abonament.AbonamentId
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Achizitie.Add(achizitie);
                    //salvam modificarile
                    ctx.SaveChanges();
                    BindDataGrid();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                if (action == ActionState.Edit)
            {
                dynamic selectedAchizitie = achizitieDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedAchizitie.AchizitieId;
                    var editedAchizitie = ctx.Achizitie.FirstOrDefault(s => s.AchizitieId == curr_id);
                    if (editedAchizitie != null)
                    {
                        editedAchizitie.ClientId = Int32.Parse(cmbClient.SelectedValue.ToString());
                        editedAchizitie.AbonamentId = Convert.ToInt32(cmbAbonament.SelectedValue.ToString());
                        //salvam modificarile
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                // pozitionarea pe item-ul curent
                clientAchizitiesVSource.View.MoveCurrentTo(selectedAchizitie);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedAchizitie = achizitieDataGrid.SelectedItem;
                    int curr_id = selectedAchizitie.AchizitieId;
                    var deletedAchizitie = ctx.Achizitie.FirstOrDefault(s => s.AchizitieId == curr_id);
                    if (deletedAchizitie != null)
                    {
                        ctx.Achizitie.Remove(deletedAchizitie);
                        ctx.SaveChanges();
                        MessageBox.Show("Achizitie stearsa cu succes", "Message");
                        BindDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void BindDataGrid()
        {
            var queryAchizitie = from achizitie in ctx.Achizitie
                                 join client in ctx.Client on achizitie.ClientId equals
                                 client.ClientId
                                 join abonament in ctx.Abonament on achizitie.AbonamentId
                     equals abonament.AbonamentId
                                 select new
                                 {
                                     achizitie.AchizitieId,
                                     achizitie.AbonamentId,
                                     achizitie.ClientId,
                                     client.Nume,
                                     client.Prenume,
                                     abonament.TipAbonament,
                                     abonament.Pret
                                 };
            clientAchizitiesVSource.Source = queryAchizitie.ToList();
        }
    }
}