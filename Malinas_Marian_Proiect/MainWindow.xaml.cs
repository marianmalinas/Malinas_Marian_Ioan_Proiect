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
using AgentieModel;
using System.Data.Entity;
using System.Data;

namespace Malinas_Marian_Proiect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        Add,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        AgentieEntitiesModel ctx = new AgentieEntitiesModel();
        CollectionViewSource apartamenteVSource;
        CollectionViewSource angajatiVSource;
        CollectionViewSource clientiVSource;
        CollectionViewSource clientiInchirierisVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            apartamenteVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("apartamenteViewSource")));
            apartamenteVSource.Source = ctx.Apartamente.Local;
            ctx.Apartamente.Load();

            angajatiVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("angajatiViewSource")));
            angajatiVSource.Source = ctx.Angajati.Local;
            ctx.Angajati.Load();

            clientiVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiViewSource")));
            clientiVSource.Source = ctx.Clienti.Local;
            ctx.Clienti.Load();

            clientiInchirierisVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiInchirierisViewSource")));
            clientiInchirierisVSource.Source = ctx.Inchirieri.Local;
            ctx.Inchirieri.Load();

            cmbAngajati.ItemsSource = ctx.Angajati.Local;
            cmbAngajati.DisplayMemberPath = "NumePrenume";
            cmbAngajati.SelectedValuePath = "AngajatId";

            cmbApartamente.ItemsSource = ctx.Apartamente.Local;
            //cmbApartamente.DisplayMemberPath = "Adresa";
            //cmbApartamente.DisplayMemberPath = "Cartier";
            cmbApartamente.SelectedValuePath = "ApId";

            cmbClienti.ItemsSource = ctx.Clienti.Local;
            cmbClienti.SelectedValuePath = "ClientId";

            BindDataGrid();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            action = ActionState.Add;
            BindingOperations.ClearBinding(numePrenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);

            SetValidationBinding();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(numePrenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);

            SetValidationBinding();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrlagentie.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Angajati":
                    SaveAngajati();
                    break;
                case "Apartamente":
                    SaveApartamente();
                    break;
                case "Clienti":
                    SaveClienti();
                    break;
                case "Inchirieri":
                    SaveInchirieri();
                    break;
            }
            ReInitialize();
            MessageBox.Show("Modificarea s-a facut cu succes!");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            angajatiVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            angajatiVSource.View.MoveCurrentToNext();
        }

        private void btnPrev1_Click(object sender, RoutedEventArgs e)
        {
            apartamenteVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            apartamenteVSource.View.MoveCurrentToNext();
        }

        private void btnPrev2_Click(object sender, RoutedEventArgs e)
        {
            clientiVSource.View.MoveCurrentToPrevious();
        }

        private void btnNext2_Click(object sender, RoutedEventArgs e)
        {
            clientiVSource.View.MoveCurrentToNext();
        }

        private void SaveAngajati()
        {
            Angajati angajat = null;
            if (action == ActionState.Add)
            {
                try
                {
                    angajat = new Angajati()
                    {
                        NumePrenume = numePrenumeTextBox.Text.Trim()
                    };
                    ctx.Angajati.Add(angajat);
                    angajatiVSource.View.Refresh();
                    ctx.SaveChanges();
                }
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
                    angajat = (Angajati)angajatiDataGrid.SelectedItem;
                    angajat.NumePrenume = numePrenumeTextBox.Text.Trim();
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
                    angajat = (Angajati)angajatiDataGrid.SelectedItem;
                    ctx.Angajati.Remove(angajat);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                angajatiVSource.View.Refresh();
            }

        }

        private void SaveApartamente()
        {
            Apartamente apartament = null;
            if (action == ActionState.Add)
            {
                try
                {
                    apartament = new Apartamente()
                    {
                        Adresa = adresaTextBox.Text.Trim(),
                        Cartier = cartierTextBox.Text.Trim(),
                        Pret = int.Parse(pretTextBox.Text),
                        DataPublicare = dataPublicareTextBox.Text.Trim(),
                        Descriere = descriereTextBox.Text.Trim()

                    };
                    ctx.Apartamente.Add(apartament);
                    apartamenteVSource.View.Refresh();
                    ctx.SaveChanges();
                }
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
                    apartament = (Apartamente)apartamenteDataGrid.SelectedItem;
                    apartament.Adresa = adresaTextBox.Text.Trim();
                    apartament.Cartier = cartierTextBox.Text.Trim();
                    apartament.Pret = int.Parse(pretTextBox.Text);
                    apartament.DataPublicare = dataPublicareTextBox.Text.Trim();
                    apartament.Descriere = descriereTextBox.Text.Trim();
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
                    apartament = (Apartamente)apartamenteDataGrid.SelectedItem;
                    ctx.Apartamente.Remove(apartament);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                apartamenteVSource.View.Refresh();
            }
        }

        private void SaveClienti()
        {
            Clienti client = null;
            if (action == ActionState.Add)
            {
                try
                {
                    client = new Clienti()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim(),
                        Telefon = telefonTextBox.Text.Trim()
                    };
                    ctx.Clienti.Add(client);
                    clientiVSource.View.Refresh();
                    ctx.SaveChanges();
                }
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
                    client = (Clienti)clientiDataGrid.SelectedItem;
                    client.Nume = numeTextBox.Text.Trim();
                    client.Prenume = prenumeTextBox.Text.Trim();
                    client.Telefon = telefonTextBox.Text.Trim();
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
                    client = (Clienti)clientiDataGrid.SelectedItem;
                    ctx.Clienti.Remove(client);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                angajatiVSource.View.Refresh();
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

        private void SaveInchirieri()
        {
            Inchirieri inchiriere = null;
            if (action == ActionState.Add)
            {
                try
                {
                    Angajati angajat = (Angajati)cmbAngajati.SelectedItem;
                    Apartamente apartament = (Apartamente)cmbApartamente.SelectedItem;
                    Clienti client = (Clienti)cmbClienti.SelectedItem;
                    inchiriere = new Inchirieri()
                    {
                        AngajatId = angajat.AngajatId,
                        ApId = apartament.ApId,
                        ClientId = client.ClientId
                    };
                    ctx.Inchirieri.Add(inchiriere);
                    MessageBox.Show("ceva");
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
                dynamic selectedInchirieri = inchirieriDataGrid.SelectedItem;
                try
                {
                    int curr_id = selectedInchirieri.Id;
                    var editedInchiriere = ctx.Inchirieri.FirstOrDefault(s => s.Id == curr_id);
                    if (editedInchiriere != null)
                    {
                        editedInchiriere.AngajatId = Int32.Parse(cmbAngajati.SelectedValue.ToString());
                        editedInchiriere.ApId = Convert.ToInt32(cmbApartamente.SelectedValue.ToString());
                        editedInchiriere.ClientId = Convert.ToInt32(cmbClienti.SelectedValue.ToString());
                        ctx.SaveChanges();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                BindDataGrid();
                clientiInchirierisVSource.View.MoveCurrentTo(selectedInchirieri);
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    dynamic selectedInchirieri = inchirieriDataGrid.SelectedItem;
                    int curr_id = selectedInchirieri.Id;
                    var deletedInchirieri = ctx.Inchirieri.FirstOrDefault(s => s.Id == curr_id);
                    if (deletedInchirieri != null)
                    {
                        ctx.Inchirieri.Remove(deletedInchirieri);
                        ctx.SaveChanges();
                        MessageBox.Show("Inregistrarea a fost stearsa", "Message");
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
            var queryOrder = from inc in ctx.Inchirieri
                             join ang in ctx.Angajati on inc.AngajatId equals
                             ang.AngajatId
                             join ap in ctx.Apartamente on inc.ApId
                 equals ap.ApId
                            join cl in ctx.Clienti on inc.ClientId equals cl.ClientId
                             select new
                             {
                                 inc.Id,
                                 inc.ApId,
                                 inc.AngajatId,
                                 inc.ClientId,
                                 ang.NumePrenume,
                                 ap.Cartier,
                                 ap.Adresa,
                                 cl.Nume,
                                 cl.Prenume
                             };
            clientiInchirierisVSource.Source = queryOrder.ToList();
        }

        private void SetValidationBinding()
        {
            Binding numePrenumeValidationBinding = new Binding();
            numePrenumeValidationBinding.Source = angajatiVSource;
            numePrenumeValidationBinding.Path = new PropertyPath("NumePrenume");
            numePrenumeValidationBinding.NotifyOnValidationError = true;
            numePrenumeValidationBinding.Mode = BindingMode.TwoWay;
            numePrenumeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            numePrenumeValidationBinding.ValidationRules.Add(new StringNotEmpty());
            numePrenumeTextBox.SetBinding(TextBox.TextProperty, numePrenumeValidationBinding);

            Binding numeValidationBinding = new Binding();
            numeValidationBinding.Source = clientiVSource;
            numeValidationBinding.Path = new PropertyPath("Nume");
            numeValidationBinding.NotifyOnValidationError = true;
            numeValidationBinding.Mode = BindingMode.TwoWay;
            numeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            numeValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            numeTextBox.SetBinding(TextBox.TextProperty, numeValidationBinding);

            Binding prenumeValidationBinding = new Binding();
            prenumeValidationBinding.Source = clientiVSource;
            prenumeValidationBinding.Path = new PropertyPath("Prenume");
            prenumeValidationBinding.NotifyOnValidationError = true;
            prenumeValidationBinding.Mode = BindingMode.TwoWay;
            prenumeValidationBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            prenumeValidationBinding.ValidationRules.Add(new StringMinLengthValidator());
            prenumeTextBox.SetBinding(TextBox.TextProperty, prenumeValidationBinding);
        }
    }
}
