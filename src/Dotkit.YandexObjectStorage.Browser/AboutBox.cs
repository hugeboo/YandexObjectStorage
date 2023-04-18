using Amazon.S3.Model;
using Dotkit.S3;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dotkit.YandexObjectStorage.Browser
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            FillData();
        }

        private void FillData()
        {
            var asm = Assembly.GetExecutingAssembly();
            var s3Asm = Assembly.GetAssembly(typeof(IS3Service))!;
            var awsAsm = Assembly.GetAssembly(typeof(S3Bucket))!;
            var jsonAsm = Assembly.GetAssembly(typeof(JsonWriterException))!;

            var title = (asm.GetCustomAttribute(typeof(AssemblyProductAttribute)) as AssemblyProductAttribute)?.Product;
            var version = asm.GetName().Version?.ToString();
            var copyright = (asm.GetCustomAttribute(typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute)?.Copyright;
            this.Text = $"About {title}";
            this.labelProductName.Text = $"{title} {version}";
            this.labelCopyright.Text = copyright;

            var desc = (asm.GetCustomAttribute(typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute)?.Description;
            var description = $"{desc}\r\n\r\nUsed packages:\r\n{s3Asm.FullName}\r\n{awsAsm.FullName}\r\n{jsonAsm.FullName}";
            this.textBoxDescription.Text = description;
        }
    }
}
