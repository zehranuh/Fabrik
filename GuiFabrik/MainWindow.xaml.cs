using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fabrik;

namespace GuiFabrik
{
    public partial class MainWindow : Window
    {
        private MachineController _machineController;

        public MainWindow()
        {
            InitializeComponent();
            Machine machine = new Machine();
            JobManager jobManager = new JobManager();
            _machineController = new MachineController(machine, jobManager);
            _machineController.JobStatusChanged += UpdateJobStatus;
            _machineController.JobCompleted += UpdateJobStatus;
            _machineController.MachineFailed += ShowErrorMessage;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _machineController.StartMachine();
            UpdateStatus();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _machineController.StopMachine();
            UpdateStatus();
        }

        private void AddJobButton_Click(object sender, RoutedEventArgs e)
        {
            string jobName = JobNameTextBox.Text;
            int quantity;
            if (!int.TryParse(QuantityTextBox.Text, out quantity))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Stückzahl ein.");
                return;
            }

            string product = "";
            if (JobTypeComboBox.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)JobTypeComboBox.SelectedItem;
                switch (selectedItem.Content.ToString().Split(' ')[0])
                {
                    case "1":
                        product = "Auto";
                        break;
                    case "2":
                        product = "Kabel";
                        break;
                    case "3":
                        product = "Metallstück";
                        break;
                    default:
                        MessageBox.Show("Bitte wählen Sie einen gültigen Jobtyp.");
                        return;
                }
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie einen Jobtyp.");
                return;
            }

            Job job = new Job(jobName, product, quantity);
            _machineController.AddJob(job);
            MessageBox.Show($"Job {jobName} hinzugefügt: {quantity} {product}");
        }

        private void StartJobsButton_Click(object sender, RoutedEventArgs e)
        {
            _machineController.StartJobs();
            UpdateStatus();
        }

        private void GetJobStatusButton_Click(object sender, RoutedEventArgs e)
        {
            _machineController.GetJobStatus();
        }

        private void FixErrorButton_Click(object sender, RoutedEventArgs e)
        {
            string errorCode = ErrorCodeTextBox.Text;
            _machineController.FixMachineError(errorCode);
            MessageBox.Show("Fehler behoben! Starte noch mal deinen Job.", "Fehler behoben", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            StatusLabel.Content = _machineController.GetMachineStatus();
            SignalLightLabel.Content = _machineController.GetSignalLightState().ToString();
        }

        private void UpdateJobStatus(string status)
        {
            Dispatcher.Invoke(() => JobStatusLabel.Content = status);
        }

        private void ShowErrorMessage(string message)
        {
            Dispatcher.Invoke(() => MessageBox.Show(message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error));
        }
    } 
}