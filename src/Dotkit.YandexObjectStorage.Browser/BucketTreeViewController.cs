using Dotkit.YandexObjectStorage.FileSystem;
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
        private readonly YClient _yClient;
        private readonly TreeView _treeView;
        private readonly HashSet<TreeNode> _initializedNodes = new();
        private readonly Dictionary<string, TreeNode> _nodeByFolderKey = new();

        //private ContextMenuStrip _treeContextMenu;
        private ToolStripMenuItem? _createFolderToolStripMenuItem;
        private ToolStripMenuItem? _deleteFolderToolStripMenuItem;

        public event EventHandler? EmptySelectedChanged;
        public event EventHandler<BucketEventArgs>? BucketSelectedChanged;
        public event EventHandler<FolderEventArgs>? FolderSelectedChanged;

        public BucketTreeViewController(YClient yosClient, TreeView treeView)
        {
            _yClient = yosClient;
            _treeView = treeView;
            _treeView.AfterSelect += treeView_AfterSelect;
            _treeView.MouseClick += treeView_MouseClick;
            _treeView.BeforeExpand += treeView_BeforeExpand;
            _treeView.BeforeCollapse += treeView_BeforeCollapse;
            CreateContextMenu();
        }

        public void Attach(ObjectListViewController objectListViewController)
        {
            objectListViewController.FolderDoubleClick += ObjectListViewController_FolderDoubleClick;
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
                _deleteFolderToolStripMenuItem,
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
                string bucketName = string.Empty;
                string? baseFolder = null;
                if (node.Tag is YBucketInfo bi)
                {
                    bucketName = bi.Name;
                }
                else if (node.Tag is YFolderInfo fi)
                {
                    bucketName = fi.BucketName;
                    baseFolder = fi.Key;
                }
                Utils.DoBackground(
                    () =>
                    {
                        YFolder.CreateAsync(_yClient, bucketName, dlg.FolderName, baseFolder).GetAwaiter().GetResult();
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
            var node = _treeView.SelectedNode;
            if (node.Tag is not YFolderInfo fi) return;

            if (MessageBox.Show($"Do you really want to delete '{node.Text}' ?", "Delete Folder", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                == DialogResult.OK)
            {
                Utils.DoBackground(
                    () =>
                    {
                        YFolder.DeleteAsync(_yClient, fi).GetAwaiter().GetResult();
                    },
                    () =>
                    {
                        RefreshNode(node.Parent);
                    },
                    (ex) =>
                    {
                        ShowMessageBox(ex);
                    });
            }
        }

        private void refreshToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            RefreshNode(_treeView.SelectedNode);
        }

        private void RefreshNode(TreeNode? node)
        {
            if (node == null) return;
            Utils.DoBackground(
               () =>
               {
                   if (node.Tag is YBucketInfo bi)
                   {
                       return YFolder.GetAllAsync(_yClient, bi.Name).GetAwaiter().GetResult();
                   }
                   else if (node.Tag is YFolderInfo fi)
                   {
                       return YFolder.GetAllAsync(_yClient, fi.BucketName, fi.Key).GetAwaiter().GetResult();
                   }
                   else
                   {
                       return new List<YFolderInfo>();
                   }
               },
               (lstFolder) =>
               {
                   node.Nodes.Clear();
                   var nodes = lstFolder.Select(CreateFolderNode).ToArray();
                   node.Nodes.AddRange(nodes);
                   node.Expand();
               },
               (ex) =>
               {
                   ShowMessageBox(ex);
               });
        }

        private void treeContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var selected = _treeView.SelectedNode?.Tag;
            _createFolderToolStripMenuItem!.Enabled = selected != null;
            _deleteFolderToolStripMenuItem!.Enabled = selected is YFolderInfo;
        }

        private void ObjectListViewController_FolderDoubleClick(object? sender, FolderEventArgs e)
        {
            if (_nodeByFolderKey.TryGetValue(e.Folder.Key, out TreeNode? node))
            {
                _treeView.SelectedNode = node;
                node.Expand();
            }
        }

        public void Init()
        {
            _treeView.Nodes.Clear();
            Utils.DoBackground(
                () => 
                {
                    return YBucket.GetAllAsync(_yClient).GetAwaiter().GetResult();
                }, 
                (lstBucket) => 
                {
                    var nodes = lstBucket.Select(CreateBucketNode).ToArray();
                    _treeView.Nodes.AddRange(nodes);
                    _treeView.SelectedNode = nodes.FirstOrDefault();
                }, 
                (ex) => 
                {
                    ShowMessageBox(ex);
                });
        }

        private TreeNode CreateBucketNode(YBucketInfo bucket)
        {
            return new TreeNode(bucket.Name)
            {
                Tag = bucket,
                ImageKey = "bucket",
                SelectedImageKey = "bucket"
            };
        }

        private TreeNode CreateFolderNode(YFolderInfo folder)
        {
            var node = new TreeNode(folder.Name)
            {
                Tag = folder,
                ImageKey = "folder",
                SelectedImageKey = "folder"
            };
            node.Nodes.Add(new TreeNode());
            _nodeByFolderKey[folder.Key] = node;
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

            if (e.Node.Tag is YBucketInfo bi)
            {
                BucketSelectedChanged?.Invoke(this, new BucketEventArgs(bi));
            }
            else if (e.Node.Tag is YFolderInfo fi)
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
            if (e.Node?.Tag is YFolderInfo)
            {
                e.Node.ImageKey = "folder";
                e.Node.SelectedImageKey = "folder";
            }
        }

        private void treeView_BeforeExpand(object? sender, TreeViewCancelEventArgs e)
        {
            if (e.Node != null && _initializedNodes.Add(e.Node))
            {
                RefreshNode(e.Node);
            }

            if (e.Node?.Tag is YFolderInfo)
            {
                e.Node.ImageKey = "open_folder";
                e.Node.SelectedImageKey = "open_folder";
            }
        }
    }

    public sealed class BucketEventArgs : EventArgs
    {
        public YBucketInfo Bucket { get; }
        public BucketEventArgs(YBucketInfo bucket)
        {
            Bucket = bucket;
        }
    }

    public sealed class FolderEventArgs : EventArgs
    {
        public YFolderInfo Folder { get; }
        public FolderEventArgs(YFolderInfo folder)
        {
            Folder = folder;
        }
    }
}
