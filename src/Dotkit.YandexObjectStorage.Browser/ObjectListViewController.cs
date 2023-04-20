using Dotkit.S3;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal sealed class ObjectListViewController
    {
        private readonly IS3Service _service;
        private readonly ListView _listView;
        private readonly MainForm _mainForm;
        private readonly TextBox _folderTextBox;

        private ToolStripMenuItem? _deleteToolStripMenuItem;
        private ToolStripMenuItem? _copyToolStripMenuItem;
        private ToolStripMenuItem? _pasteToolStripMenuItem;
        private ToolStripMenuItem? _downloadToolStripMenuItem;

        private S3DirectoryInfo? _currentFolder;
        private ProgressForm? _progressForm;

        public event EventHandler<FolderEventArgs>? FolderDoubleClick;
        public event EventHandler? CreateNewFolder;
        public event EventHandler<ItemsEventArgs>? DeleteItems;
        public event EventHandler? Refresh;
        public event EventHandler<ItemsEventArgs>? SelectedChanged;

        public ObjectListViewController(IS3Service service, ListView listView, MainForm mainForm, TextBox folderTextBox)
        {
            _service = service;
            _listView = listView;
            _mainForm = mainForm;
            _folderTextBox = folderTextBox;
            listView.MouseDoubleClick += ListView_MouseDoubleClick;
            listView.ItemSelectionChanged += ListView_ItemSelectionChanged;
            listView.KeyDown += ListView_KeyDown;
            folderTextBox.Resize += FolderTextBox_Resize;
            CreateContextMenu();
        }

        private void FolderTextBox_Resize(object? sender, EventArgs e)
        {
            if (_currentFolder != null)
            {
                _folderTextBox.Text = EllipsisFolderPath(_currentFolder.FullName);
            }
        }

        private string EllipsisFolderPath(string path)
        {
            string result = new string(path);
            // TODO: Use Utils.EllipsisString
            TextRenderer.MeasureText(result, _folderTextBox.Font, _folderTextBox.Size, 
                TextFormatFlags.ModifyString | TextFormatFlags.PathEllipsis);
            var index0 = result.IndexOf('\0');
            if (index0 >= 0) result = result.Substring(0, index0);
            return result;
        }

        private void ListView_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                deleteToolStripMenuItem_Click(sender, e);
            }
            else if (e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    copyToolStripMenuItem_Click(sender, e);
                }
                else if (e.KeyCode == Keys.V)
                {
                    pasteToolStripMenuItem_Click(sender, e);
                }
                else if (e.KeyCode == Keys.A)
                {
                    foreach(ListViewItem item in _listView.Items)
                    {
                        item.Selected = true;
                    }
                }
            }
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
            _folderTextBox.Text = null;
            UpdateItems();
        }

        private void BucketTreeViewController_FolderSelectedChanged(object? sender, FolderEventArgs e)
        {
            _currentFolder = e.Folder;
            _folderTextBox.Text = EllipsisFolderPath(e.Folder.FullName);
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
            _deleteToolStripMenuItem.Text = "Delete...";
            _deleteToolStripMenuItem.Image = Properties.Resources.icons8_delete_24;
            _deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;

            _copyToolStripMenuItem = new ToolStripMenuItem();
            _copyToolStripMenuItem.Text = "Copy";
            _copyToolStripMenuItem.Image = Properties.Resources.copyToolStripButton_Image;
            _copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;

            _pasteToolStripMenuItem = new ToolStripMenuItem();
            _pasteToolStripMenuItem.Text = "Paste";
            _pasteToolStripMenuItem.Image = Properties.Resources.pasteToolStripButton_Image;
            _pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;

            _downloadToolStripMenuItem = new ToolStripMenuItem();
            _downloadToolStripMenuItem.Text = "Download";
            _downloadToolStripMenuItem.Click += downloadToolStripMenuItem_Click;

            var refreshToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Image = Properties.Resources.icons8_synchro_24;
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

        public bool IsDeleteAvailable()
        {
            return _listView.Focused && _listView.SelectedItems.Count > 0;
        }

        public void Delete()
        {
            var items = GetSelectedItems();
            DeleteItems?.Invoke(this, new ItemsEventArgs(items.ToArray()));
        }

        private void deleteToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Delete();
        }

        private void refreshToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Refresh?.Invoke(this, EventArgs.Empty);
        }

        private void treeContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            _deleteToolStripMenuItem!.Enabled = IsDeleteAvailable();
            _copyToolStripMenuItem!.Enabled = IsCopyAvailable();
            _downloadToolStripMenuItem!.Enabled = _copyToolStripMenuItem!.Enabled;
            _pasteToolStripMenuItem!.Enabled = IsPasteAvailable();
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
            Copy();
        }

        public bool IsCopyAvailable()
        {
            if (_listView.Focused)
            {
                var selected = GetSelectedItems();
                return selected.Any() && selected.All(it => it.Type == FileSystemType.File);
            }
            return false;
        }

        public void Copy()
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

            if (string.IsNullOrEmpty(Program.Configuration.LocalFileStorageRoot))
            {
                using var dlg = new SettingsForm();
                if (dlg.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
            }

            var root = Path.Combine(Program.Configuration.LocalFileStorageRoot, Program.S3Configuration.BucketName);
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);

            ShowProgressForm("Downloading files...");
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
                        HideProgressForm();
                        actionWithDownloadedFiles?.Invoke(lstFiles);
                    },
                    (ex) =>
                    {
                        HideProgressForm();
                        ShowMessageBox(ex);
                    });
            }
            catch (Exception ex)
            {
                HideProgressForm();
                ShowMessageBox(ex);
            }
        }

        private void ShowProgressForm(string message)
        {
            if (_progressForm != null) HideProgressForm();
            _progressForm = new ProgressForm
            {
                Message = message
            };
            _progressForm.ShowEx(_mainForm);
        }

        private void HideProgressForm()
        {
            _progressForm?.Close();
            _progressForm = null;
        }

        public bool IsPasteAvailable()
        {
            return _listView.Focused && _currentFolder != null &&
                Clipboard.GetFileDropList().Count > 0;
        }

        public void Paste()
        {
            var lstFiles = new List<string>();
            foreach (var filePath in Clipboard.GetFileDropList())
            {
                if (!string.IsNullOrEmpty(filePath)) lstFiles.Add(filePath);
            }
            UploadFiles(lstFiles);
        }

        private void pasteToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Paste();
        }

        private void UploadFiles(IEnumerable<string> filePaths)
        {
            if (_currentFolder == null) return;

            ShowProgressForm("Uploading file...");
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
                       HideProgressForm();
                       UpdateItems();
                   },
                   (ex) =>
                   {
                       HideProgressForm();
                       ShowMessageBox(ex);
                       UpdateItems();
                   });
            }
            catch (Exception ex)
            {
                HideProgressForm();
                ShowMessageBox(ex);
                UpdateItems();
            }
        }
    }
}
