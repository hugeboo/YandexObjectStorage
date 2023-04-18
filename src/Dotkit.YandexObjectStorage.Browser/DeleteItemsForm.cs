using Dotkit.S3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dotkit.YandexObjectStorage.Browser
{
    public partial class DeleteItemsForm : Form
    {
        private readonly IS3Service? _service;
        private CancellationTokenSource? _cancellationTokenSource;

        public DeleteItemsForm()
        {
            InitializeComponent();
        }

        public DeleteItemsForm(IS3Service service, IEnumerable<IS3FileSystemInfo> items)
        {
            InitializeComponent();
            _service = service ?? throw new ArgumentNullException(nameof(service));
            InitListView(items ?? throw new ArgumentNullException(nameof(items)));
            UpdateDeleteButton();
        }

        private void InitListView(IEnumerable<IS3FileSystemInfo> items)
        {
            var lstItems = new List<ListViewItem>();
            foreach (var item in items)
            {
                var imgKey = item.Type == FileSystemType.Directory ? "folder" : "file";
                var listViewItem = new ListViewItem(new[] { item.Type.ToString(), item.FullName }, imgKey)
                {
                    Checked = true,
                    Tag = item
                };
                lstItems.Add(listViewItem);
            }
            listView.Items.Clear();
            listView.Items.AddRange(lstItems.ToArray());
            typeColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            nameColumnHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private List<IS3FileSystemInfo> GetCheckedItems()
        {
            var lst = new List<IS3FileSystemInfo>();
            foreach (ListViewItem listViewItem in listView.Items)
            {
                if (listViewItem?.Checked ?? false) lst.Add((IS3FileSystemInfo)listViewItem.Tag);
            }
            return lst;
        }

        private void UpdateDeleteButton()
        {
            var listView = GetCheckedItems();
            if (listView.Any())
            {
                deleteButton.Text = $"Delete ({listView.Count})";
                deleteButton.Enabled = true;
            }
            else
            {
                deleteButton.Text = "Delete";
                deleteButton.Enabled = false;
            }
        }

        private void DeleteItems(IEnumerable<IS3FileSystemInfo> items)
        {
            Utils.DoBackground(
                () =>
                {
                    _cancellationTokenSource = new CancellationTokenSource();
                    foreach (var item in items)
                    {
                        item.DeleteAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                        if (_cancellationTokenSource.IsCancellationRequested) break;
                    }
                },
                () =>
                {
                    if (!(_cancellationTokenSource?.IsCancellationRequested ?? false))
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                },
                (ex) =>
                {
                    ShowMessageBox(ex);
                });
        }

        private static void ShowMessageBox(Exception? ex)
        {
            MessageBox.Show(ex?.Message ?? "Unknown exception", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteItems(GetCheckedItems());
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (progressBar.Visible)
            {
                _cancellationTokenSource?.Cancel();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void listView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateDeleteButton();
        }

        private void DeleteItemsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = progressBar.Visible;
        }
    }
}
