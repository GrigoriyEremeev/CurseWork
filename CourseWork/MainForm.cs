using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class MainForm : Form
    {
        Data data1 = new Data();
        Data data2 = new Data();
        List<PointF> Vcoordinates = new List<PointF>();
        Operations op = new Operations();
        int bridges, bridges2;
        public MainForm()
        {
            InitializeComponent();
        }
        private void button_create_1_Click(object sender, EventArgs e)
        {
            string masG = textBox_G.Text;
            string masP = textBox_P.Text;
            int am = 0;
            int ad = 0;
            string []brid;
            string str = "";
            if(textBox_G.Text == "" || textBox_P.Text == "")
            {
                MessageBox.Show("Введите поля G и P", "Ошибка!");
            }
            else
            {
                if (masG.Split(' ').Length / 2 <= 50 && masP.Split(' ').Length <= 20)
                {
                    try
                    {
                        op.SetData(masG, masP, data1);
                        Vcoordinates = op.FindVcoordinates(ref data1);
                        int[,] mtrx = op.AdjMatrix(ref data1);
                        op.DrawEdges(graph1, ref Vcoordinates, ref data1, ref am , ref str);
                        bridges = Bridges.FindBridges(ref mtrx, ref data1, out  brid);
                        op.DrawVertex(graph1, ref Vcoordinates,ref data1,ref ad);
                        textBox_graph1.Text = $"Количесво вершин = {ad}\nКоличесво ребер = {am / 2}\n";
                        DGV_graph1.RowCount = data1.arrP.Length;
                        DGV_graph1.ColumnCount = data1.arrP.Length;
                        DGV_graph1.RowHeadersVisible = false;
                        DGV_graph1.ColumnHeadersVisible = false;
                        for (int i = 0; i < data1.arrP.Length; i++)
                        { 
                            for (int j = 0; j < data1.arrP.Length; j++)
                            {
                                DGV_graph1.Columns[i].Width = 20;
                                DGV_graph1.Rows[i].Cells[j].Value = mtrx[i, j];
                            }

                        }
                        textBox_graph1.Text += "Количесво мостов = " + bridges + "\n";
                        for (int i = 0; i < bridges; i++)
                        {
                            textBox_graph1.Text += i + 1 + ". Мост(" + brid[i + 1] + ")\n";
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Корректно введите поля G и P", "Ошибка!");
                        graph1.Image = null;
                    }
                }
                else
                {
                    textBox_G.Text = null;
                    textBox_P.Text = null;
                    MessageBox.Show("Количесво вершин не более 20\nКоличесво ребер не более 50", "Ошибка!");
                }
            }
        }
        private void button_create_2_Click(object sender, EventArgs e)
        {
            string masG1 = textBox_2G.Text;
            string masP1 = textBox_2P.Text;
            int am = 0;
            int ad = 0;
            string[] brid2;
            string str = "";
            if (textBox_2G.Text == "" || textBox_2P.Text == "")
            {
                MessageBox.Show("Введите поля G и P", "Ошибка!");
            }
            else
            {
                if (masG1.Split(' ').Length / 2 <= 50 && masP1.Split(' ').Length <= 20)
                {
                    try
                    {

                        op.SetData(masG1, masP1, data2);
                        Vcoordinates = op.FindVcoordinates(ref data2);
                        op.DrawEdges(graph2, ref Vcoordinates, ref data2, ref am, ref str);
                        textBox_graph1.Text += str;
                        int[,] mtrx2 = op.AdjMatrix(ref data2);
                        bridges2 = Bridges.FindBridges(ref mtrx2, ref data2, out brid2);
                        op.DrawVertex(graph2, ref Vcoordinates, ref data2, ref ad);
                        textBox_graph2.Text = $"Количесво вершин = {ad}\nКоличесво ребер = {am / 2}\n";
                        DGV_graph2.RowCount = data2.arrP.Length;
                        DGV_graph2.ColumnCount = data2.arrP.Length;
                        DGV_graph2.RowHeadersVisible = false;
                        DGV_graph2.ColumnHeadersVisible = false;
                        for (int i = 0; i < data2.arrP.Length; i++)
                        {

                            for (int j = 0; j < data2.arrP.Length; j++)
                            {
                                DGV_graph2.Columns[i].Width = 20;
                                DGV_graph2.Rows[i].Cells[j].Value = mtrx2[i, j];
                            }

                        }
                        textBox_graph2.Text += "Количесво мостов = " + bridges2 + "\n";
                        for (int i = 0; i < bridges2; i++)
                        {
                            textBox_graph2.Text += i + 1 + ". Мост(" + brid2[i + 1] + ")\n";
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Корректно введите поля G и P", "Ошибка!");
                        graph2.Image = null;
                    }

                }
                else
                {
                    textBox_2G.Text = null;
                    textBox_2P.Text = null;
                    MessageBox.Show("Количесво вершин не более 20\nКоличесво ребер не более 50", "Ошибка!");
                }
            }
        }
        private void button_clear_Click(object sender, EventArgs e)
        {
            graph1.Image = null;
            graph2.Image = null;
            textBox_graph1.Text = "";
            textBox_graph2.Text = "";
            textbox_res.Text = "";
            DGV_graph1.Columns.Clear();
            DGV_graph2.Columns.Clear();

        }
        private void button_read_1_Click(object sender, EventArgs e)
        {
            op.ReadFromFile(textBox_G, textBox_P);
        }
        private void button_read_2_Click(object sender, EventArgs e)
        {
            op.ReadFromFile(textBox_2G, textBox_2P);
        }
        private void button_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button_Equivalence_Click(object sender, EventArgs e)
        {
            if (textBox_graph1.Text == "" || textBox_graph2.Text == "")
            {
                MessageBox.Show("Для эквивалентности нужны два графа", "Ошибка!");
            }
            else
            {
                if (bridges == bridges2)
                    textbox_res.Text = ($"Графы эквивалентны за количесвом мостов\nКоличесво мостов в 1 графе:{bridges}\nКоличесво мостов в 2 графе:{bridges2}");
                else
                    textbox_res.Text = ($"Графы не эквивалентны за количесвом мостов\nКоличесво мостов в 1 графе:{bridges}\nКоличесво мостов в 2 графе:{bridges2}");
            }
        }    
    }
}
