using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Reload();
        }
        void Reload()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                var query = "SELECT * FROM Category";
                dataGridView1.DataSource = db.Query<Category>(query).ToList();
                var query1 = "SELECT * FROM Product";
                dataGridView2.DataSource = db.Query<Product>(query1).ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                CategoryForm cf = new CategoryForm();
                var query = "SELECT * FROM Category";
                cf.dataGridView1.DataSource = db.Query<Category>(query).ToList();
                DialogResult result = cf.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                string name = cf.textBox1.Text;
                var query1 = "INSERT INTO Category(name) VALUES(@name)";
                db.Execute(query1, new {name});
                cf.dataGridView1.Refresh();
                MessageBox.Show("New category has been added.");
            }
            Reload();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                CategoryForm cf = new CategoryForm();
                var query = "SELECT * FROM Category";
                cf.dataGridView1.DataSource = db.Query<Category>(query).ToList();
                DialogResult result = cf.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                if (cf.dataGridView1.SelectedRows.Count != 1) return;
                int index = cf.dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(cf.dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                string name = cf.textBox1.Text;
                var query1 = "UPDATE Category SET name=@name WHERE id = @id";
                db.Execute(query1, new {name, id});
                cf.dataGridView1.Refresh();
                MessageBox.Show("Info has been edited.");
            }
            Reload();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                Delete dl = new Delete();
                var query = "SELECT * FROM Category";
                dl.dataGridView1.DataSource = db.Query<Category>(query).ToList();
                DialogResult result = dl.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                if (dl.dataGridView1.SelectedRows.Count < 1) return;
                int index = dl.dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dl.dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                var query1 = "delete from Category where id=@id";
                db.Execute(query1, new { id });
                dl.dataGridView1.Refresh();
                MessageBox.Show("The category has been deleted.");
            }
            Reload();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                ProductForm pf = new ProductForm();
                var query = "SELECT * FROM Product";
                pf.dataGridView1.DataSource = db.Query<Product>(query).ToList();
                var query1 = "SELECT * FROM Category";
                pf.comboBox1.DataSource = db.Query<Category>(query1).ToList();
                pf.comboBox1.ValueMember = "id";
                pf.comboBox1.DisplayMember = "name";
                DialogResult result = pf.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                string name = pf.textBox1.Text;
                Category category = pf.comboBox1.SelectedItem as Category;
                int idCategory = category.Id;
                var query2 = "INSERT INTO Product(name, idCategory) VALUES(@name, @idCategory)";
                db.Execute(query2, new { name, idCategory});
                pf.dataGridView1.Refresh();
                MessageBox.Show("New product has been added.");
            }
            Reload();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                ProductForm pf = new ProductForm();
                var query = "SELECT * FROM Product";
                pf.dataGridView1.DataSource = db.Query<Product>(query).ToList();
                var query1 = "SELECT * FROM Category";
                pf.comboBox1.DataSource = db.Query<Category>(query1).ToList();
                pf.comboBox1.ValueMember = "id";
                pf.comboBox1.DisplayMember = "name";
                DialogResult result = pf.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                if (pf.dataGridView1.SelectedRows.Count != 1) return;
                int index = pf.dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(pf.dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                string name = pf.textBox1.Text;
                Category category = pf.comboBox1.SelectedItem as Category;
                int idCategory = category.Id;
                var query2 = "UPDATE Product SET name=@name, idCategory=@idCategory WHERE id = @id";
                db.Execute(query2, new { name, idCategory, id});
                pf.dataGridView1.Refresh();
                MessageBox.Show("Info has been edited.");
            }
            Reload();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString))
            {
                Delete dl = new Delete();
                var query = "SELECT * FROM Product";
                dl.dataGridView1.DataSource = db.Query<Product>(query).ToList();
                DialogResult result = dl.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                if (dl.dataGridView1.SelectedRows.Count < 1) return;
                int index = dl.dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dl.dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                var query1 = "delete from Product where id=@id";
                db.Execute(query1, new {id});
                dl.dataGridView1.Refresh();
                MessageBox.Show("The product has been deleted.");
            }
            Reload();
        }
    }
}
