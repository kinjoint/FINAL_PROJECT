namespace FINAL_KINJO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            if (username == "" || password == "" )
            {
                MessageBox.Show("Please enter username and password.");
                return;
            } 
            DBConnect db = new DBConnect();

            try
            {
                db.Open();
                string query = "SELECT COUNT(*) FROM student_account WHERE username=@username AND password=@password";

                MySql.Data.MySqlClient.MySqlCommand cmd =
                 new MySql.Data.MySqlClient.MySqlCommand(query, db.Connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();

                if (count == 1)
                {
                    MessageBox.Show("login Successful");
                    new frmDashboard().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                db.Close();
            }

        }
    }
}
