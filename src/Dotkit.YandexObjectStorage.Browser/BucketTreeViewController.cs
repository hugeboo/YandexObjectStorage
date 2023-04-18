using Dotkit.S3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal sealed class BucketTreeViewController
    {
        private readonly IS3Service _service;
        private readonly TreeView _treeView;
        private readonly MainForm _mainForm;
        private readonly HashSet<TreeNode> _initializedNodes = new();
        private readonly Dictionary<string, TreeNode> _nodeByFolderKey = new();

        private ToolStripMenuItem? _createFolderToolStripMenuItem;
        private ToolStripMenuItem? _deleteFolderToolStripMenuItem;

        public event EventHandler? EmptySelectedChanged;
        public event EventHandler<FolderEventArgs>? FolderSelectedChanged;

        public BucketTreeViewController(IS3Service service, TreeView treeView, MainForm mainForm)
        {
            _service = service;
            _treeView = treeView;
            _mainForm = mainForm;
            _treeView.AfterSelect += treeView_AfterSelect;
            _treeView.MouseClick += treeView_MouseClick;
            _treeView.BeforeExpand += treeView_BeforeExpand;
            _treeView.BeforeCollapse += treeView_BeforeCollapse;
            _treeView.KeyDown += treeView_KeyDown;
            CreateContextMenu();
            _mainForm = mainForm;
        }

        private void treeView_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                deleteFolderToolStripMenuItem_Click(sender, e);
            }
        }

        public void Attach(ObjectListViewController objectListViewController)
        {
            objectListViewController.FolderDoubleClick += ObjectListViewController_FolderDoubleClick;
            objectListViewController.CreateNewFolder += ObjectListViewController_CreateNewFolder;
            objectListViewController.DeleteItems += ObjectListViewController_DeleteFolders;
            objectListViewController.Refresh += ObjectListViewController_Refresh;
        }

        private void ObjectListViewController_Refresh(object? sender, EventArgs e)
        {
            RefreshNode(_treeView.SelectedNode);
        }

        private void ObjectListViewController_DeleteFolders(object? sender, ItemsEventArgs e)
        {
            DeleteItems(_treeView.SelectedNode, e.Items);
        }

        private void ObjectListViewController_CreateNewFolder(object? sender, EventArgs e)
        {
            createFolderToolStripMenuItem_Click(sender, e);
        }

        private void CreateContextMenu()
        {
            _createFolderToolStripMenuItem = new ToolStripMenuItem();
            _createFolderToolStripMenuItem.Text = "Create folder...";
            _createFolderToolStripMenuItem.Click += createFolderToolStripMenuItem_Click;

            _deleteFolderToolStripMenuItem = new ToolStripMenuItem();
            _deleteFolderToolStripMenuItem.Text = "Delete folder";
            _deleteFolderToolStripMenuItem.Click += deleteFolderToolStripMenuItem_Click;

            var refreshToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;

            var treeContextMenu = new ContextMenuStrip();
            treeContextMenu.Items.AddRange(new ToolStripItem[] 
            {
                _createFolderToolStripMenuItem,
                new ToolStripSeparator(),
                _deleteFolderToolStripMenuItem,
                new ToolStripSeparator(),
                refreshToolStripMenuItem 
            });
            treeContextMenu.Opening += treeContextMenu_Opening;

            _treeView.ContextMenuStrip = treeContextMenu;
        }

        private void createFolderToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            using var dlg = new CreateFolderForm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var node = _treeView.SelectedNode;
                var parentFolder = node?.Tag as S3DirectoryInfo;
                Utils.DoBackground(
                    () =>
                    {
                        if (parentFolder == null) parentFolder = _service.Root;

                        var fi = parentFolder.GetSubDirectoryAsync(dlg.FolderName).ConfigureAwait(false).GetAwaiter().GetResult();
                        fi.CreateAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    },
                    () =>
                    {
                        RefreshNode(node);
                    },
                    (ex) =>
                    {
                        ShowMessageBox(ex);
                    });
            }
        }

        private void deleteFolderToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            DeleteFolder(_treeView.SelectedNode);
        }

        private void DeleteFolder(TreeNode node)
        {
            if (node?.Tag is not S3DirectoryInfo fi) return;

            var dlg = new DeleteItemsForm(_service, new[] { fi });
            dlg.ShowDialog(_mainForm);
            RefreshNode(node.Parent);
        }

        private void DeleteItems(TreeNode parentNode, IEnumerable<IS3FileSystemInfo> items)
        {
            var dlg = new DeleteItemsForm(_service, items);
            dlg.ShowDialog(_mainForm);
            RefreshNode(parentNode);
        }

        private void refreshToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            RefreshNode(_treeView.SelectedNode);
        }

        private void RefreshNode(TreeNode? node)
        {
            if (node == null) return;
            _treeView.Cursor = Cursors.WaitCursor;
            Utils.DoBackground(
               () =>
               {
                   if (node.Tag is S3DirectoryInfo fi)
                   {
                       return fi.GetDirectories().ConfigureAwait(false).GetAwaiter().GetResult();
                   }
                   else
                   {
                       return new List<S3DirectoryInfo>();
                   }
               },
               (lstFolder) =>
               {
                   _treeView.Cursor = Cursors.Default;
                   node.Nodes.Clear();
                   var nodes = lstFolder.Select(CreateFolderNode).ToArray();
                   node.Nodes.AddRange(nodes);
                   node.Expand();
                   FireSelectedChanged(node);
               },
               (ex) =>
               {
                   _treeView.Cursor = Cursors.Default;
                   ShowMessageBox(ex);
               });
        }

        private void treeContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var selected = _treeView.SelectedNode?.Tag;
            _createFolderToolStripMenuItem!.Enabled = true;
            _deleteFolderToolStripMenuItem!.Enabled = selected != null;
        }

        private void ObjectListViewController_FolderDoubleClick(object? sender, FolderEventArgs e)
        {
            if (_nodeByFolderKey.TryGetValue(e.Folder.FullName, out TreeNode? node))
            {
                _treeView.SelectedNode = node;
                node!.Expand();
            }
        }

        public void Init()
        {
            _treeView.Nodes.Clear();
            var rootNode = CreateFolderNode(_service.Root);
            _treeView.Nodes.Add(rootNode);
            _treeView.SelectedNode = rootNode;
        }

        private TreeNode CreateFolderNode(S3DirectoryInfo folder)
        {
            var imgKey = string.IsNullOrEmpty(folder.Key) ? "bucket" : "folder";
            var name = string.IsNullOrEmpty(folder.Key) ? Program.S3Configuration.BucketName : folder.Name;
            var node = new TreeNode(name)
            {
                Tag = folder,
                ImageKey = imgKey,
                SelectedImageKey = imgKey
            };
            node.Nodes.Add(new TreeNode());
            _nodeByFolderKey[folder.FullName] = node;
            return node;
        }

        private static void ShowMessageBox(Exception? ex)
        {
            MessageBox.Show(ex?.Message ?? "Unknown exception", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void treeView_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                EmptySelectedChanged?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (_initializedNodes.Add(e.Node))
            {
                RefreshNode(e.Node);
            }

            FireSelectedChanged(e.Node);
        }

        private void FireSelectedChanged(TreeNode node)
        {
            if (node.Tag is S3DirectoryInfo fi)
            {
                FolderSelectedChanged?.Invoke(this, new FolderEventArgs(fi));
            }
            else
            {
                EmptySelectedChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void treeView_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = _treeView.HitTest(e.Location);
                _treeView.SelectedNode = hit.Node;
            }
        }

        private void treeView_BeforeCollapse(object? sender, TreeViewCancelEventArgs e)
        {
            if (e.Node?.Tag is S3DirectoryInfo di)
            {
                var imgKey = string.IsNullOrEmpty(di.Key) ? "bucket" : "folder";
                e.Node.ImageKey = imgKey;
                e.Node.SelectedImageKey = imgKey;
            }
        }

        private void treeView_BeforeExpand(object? sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null && _initializedNodes.Add(e.Node))
            {
                RefreshNode(e.Node);
            }

            if (e.Node?.Tag is S3DirectoryInfo di)
            {
                var imgKey = string.IsNullOrEmpty(di.Key) ? "bucket" : "open_folder";
                e.Node.ImageKey = imgKey;
                e.Node.SelectedImageKey = imgKey;
            }
        }
    }

    public sealed class FolderEventArgs : EventArgs
    {
        public S3DirectoryInfo Folder { get; }
        public FolderEventArgs(S3DirectoryInfo folder)
        {
            Folder = folder;
        }
    }

    public sealed class ItemsEventArgs : EventArgs
    {
        public IS3FileSystemInfo[] Items { get; }
        public ItemsEventArgs(IS3FileSystemInfo[] items)
        {
            Items = items;
        }
    }
}
