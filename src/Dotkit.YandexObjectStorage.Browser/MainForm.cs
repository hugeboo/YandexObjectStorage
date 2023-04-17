using Dotkit.S3;

namespace Dotkit.YandexObjectStorage.Browser
{
    public partial class MainForm : Form
    {
        private readonly S3Configuration _s3Config;
        private readonly IS3Service _service;
        private readonly BucketTreeViewController _bucketTreeViewController;
        private readonly ObjectListViewController _objectListViewController;

        public MainForm()
        {
            InitializeComponent();

            _s3Config = new S3Configuration
            {
                ServiceURL = "https://s3.yandexcloud.net",
                AccessKeyId = "YCAJEIzcBfUuI2bK_G3l4k4br",
                SecretAccessKey = "YCNOYDJLZkFf292p-BZMrHLxsnuWzE2JCWCXlA1N",
                BucketName = "test1-sesv"
            };

            _service = _s3Config.CreateService();
            _bucketTreeViewController = new BucketTreeViewController(_service, mainTreeView);
            _objectListViewController = new ObjectListViewController(_service, mainListView);
            _bucketTreeViewController.Attach(_objectListViewController);
            _objectListViewController.Attach(_bucketTreeViewController);

            //var client = new YOSClient(new YOSConfig());
            //var buckets = client.GetBuckets().Result;
            //var folders = client.GetFolders(buckets[0].Name!).Result;
            //var objects = client.GetObjects(buckets[0].Name!, "").Result;
            //var filePath = client.DownloadObject(buckets[0].Name!, objects[0].Key!, "c:\\temp").Result;
        }

        private void ApplyConfig()
        {
            this.Text = $"{this.Text} - {_s3Config}";
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