using Dotkit.S3;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal sealed class ObjectListViewController
    {
        private readonly IS3Service _service;
        private readonly ListView _listView;

        private ToolStripMenuItem? _deleteToolStripMenuItem;
        private ToolStripMenuItem? _copyToolStripMenuItem;
        private ToolStripMenuItem? _pasteToolStripMenuItem;

        private S3DirectoryInfo? _currentFolder;

        public event EventHandler<FolderEventArgs>? FolderDoubleClick;
        public event EventHandler? CreateNewFolder;
        public event EventHandler<ItemsEventArgs>? DeleteItems;
        public event EventHandler? Refresh;

        public ObjectListViewController(IS3Service service, ListView listView)
        {
            _service = service;
            _listView = listView;
            listView.MouseDoubleClick += ListView_MouseDoubleClick;
            CreateContextMenu();
        }

        private void ListView_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            var ht = _listView.HitTest(e.Location);
            if (ht.Item != null)
            {
                if (ht.Item.Tag is S3DirectoryInfo fi)
                {
                    FolderDoubleClick?.Invoke(this, new FolderEventArgs(fi));
                }
            }
        }

        public void Attach(BucketTreeViewController bucketTreeViewController)
        {
            bucketTreeViewController.FolderSelectedChanged += BucketTreeViewController_FolderSelectedChanged;
            bucketTreeViewController.EmptySelectedChanged += BucketTreeViewController_EmptySelectedChanged;
        }

        private void BucketTreeViewController_EmptySelectedChanged(object? sender, EventArgs e)
        {
            _currentFolder = null;
            UpdateItems();
        }

        private void BucketTreeViewController_FolderSelectedChanged(object? sender, FolderEventArgs e)
        {
            _currentFolder = e.Folder;
            UpdateItems();
        }

        private void UpdateItems()
        {
            if (_currentFolder == null)
            {
                _listView.Items.Clear();
                return;
            }

            Utils.DoBackground(
                () =>
                {
                    var lstFiles = _currentFolder.GetItems().ConfigureAwait(false).GetAwaiter().GetResult();
                    return lstFiles;
                },
                (lstFiles) =>
                {
                    _listView.Items.Clear();
                    var items = new List<ListViewItem>();
                    items.AddRange(lstFiles.Select(CreateItem));
                    _listView.Items.AddRange(items.ToArray());
                },
                (ex) =>
                {
                    ShowMessageBox(ex);
                });
        }

        private ListViewItem CreateItem(IS3FileSystemInfo s3Item)
        {
            string imgKey = s3Item.Type == FileSystemType.Directory ? "folder" : EnsureFileImageKey(s3Item);
            var item = new ListViewItem(s3Item.Name, imgKey);
            item.Tag = s3Item;
            return item;
        }

        private string EnsureFileImageKey(IS3FileSystemInfo s3Item)
        {
            //var imageList = _listView.LargeImageList;
            //if (imageList.Images.ContainsKey(s3Item.Extension))
            //{
            //    return s3Item.Extension;
            //}
            //else
            //{
                
            //    var icon = Icon.ExtractAssociatedIcon(s3Item.Name);
            //    if (icon != null)
            //    {
            //        imageList.Images.Add(s3Item.Extension, icon);
            //        return s3Item.Extension;
            //    }
            //}
            return "file_default";
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

            _deleteToolStripMenuItem = new ToolStripMenuItem();
            _deleteToolStripMenuItem.Text = "Delete";
            _deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;

            _copyToolStripMenuItem = new ToolStripMenuItem();
            _copyToolStripMenuItem.Text = "Copy";
            _copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;

            _pasteToolStripMenuItem = new ToolStripMenuItem();
            _pasteToolStripMenuItem.Text = "Paste";
            _pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;

            var refreshToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;

            var treeContextMenu = new ContextMenuStrip();
            treeContextMenu.Items.AddRange(new ToolStripItem[]
            {
                createFolderToolStripMenuItem,
                new ToolStripSeparator(),
                _copyToolStripMenuItem,
                _pasteToolStripMenuItem,
                new ToolStripSeparator(),
                _deleteToolStripMenuItem,
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

        private void deleteToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            var items = new List<IS3FileSystemInfo>();
            foreach (ListViewItem item in _listView.SelectedItems)
            {
                if (item.Tag is IS3FileSystemInfo si)
                {
                    items.Add(si);
                }
            }
            DeleteItems?.Invoke(this, new ItemsEventArgs(items.ToArray()));
        }

        private void refreshToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Refresh?.Invoke(this, EventArgs.Empty);
        }

        private void treeContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _deleteToolStripMenuItem!.Enabled = _listView.SelectedItems.Count > 0;
            _copyToolStripMenuItem!.Enabled = false;
            _pasteToolStripMenuItem!.Enabled =
                _currentFolder != null &&
                Clipboard.GetFileDropList().Count > 0;
        }

        private void copyToolStripMenuItem_Click(object? sender, EventArgs e)
        {

        }

        private void pasteToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (_currentFolder == null) return;
            foreach(var filePath in Clipboard.GetFileDropList())
            {
                Utils.DoBackground(
                    () =>
                    {
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            _currentFolder.UploadFileAsync(filePath).ConfigureAwait(false).GetAwaiter().GetResult();
                        }
                    },
                    () =>
                    {
                        UpdateItems();
                    },
                    (ex) =>
                    {
                        ShowMessageBox(ex);
                    });
            }
        }
    }
}
