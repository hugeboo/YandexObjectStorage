using Dotkit.YandexObjectStorage.FileSystem;
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
        private readonly YClient _yClient;
        private readonly ListView _listView;

        public event EventHandler<FolderEventArgs>? FolderDoubleClick;

        public ObjectListViewController(YClient yosClient, ListView listView)
        {
            _yClient = yosClient;
            _listView = listView;
            listView.MouseDoubleClick += ListView_MouseDoubleClick;
        }

        private void ListView_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            var ht = _listView.HitTest(e.Location);
            if (ht.Item != null)
            {
                if (ht.Item.Tag is YFolderInfo fi)
                {
                    FolderDoubleClick?.Invoke(this, new FolderEventArgs(fi));
                }
            }
        }

        public void Attach(BucketTreeViewController bucketTreeViewController)
        {
            bucketTreeViewController.BucketSelectedChanged += BucketTreeViewController_BucketSelectedChanged;
            bucketTreeViewController.FolderSelectedChanged += BucketTreeViewController_FolderSelectedChanged;
            bucketTreeViewController.EmptySelectedChanged += BucketTreeViewController_EmptySelectedChanged;
        }

        private void BucketTreeViewController_EmptySelectedChanged(object? sender, EventArgs e)
        {
            _listView.Items.Clear();
        }

        private void BucketTreeViewController_FolderSelectedChanged(object? sender, FolderEventArgs e)
        {
            Utils.DoBackground(
                () =>
                {
                    return YFolder.GetAllAsync(_yClient, e.Folder!.BucketName, e.Folder!.Name).GetAwaiter().GetResult();
                },
                (lstFolder) =>
                {
                    _listView.Items.Clear();
                    var items = lstFolder.Select(CreateFolderItem).ToArray();
                    _listView.Items.AddRange(items);
                },
                (ex) =>
                {
                    ShowMessageBox(ex);
                });
        }

        private void BucketTreeViewController_BucketSelectedChanged(object? sender, BucketEventArgs e)
        {
            Utils.DoBackground(
                () =>
                {
                    return YFolder.GetAllAsync(_yClient, e.Bucket!.Name).GetAwaiter().GetResult();
                },
                (lstFolder) =>
                {
                    _listView.Items.Clear();
                    var items = lstFolder.Select(CreateFolderItem).ToArray();
                    _listView.Items.AddRange(items);
                },
                (ex) =>
                {
                    ShowMessageBox(ex);
                });
        }

        private ListViewItem CreateFolderItem(YFolderInfo folder)
        {
            var item = new ListViewItem(folder.Name, "folder");
            item.Tag = folder;
            return item;
        }

        private void UpdateItems()
        {
        }

        private void ShowMessageBox(Exception? ex)
        {
            MessageBox.Show(ex?.Message ?? "Unknown exception", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
