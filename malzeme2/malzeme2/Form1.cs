using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace malzeme2
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\LENOVO\Desktop\YNG\malzeme2\malzeme2\Database1.mdf;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }
        public static bool check(string str)
        {
            return (String.IsNullOrEmpty(str) ||
                str.Trim().Length == 0) ? true : false;
        }
        public DataTable LoadUserTable()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM malzeme";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EKLE
            if (check(textBox1.Text) == true || check(textBox2.Text) == true || check(textBox3.Text) == true || check(textBox4.Text) == true || check(textBox5.Text) == true || check(textBox6.Text) == true)
            {
                MessageBox.Show("Error!!");

            }
            else
            {
                con.Open();
                string query = "insert into malzeme (MalzemeKodu , MalzemeAdi , YillikSatisi , BirimFiyat , MinimumStok , TedarikSuresi ) VALUES (@MalzemeKodu , @MalzemeAdi , @YillikSatisi , @BirimFiyat , @MinimumStok , @TedarikSuresi ) ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MalzemeKodu", textBox1.Text);
                cmd.Parameters.AddWithValue("@MalzemeAdi", textBox6.Text);
                cmd.Parameters.AddWithValue("@YillikSatisi", textBox5.Text);
                cmd.Parameters.AddWithValue("@BirimFiyat", textBox4.Text);
                cmd.Parameters.AddWithValue("@MinimumStok", textBox3.Text);
                cmd.Parameters.AddWithValue("@TedarikSuresi", textBox2.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("saved");
                dataGridView1.DataSource = LoadUserTable();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Malzeme' table. You can move, or remove it, as needed.
            this.malzemeTableAdapter.Fill(this.database1DataSet.Malzeme);
            dataGridView1.DataSource = LoadUserTable();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count != 0)
                {
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox6.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

                }

            }

            catch (Exception)
            {
                throw;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (check(textBox1.Text) == true || check(textBox2.Text) == true || check(textBox3.Text) == true || check(textBox4.Text) == true || check(textBox5.Text) == true || check(textBox6.Text) == true)
            {
                MessageBox.Show("please enter the required data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con.Open();
                string query = "UPDATE malzeme SET MalzemeKodu=@MalzemeKodu ,MalzemeAdi=@MalzemeAdi , YillikSatisi=@YillikSatisi, BirimFiyat=@BirimFiyat , MinimumStok=@MinimumStok , TedarikSuresi=@TedarikSuresi WHERE id= '" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MalzemeKodu", textBox1.Text);
                cmd.Parameters.AddWithValue("@MalzemeAdi", textBox6.Text);
                cmd.Parameters.AddWithValue("@YillikSatisi", textBox5.Text);
                cmd.Parameters.AddWithValue("@BirimFiyat", textBox4.Text);
                cmd.Parameters.AddWithValue("@MinimumStok", textBox3.Text);
                cmd.Parameters.AddWithValue("@TedarikSuresi", textBox2.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("updated successfully");
                dataGridView1.DataSource = LoadUserTable();
                textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
            }
        }
    }
}
