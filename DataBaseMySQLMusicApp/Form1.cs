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
            //Привязываем список к источнику
            albumsBindingSource.DataSource = albumsDAO.getAllAlbums();
            dataGridView1.DataSource = albumsBindingSource;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlbumsDAO albumsDAO = new AlbumsDAO();
            //Привязываем список к источнику
            albumsBindingSource.DataSource = albumsDAO.searchTitles(textBox1.Text);
            dataGridView1.DataSource = albumsBindingSource;
        }
    }
}
