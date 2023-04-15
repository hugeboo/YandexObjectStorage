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

        private ToolStripMenuItem? _deleteFolderToolStripMenuItem;

        public event EventHandler<FolderEventArgs>? FolderDoubleClick;
        public event EventHandler? CreateNewFolder;
        public event EventHandler<FoldersEventArgs>? DeleteFolders;
        public event EventHandler? Refresh;

        public ObjectListViewController(YClient yosClient, ListView listView)
        {
            _yClient = yosClient;
            _listView = listView;
            listView.MouseDoubleClick += ListView_MouseDoubleClick;
            CreateContextMenu();
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

        private static void ShowMessageBox(Exception? ex)
        {
            MessageBox.Show(ex?.Message ?? "Unknown exception", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CreateContextMenu()
        {
            var createFolderToolStripMenuItem = new ToolStripMenuItem();
            createFolderToolStripMenuItem.Text = "Create folder...";
            createFolderToolStripMenuItem.Click += createFolderToolStripMenuItem_Click;

            _deleteFolderToolStripMenuItem = new ToolStripMenuItem();
            _deleteFolderToolStripMenuItem.Text = "Delete folder";
            _deleteFolderToolStripMenuItem.Click += deleteFolderToolStripMenuItem_Click;

            var refreshToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;

            var treeContextMenu = new ContextMenuStrip();
            treeContextMenu.Items.AddRange(new ToolStripItem[]
            {
                createFolderToolStripMenuItem,
                _deleteFolderToolStripMenuItem,
                new ToolStripSeparator(),
                refreshToolStripMenuItem
            });
            treeContextMenu.Opening += treeContextMenu_Opening;

            _listView.ContextMenuStrip = treeContextMenu;
        }

        private void createFolderToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            CreateNewFolder?.Invoke(this, EventArgs.Empty);
        }

        private void deleteFolderToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            var folders = new List<YFolderInfo>();
            foreach (ListViewItem item in _listView.SelectedItems)
            {
                if (item.Tag is YFolderInfo fi)
                {
                    folders.Add(fi);
                }
            }
            DeleteFolders?.Invoke(this, new FoldersEventArgs(folders.ToArray()));
        }

        private void refreshToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Refresh?.Invoke(this, EventArgs.Empty);
        }

        private void treeContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _deleteFolderToolStripMenuItem!.Enabled = _listView.SelectedItems.Count > 0;
        }
    }
}
