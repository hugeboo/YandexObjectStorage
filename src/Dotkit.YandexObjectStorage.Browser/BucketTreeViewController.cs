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
        private readonly HashSet<TreeNode> _updatedNodes = new HashSet<TreeNode>();

        public event EventHandler? EmptySelectedChanged;
        public event EventHandler<BucketSelectedChangedEventArgs>? BucketSelectedChanged;
        public event EventHandler<FolderSelectedChangedEventArgs>? FolderSelectedChanged;

        public BucketTreeViewController(YClient yosClient, TreeView treeView)
        {
            _yClient = yosClient;
            _treeView = treeView;
            _treeView.AfterSelect += treeView_AfterSelect;
            _treeView.MouseClick += treeView_MouseClick;
            _treeView.BeforeExpand += treeView_BeforeExpand;
            _treeView.BeforeCollapse += treeView_BeforeCollapse;
        }

        public void Attach(ObjectListViewController objectListViewController)
        {
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
            return new TreeNode(folder.Name)
            {
                Tag = folder,
                ImageKey = "folder",
                SelectedImageKey = "folder"
            };
        }

        private void ShowMessageBox(Exception? ex)
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

            if (_updatedNodes.Add(e.Node))
            {
                Utils.DoBackground(
                    () =>
                    {
                        if (e.Node.Tag is YBucketInfo bi)
                        {
                            return YFolder.GetAllAsync(_yClient, bi.Name).GetAwaiter().GetResult();
                        }
                        else if (e.Node.Tag is YFolderInfo fi)
                        {
                            return YFolder.GetAllAsync(_yClient, fi.BucketName, fi.Name).GetAwaiter().GetResult();
                        }
                        else
                        {
                            return new List<YFolderInfo>();
                        }
                    },
                    (lstFolder) =>
                    {
                        e.Node.Nodes.Clear();
                        var nodes = lstFolder.Select(CreateFolderNode).ToArray();
                        e.Node.Nodes.AddRange(nodes);
                        e.Node.Expand();
                    },
                    (ex) =>
                    {
                        ShowMessageBox(ex);
                    });
            }

            if (e.Node.Tag is YBucketInfo bi)
            {
                BucketSelectedChanged?.Invoke(this, new BucketSelectedChangedEventArgs(bi));
            }
            else if (e.Node.Tag is YFolderInfo fi)
            {
                FolderSelectedChanged?.Invoke(this, new FolderSelectedChangedEventArgs(fi));
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
            if (e.Node?.Tag is YFolderInfo)
            {
                e.Node.ImageKey = "open_folder";
                e.Node.SelectedImageKey = "open_folder";
            }
        }
    }

    public sealed class BucketSelectedChangedEventArgs : EventArgs
    {
        public YBucketInfo Bucket { get; }
        public BucketSelectedChangedEventArgs(YBucketInfo bucket)
        {
            Bucket = bucket;
        }
    }

    public sealed class FolderSelectedChangedEventArgs : EventArgs
    {
        public YFolderInfo Folder { get; }
        public FolderSelectedChangedEventArgs(YFolderInfo folder)
        {
            Folder = folder;
        }
    }
}
