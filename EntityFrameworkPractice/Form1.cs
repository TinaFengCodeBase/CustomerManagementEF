using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using EntityFrameworkPractice.Entities;
using System.Data.Entity;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntityFrameworkPractice
{
    public partial class Form1 : Form
    {
        Detail MyDetail = new Detail();
        public Form1()
        {
            // Set the layout of the background image
            this.BackgroundImageLayout = ImageLayout.Stretch; //
            InitializeComponent();
            PopGridView();
            ClearFields();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            await PopGridView();
        }
        private async Task PopGridView()
        {
            using(var MyModelEntities = new MyModel())
            {
                dataGridView.DataSource = await MyModelEntities.Details.ToListAsync<Detail>();
                dataGridView.Enabled = true;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            using (var MyDbEntities = new MyModel()) 
            {
                if (MyDetail == null)
                {
                    Detail MyDetail = new Detail();
                    MyDetail.FirstName = txtFirstName.Text;
                    MyDetail.LastName = txtLastName.Text;
                    MyDetail.Age = Convert.ToInt32(txtAge.Text);
                    MyDetail.Address = txtAddress.Text;
                    MyDetail.DOB = Convert.ToDateTime(dateTimePickerDOB.Text);
                    MyDetail.ID = 0;
                    if (getAge() > 0)
                    {
                        MyDbEntities.Details.Add(MyDetail);
                        await MyDbEntities.SaveChangesAsync();
                        MessageBox.Show("Information has been saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The age has to be greater than 0.");
                    }
                }
                else
                {
                    MyDetail.FirstName = txtFirstName.Text;
                    MyDetail.LastName = txtLastName.Text;
                    MyDetail.Age = Convert.ToInt32(txtAge.Text);
                    MyDetail.Address = txtAddress.Text;
                    MyDetail.DOB = Convert.ToDateTime(dateTimePickerDOB.Text);
                    if (getAge() >= 0)
                    {
                        MyDbEntities.Entry(MyDetail).State = System.Data.Entity.EntityState.Modified;
                        await MyDbEntities.SaveChangesAsync();
                        btnSave.Text = "Save";
                        MessageBox.Show("Information has been updated", "Modified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The age has to be greater than 0.");
                    }
                }
            }
            await PopGridView();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            ClearFields();
            Detail MyDetail = new Detail();
            MyDetail.FirstName = "";
            MyDetail.LastName = "";
            MyDetail.Age = 0;
            MyDetail.Address = "";
            MyDetail.DOB = new DateTime();
            txtFirstName.Focus();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow.Index != -1)
            {
                MyDetail.ID = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);
                using (var MyDBEntities = new MyModel())
                {
                    MyDetail = MyDBEntities.Details.Where(x => x.ID == MyDetail.ID).FirstOrDefault();
                    txtFirstName.Text = MyDetail.FirstName;
                    txtLastName.Text = MyDetail.LastName;
                    txtAge.Text = MyDetail.Age.ToString();
                    txtAddress.Text = MyDetail.Address.ToString();
                    dateTimePickerDOB.Text = MyDetail.DOB.ToString();
                    MyDetail.ID = 0;
                }
            }

        }
        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.CurrentRow.Index != -1)
            {
                if (dataGridView.CurrentRow.Cells[0].Value != null)
                {
                    int id = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);
                  
                    using (var MyDBEntities = new MyModel())
                    {
                        MyDetail = MyDBEntities.Details.Where(x => x.ID == id).FirstOrDefault();
                        txtFirstName.Text = MyDetail.FirstName;
                        txtLastName.Text = MyDetail.LastName;
                        txtAge.Text = MyDetail.Age.ToString();
                        txtAddress.Text = MyDetail.Address.ToString();
                        dateTimePickerDOB.Text = MyDetail.DOB.ToString();
                    }
                }
            }
        }
        
        private async void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this information?", "Please confirm.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var MyDbEntities = new MyModel())
                {
                    var entry = MyDbEntities.Entry(MyDetail);
                    if (entry.State == EntityState.Detached)
                    {
                        MyDbEntities.Details.Attach(MyDetail);
                        MyDbEntities.Details.Remove(MyDetail);
                        await MyDbEntities.SaveChangesAsync();
                        await PopGridView();
                        ClearFields();
                    }
                }
            }
        }

        void ClearFields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtAge.Text = "";
            MyDetail = null;
            btnSave.Text = "Save";
            dateTimePickerDOB.Text = DateTime.Now.ToString();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearFields();
          
        }

        private int getAge()
        {
            int age = DateTime.Now.Year - dateTimePickerDOB.Value.Year;
            return age;
        }
        private void dateTimePickerDOB_ValueChanged(object sender, EventArgs e)
        {
            txtAge.Text = getAge().ToString();
        }
    }
}