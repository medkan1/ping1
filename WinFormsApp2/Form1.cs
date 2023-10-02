using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;


namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        List<string> ipAddressList = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string ipAddress = textBox1.Text;
            string name = textBox2.Text;

            if (!string.IsNullOrEmpty(ipAddress))
            {
                // Add the IP address to the DataGridView as a new row
                dataGridView1.Rows.Add(name, ipAddress);
                ipAddressList.Add(ipAddress);

                // Clear the TextBox for the next entry
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid IP address.");
            }
        }

        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Remove the IP address from the list
                ipAddressList.Remove(selectedRow.Cells[1].Value.ToString());

                // Remove the selected row from the DataGridView
                dataGridView1.Rows.Remove(selectedRow);
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}