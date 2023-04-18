using Dotkit.S3;

namespace Dotkit.YandexObjectStorage.Browser
{
    public partial class MainForm : Form
    {
        private readonly IS3Service _service;
        private readonly BucketTreeViewController _bucketTreeViewController;
        private readonly ObjectListViewController _objectListViewController;

        public MainForm()
        {
            InitializeComponent();

            Program.Config.S3Configuration = new S3Configuration
            {
                ServiceURL = "https://s3.yandexcloud.net",
                AccessKeyId = "YCAJEIzcBfUuI2bK_G3l4k4br",
                SecretAccessKey = "YCNOYDJLZkFf292p-BZMrHLxsnuWzE2JCWCXlA1N",
                BucketName = "test1-sesv"
            };

            _service = Program.Config.S3Configuration.CreateService();
            _bucketTreeViewController = new BucketTreeViewController(_service, mainTreeView);
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
            this.Text = $"{this.Text} - {Program.Config.S3Configuration.BucketName}";
            if (Program.Config.UIState.MainSlitterDistance > 0) mainSplitContainer.SplitterDistance = Program.Config.UIState.MainSlitterDistance;
            if (Program.Config.UIState.MainFormWidth > 0 && Program.Config.UIState.MainFormHeight > 0)
                this.Size = new Size(Program.Config.UIState.MainFormWidth, Program.Config.UIState.MainFormHeight);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ApplyConfig();
            _bucketTreeViewController.Init();
        }

        private void mainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Program.Config.UIState.MainSlitterDistance = mainSplitContainer.SplitterDistance;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Program.Config.UIState.MainFormWidth = this.Width;
            Program.Config.UIState.MainFormHeight = this.Height;
        }
    }
}