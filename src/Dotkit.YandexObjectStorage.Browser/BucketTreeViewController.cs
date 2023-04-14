using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.Browser
{
    internal sealed class BucketTreeViewController
    {
        private readonly YOSClient _yosClient;
        private readonly TreeView _treeView;
        private ObjectListViewController? _objectListViewController;

        public event EventHandler<ItemSelectedChangedEventArgs>? ItemSelectedChanged;

        public BucketTreeViewController(YOSClient yosClient, TreeView treeView)
        {
            _yosClient = yosClient;
            _treeView = treeView;
            _treeView.AfterSelect += treeView_AfterSelect;
            _treeView.MouseClick += treeView_MouseClick;
            _treeView.BeforeExpand += treeView_BeforeExpand;
            _treeView.BeforeCollapse += treeView_BeforeCollapse;
        }

        public void Attach(ObjectListViewController objectListViewController)
        {
            _objectListViewController = objectListViewController;
        }

        public void Init()
        {
            _treeView.Nodes.Clear();
            Utils.DoBackground(
                () => 
                {
                    return _yosClient.GetBuckets().GetAwaiter().GetResult();
                }, 
                (lstBucket) => 
                {
                    var nodes = lstBucket.Select(CreateBucketNode).ToArray();
                    _treeView.Nodes.AddRange(nodes);
                    _treeView.SelectedNode = nodes.FirstOrDefault();
                }, 
                (ex) => 
                {
                    MessageBox.Show(ex?.Message ?? "Unknown exception", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
        }

        private TreeNode CreateBucketNode(YOSBucket bucket)
        {
            return new TreeNode(bucket.Name)
            {
                Tag = bucket,
                ImageKey = "bucket",
                SelectedImageKey = "bucket"
            };
        }

        private TreeNode CreateFolderNode(YOSFolder folder)
        {
            return new TreeNode(folder.Name)
            {
                Tag = folder,
                ImageKey = "folder",
                SelectedImageKey = "folder"
            };
        }

        private void treeView_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                ItemSelectedChanged?.Invoke(this, new ItemSelectedChangedEventArgs());
                return;
            }

            var eventArgs = new ItemSelectedChangedEventArgs
            {
                Bucket = e.Node.Tag as YOSBucket,
                Folder = e.Node.Tag as YOSFolder
            };

            Utils.DoBackground(
                () =>
                {
                    if (eventArgs.IsBucket)
                    {
                        return _yosClient.GetFolders(eventArgs.Bucket!.Name).GetAwaiter().GetResult();
                    }
                    else if (eventArgs.IsFolder)
                    {
                        return _yosClient.GetFolders(eventArgs.Folder!).GetAwaiter().GetResult();
                    }
                    else
                    {
                        return new List<YOSFolder>();
                    }
                },
                (lstFolder) =>
                {
                    e.Node.Nodes.Clear();
                    var nodes = lstFolder.Select(CreateFolderNode).ToArray();
                    e.Node.Nodes.AddRange(nodes);
                },
                (ex) =>
                {
                    MessageBox.Show(ex?.Message ?? "Unknown exception", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });

            ItemSelectedChanged?.Invoke(this, eventArgs);
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
            if (e.Node?.Tag is YOSFolder)
            {
                e.Node.ImageKey = "folder";
                e.Node.SelectedImageKey = "folder";
            }
        }

        private void treeView_BeforeExpand(object? sender, TreeViewCancelEventArgs e)
        {
            if (e.Node?.Tag is YOSFolder)
            {
                e.Node.ImageKey = "open_folder";
                e.Node.SelectedImageKey = "open_folder";
            }
        }
    }

    public sealed class ItemSelectedChangedEventArgs : EventArgs
    {
        public bool IsEmpty => !IsBucket && !IsFolder;
        public bool IsBucket => Bucket != null;
        public bool IsFolder => Folder != null;

        public YOSBucket? Bucket { get; set; }
        public YOSFolder? Folder { get; set; }
    }
}
