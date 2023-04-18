using Dotkit.S3;

namespace Dotkit.YandexObjectStorage.Browser
{
    public partial class MainForm : Form
    {
        private readonly IS3Service _service;
        private readonly BucketTreeViewController _bucketTreeViewController;
        private readonly ObjectListViewController _objectListViewController;
        private readonly UIState _uiState;

        public MainForm()
        {
            InitializeComponent();

            _uiState = UIState.Load();

            //Program.S3Configuration.ServiceURL = "https://s3.yandexcloud.net";
            //Program.S3Configuration.AccessKeyId = "YCAJEIzcBfUuI2bK_G3l4k4br";
            //Program.S3Configuration.SecretAccessKey = "YCNOYDJLZkFf292p-BZMrHLxsnuWzE2JCWCXlA1N";
            //Program.S3Configuration.BucketName = "test1-sesv";

            _service = Program.S3Configuration.CreateService();
            _bucketTreeViewController = new BucketTreeViewController(_service, mainTreeView, this);
            _objectListViewController = new ObjectListViewController(_service, mainListView, this);
            _bucketTreeViewController.Attach(_objectListViewController);
            _objectListViewController.Attach(_bucketTreeViewController);

            _objectListViewController.SelectedChanged += objectListViewController_SelectedChanged;
        }

        private void objectListViewController_SelectedChanged(object? sender, ItemsEventArgs e)
        {
            if (e.Items.Length != 1)
            {
                toolStripStatusLabel1.Text = string.Empty;
                toolStripStatusLabel2.Text = string.Empty;
                toolStripStatusLabel3.Text = string.Empty;
            }
            else
            {
                toolStripStatusLabel3.Text = e.Items[0].LastModifiedTime.ToString(" HH:mm:ss dd.MM.yyyy");
                if (e.Items[0].Type == FileSystemType.Directory)
                {
                    toolStripStatusLabel1.Text = $"Directory: {e.Items[0].Name}";
                    toolStripStatusLabel2.Text = string.Empty;
                }
                else if (e.Items[0] is S3FileInfo fi)
                {
                    toolStripStatusLabel1.Text = $"File: {fi.Name}";
                    toolStripStatusLabel2.Text = FileSizeFormatter.FormatSize(fi.Length);
                }
            }
        }

        private void ApplyConfig()
        {
            this.Text = $"{this.Text} - {Program.S3Configuration.BucketName}";
            if (_uiState.MainFormWidth > 0 && _uiState.MainFormHeight > 0 && _uiState.MainFormX > 0 && _uiState.MainFormY > 0)
            {
                this.Bounds = new Rectangle(_uiState.MainFormX, _uiState.MainFormY, _uiState.MainFormWidth, _uiState.MainFormHeight);
            }
            if (_uiState.MainSlitterDistance > 0) mainSplitContainer.SplitterDistance = _uiState.MainSlitterDistance;
        }

        public void Lock()
        {
            mainSplitContainer.Enabled = false;
            fileToolStripMenuItem.Enabled = false;
            helpToolStripMenuItem.Enabled = false;
        }

        public void Unlock()
        {
            mainSplitContainer.Enabled = true;
            fileToolStripMenuItem.Enabled = true;
            helpToolStripMenuItem.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ApplyConfig();
            _bucketTreeViewController.Init();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new AboutBox();
            dlg.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _uiState.MainFormX = this.Left;
                _uiState.MainFormY = this.Top;
                _uiState.MainFormWidth = this.Width;
                _uiState.MainFormHeight = this.Height;
                _uiState.MainSlitterDistance = mainSplitContainer.SplitterDistance;

                _uiState.Save();
            }
            catch
            {
                //...
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var dlg = new SettingsForm();
            dlg.ShowDialog();
        }
    }
}