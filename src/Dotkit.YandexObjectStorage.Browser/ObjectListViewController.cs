using Dotkit.S3;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private readonly Form _mainForm;

        private ToolStripMenuItem? _deleteToolStripMenuItem;
        private ToolStripMenuItem? _copyToolStripMenuItem;
        private ToolStripMenuItem? _pasteToolStripMenuItem;
        private ToolStripMenuItem? _downloadToolStripMenuItem;

        private S3DirectoryInfo? _currentFolder;

        public event EventHandler<FolderEventArgs>? FolderDoubleClick;
        public event EventHandler? CreateNewFolder;
        public event EventHandler<ItemsEventArgs>? DeleteItems;
        public event EventHandler? Refresh;
        public event EventHandler<ItemsEventArgs>? SelectedChanged;

        public ObjectListViewController(IS3Service service, ListView listView, Form mainForm)
        {
            _service = service;
            _listView = listView;
            _mainForm = mainForm;
            listView.MouseDoubleClick += ListView_MouseDoubleClick;
            listView.ItemSelectionChanged += ListView_ItemSelectionChanged;
            CreateContextMenu();
        }

        private void ListView_ItemSelectionChanged(object? sender, ListViewItemSelectionChangedEventArgs e)
        {
            FireSelectedChanged();
        }

        private void FireSelectedChanged()
        {
            var items = GetSelectedItems();
            SelectedChanged?.Invoke(this, new ItemsEventArgs(items.ToArray()));
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
                FireSelectedChanged();
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
                    FireSelectedChanged();
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

            _downloadToolStripMenuItem = new ToolStripMenuItem();
            _downloadToolStripMenuItem.Text = "Download";
            _downloadToolStripMenuItem.Click += downloadToolStripMenuItem_Click;

            var refreshToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;

            var treeContextMenu = new ContextMenuStrip();
            treeContextMenu.Items.AddRange(new ToolStripItem[]
            {
                createFolderToolStripMenuItem,
                new ToolStripSeparator(),
                _downloadToolStripMenuItem,
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
            var items = GetSelectedItems();
            DeleteItems?.Invoke(this, new ItemsEventArgs(items.ToArray()));
        }

        private void refreshToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Refresh?.Invoke(this, EventArgs.Empty);
        }

        private void treeContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var selected = GetSelectedItems();
            _deleteToolStripMenuItem!.Enabled = _listView.SelectedItems.Count > 0;
            _copyToolStripMenuItem!.Enabled = selected.Any() && selected.All(it => it.Type == FileSystemType.File);
            _downloadToolStripMenuItem!.Enabled = _copyToolStripMenuItem!.Enabled;
            _pasteToolStripMenuItem!.Enabled =
                _currentFolder != null &&
                Clipboard.GetFileDropList().Count > 0;
        }

        private List<IS3FileSystemInfo> GetSelectedItems()
        {
            var items = new List<IS3FileSystemInfo>();
            foreach (ListViewItem item in _listView.SelectedItems)
            {
                if (item.Tag is IS3FileSystemInfo si)
                {
                    items.Add(si);
                }
            }
            return items;
        }

        private void downloadToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            DownloadSelectedFiles(null);
        }

        private void copyToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            DownloadSelectedFiles((files) =>
            {
                if (files.Any())
                {
                    var sc = new StringCollection();
                    sc.AddRange(files.ToArray());
                    Clipboard.SetFileDropList(sc);
                }
            });
        }

        private void DownloadSelectedFiles(Action<List<string>>? actionWithDownloadedFiles)
        {
            var selected = GetSelectedItems().Cast<S3FileInfo>().Where(it => it != null).ToList();
            if (!selected.Any()) return;

            var root = Path.Combine(Program.Config.LocalFileStorageRoot, Program.Config.S3Configuration.BucketName);
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);

            var progress = ShowProgressDialog("Downloading files...");
            try
            {
                Utils.DoBackground(
                    () =>
                    {
                        var lstFiles = new List<string>();
                        foreach (var fi in selected)
                        {
                            var dir = Path.Combine(root, fi.Directory.Key);
                            var localFilePath = Path.Combine(dir, fi.Name);
                            fi.DownloadAsync(localFilePath).ConfigureAwait(false).GetAwaiter().GetResult();
                            lstFiles.Add(localFilePath);
                        }
                        return lstFiles;
                    },
                    (lstFiles) =>
                    {
                        progress.Close();
                        actionWithDownloadedFiles?.Invoke(lstFiles);
                    },
                    (ex) =>
                    {
                        progress.Close();
                        ShowMessageBox(ex);
                    });
            }
            catch (Exception ex)
            {
                progress.Close();
                ShowMessageBox(ex);
            }
        }

        private ProgressForm ShowProgressDialog(string message)
        {
            var progress = new ProgressForm
            {
                Message = message
            };
            progress.Show();
            progress.Location = new Point(
                _mainForm.Left + (_mainForm.Width - progress.Width) / 2,
                _mainForm.Top + (_mainForm.Height - progress.Height) / 2);

            return progress;
        }

        private void pasteToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            var lstFiles = new List<string>();
            foreach(var filePath in Clipboard.GetFileDropList())
            {
                if (!string.IsNullOrEmpty(filePath)) lstFiles.Add(filePath);
            }
            UploadFiles(lstFiles);
        }

        private void UploadFiles(IEnumerable<string> filePaths)
        {
            if (_currentFolder == null) return;

            var progress = ShowProgressDialog("Uploading file...");
            try
            {
                Utils.DoBackground(
                   () =>
                   {
                       foreach (var filePath in filePaths)
                       {
                           _currentFolder.UploadFileAsync(filePath).ConfigureAwait(false).GetAwaiter().GetResult();
                       }
                   },
                   () =>
                   {
                       progress.Close();
                       UpdateItems();
                   },
                   (ex) =>
                   {
                       progress.Close();
                       ShowMessageBox(ex);
                       UpdateItems();
                   });
            }
            catch (Exception ex)
            {
                progress.Close();
                ShowMessageBox(ex);
                UpdateItems();
            }
        }
    }
}
