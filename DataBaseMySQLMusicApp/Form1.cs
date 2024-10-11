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
            Album a1 = new Album()
            {
                ID = 1,
                AlbumName = "My first song",
                ArtistName = "BAba ZINA",
                Year = 1965,
                ImageUrl = "fqwqw",
                Description = "Test",
            };
            Album a2 = new Album()
            {
                ID = 1,
                AlbumName = "My second song",
                ArtistName = "BAba ZINA",
                Year = 1965,
                ImageUrl = "fqwqw",
                Description = "Test",
            };
            albumsDAO.albums.Add(a1);
            albumsDAO.albums.Add(a2);

            //Привязываем список к источнику
            albumsBindingSource.DataSource = albumsDAO.albums;
            dataGridView1.DataSource = albumsBindingSource;
        }
    }
}
