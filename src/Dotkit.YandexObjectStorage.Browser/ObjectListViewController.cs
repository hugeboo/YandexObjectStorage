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
 
        public ObjectListViewController(YClient yosClient, ListView listView)
        {
            _yClient = yosClient;
            _listView = listView;

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

        private void BucketTreeViewController_FolderSelectedChanged(object? sender, FolderSelectedChangedEventArgs e)
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

        private void BucketTreeViewController_BucketSelectedChanged(object? sender, BucketSelectedChangedEventArgs e)
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
            return new ListViewItem(folder.Name, "folder");
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
