namespace DataBaseMySQLMusicApp
{
    public partial class Form1 : Form
    {
        BindingSource albumsBindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();
            //ѕрив€зываем список к источнику
            albumsBindingSource.DataSource = albumsDAO.getAllAlbums();
            dataGridView1.DataSource = albumsBindingSource;
            pictureBox1.Load("https://upload.wikimedia.org/wikipedia/en/thumb/4/42/Beatles_-_Abbey_Road.jpg/220px-Beatles_-_Abbey_Road.jpg");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();
            //ѕрив€зываем список к источнику
            albumsBindingSource.DataSource = albumsDAO.searchTitles(textBox1.Text);
            dataGridView1.DataSource = albumsBindingSource;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            //получаем номер строки клика
            int rowClicked = dataGridView.CurrentRow.Index;
           // MessageBox.Show("номер" + rowClicked);
            string imageUrl= dataGridView.Rows[rowClicked].Cells[4].Value.ToString();
            //   MessageBox.Show("URL"+ imageUrl);
            pictureBox1.Load(imageUrl);
        }
    }
}
