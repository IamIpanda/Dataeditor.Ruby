using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DataEditor.Arce
{
    partial class AboutDialog : Form
    {
        List<Assembly> assemblies = new List<Assembly>();
        public AboutDialog()
        {
            InitializeComponent();
            this.Text = "关于 Dataeditor.Arce";
            protoListBox1.Items.Clear();
            label2.Text = "ver. " + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            label3.Text = AssemblyDescription(Assembly.GetExecutingAssembly());
            System.Reflection.Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly ass in currentAssemblies)
                if (ass.GetName().Name.StartsWith("DataEditor"))
                {
                    assemblies.Add(ass);
                    protoListBox1.Items.Add(ass.GetName().Name);
                }
            if (protoListBox1.Items.Count > 0) protoListBox1.SelectedIndex = 0;
        }

        #region 程序集特性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription(Assembly ass)
        {
            object[] attributes = ass.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }

        public string AssemblyProduct(Assembly ass)
        {
            object[] attributes = ass.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyProductAttribute)attributes[0]).Product;
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany(Assembly ass)
        {
            object[] attributes = ass.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
        #endregion

        private void protoListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var assembly = assemblies[protoListBox1.SelectedIndex];
            var sb = new System.Text.StringBuilder();
            var assemblyname = assembly.GetName();
            sb.AppendLine(assemblyname.Name);
            sb.Append("ver.");
            sb.AppendLine(assemblyname.Version.ToString(3));
            sb.Append("by.");
            sb.AppendLine(this.AssemblyCompany(assembly));
            sb.Append("description.");
            sb.AppendLine(this.AssemblyDescription(assembly));
            textBox1.Text = sb.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
