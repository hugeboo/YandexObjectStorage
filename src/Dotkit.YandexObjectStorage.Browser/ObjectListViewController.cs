using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal sealed class ObjectListViewController
    {
        private readonly YOSClient _yosClient;
        private readonly ListView _listView;
        private BucketTreeViewController? _bucketTreeViewController;

        public ObjectListViewController(YOSClient yosClient, ListView listView)
        {
            _yosClient = yosClient;
            _listView = listView;

        }

        public void Attach(BucketTreeViewController bucketTreeViewController)
        {
            _bucketTreeViewController = bucketTreeViewController;
            _bucketTreeViewController.ItemSelectedChanged += bucketTreeViewController_ItemSelectedChanged;
        }

        private ListViewItem CreateFolderItem(YOSFolder folder)
        {
            return new ListViewItem(folder.Name, "folder");
        }

        private void bucketTreeViewController_ItemSelectedChanged(object? sender, ItemSelectedChangedEventArgs e)
        {
            Utils.DoBackground(
                () =>
                {
                    if (e.IsBucket)
                    {
                        return _yosClient.GetFolders(e.Bucket!.Name).GetAwaiter().GetResult();
                    }
                    else if (e.IsFolder)
                    {
                        return _yosClient.GetFolders(e.Folder!).GetAwaiter().GetResult();
                    }
                    else
                    {
                        return new List<YOSFolder>();
                    }
                },
                (lstFolder) =>
                {
                    _listView.Items.Clear();
                    var items = lstFolder.Select(CreateFolderItem).ToArray();
                    _listView.Items.AddRange(items);
                },
                (ex) =>
                {
                    MessageBox.Show(ex?.Message ?? "Unknown exception", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
        }
    }
}
