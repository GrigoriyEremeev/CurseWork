using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace CourseWork
{
    public class Operations
    {
        public bool SetData(string massG, string massP, Data dat)
        {
            try
            {
                string[] lines = massG.Split(' ');
                string[] lines2 = massP.Split(' ');
                List<int> list = new List<int>();
                List<int> list2 = new List<int>();
                foreach (string s in lines) 
                    list.Add(int.Parse(s));
                foreach (string s in lines2) 
                    list2.Add(int.Parse(s));
                dat.arrG = new int[list.Count];
                dat.arrP = new int[list2.Count];
                for (int i = 0; i < list.Count; i++) 
                    dat.arrG[i] = list[i];
                for (int i = 0; i < list2.Count; i++)
                    dat.arrP[i] = list2[i];
                return true;
            }
            catch (Exception) { return false; }
        }
        public void DrawVertex(PictureBox paint, ref List<PointF> VPoints,ref Data data , ref int ad)
        {
            for (int i = 0; i < data.arrP.Length; i++)
            {
                Point point = new Point((int)VPoints[i].X, (int)VPoints[i].Y);
                Graphics gr = paint.CreateGraphics();
                SolidBrush pour = new SolidBrush(Color.White);
                SolidBrush lcolor = new SolidBrush(Color.Black);
                Pen pen = new Pen(Color.Red);
                Font font = new Font("Times New Roman", 12);
                Point getp = new Point(point.X + 20, point.Y + 20);
                float start = 0.0F;
                float end = 360.0F;
                Rectangle rctgl = new Rectangle(point.X + 15 , point.Y + 17 ,25, 25);
                ad++;
                gr.FillPie(pour, rctgl, start, end);
                gr.DrawArc(pen, rctgl, start, end);
                gr.DrawString((i + 1).ToString(), font, lcolor, getp);
            }    
        }
        public List<PointF> FindVcoordinates(ref Data data)
        {
            float counter = data.arrP.Length;
            float angle = 0.0f;
            float part = 360 / counter;
            double a;
            List<PointF> pointsF = new List<PointF>(); 
            for (int i = 0; i < counter; i++)
            {
                a = angle * Math.PI / 180F;
                float X = (float)(150 * Math.Cos(a) + 140);
                float Y = (float)(150 * Math.Sin(a) + 140);
                angle += part;
                pointsF.Add(new PointF(X, Y));
            }
            return pointsF;
        }
        public void DrawEdges(PictureBox paint, ref List<PointF> VPoints, ref Data data , ref int am ,ref string text)
        { 
            Graphics gr = paint.CreateGraphics();
            Pen pen = new Pen(Brushes.Blue, 2.0f);         
            List<List<int>> LList = AdVertices(ref data);
            Point a;
            Point b;
            float start = 0.0F;
            float end = 360.0F;
            for (int i = 0; i < LList.Count; i++)
            {
                foreach (var v in LList[i])
                {
                    
                    a = new Point((int)VPoints[i].X + 30, (int)VPoints[i].Y + 30);
                    b = new Point((int)VPoints[v - 1].X + 30, (int)VPoints[v - 1].Y + 30);
                    if(a == b)
                    {
                        Rectangle rctgl = new Rectangle(a.X,a.Y, 25, 25);
                        gr.DrawArc(pen, rctgl, start, end);
                        am+=2;                       
                    }
                    else
                    {
                        gr.DrawLine(pen, a, b);
                        am++;
                    }                        
                }              
            }
        }
        public List<List<int>> AdVertices(ref Data data) 
        {
            List<List<int>> LList = new List<List<int>>();
            List<int> list = new List<int>();
            int lastvalue = 0;
            for (int i = 0; i < data.arrP.Length; i++)
            {
                list.Clear();
                list = new List<int>();
                for (int j = 0 + lastvalue; j < data.arrP[i]; j++)
                {
                    list.Add(data.arrG[j]);
                }
                LList.Add(new List<int>(list));
                if (data.arrP[i] != 0)
                {
                    lastvalue = data.arrP[i];
                }
            }
            return LList;
        }
        public bool ReadFromFile(TextBox text, TextBox text2)
        {
            string str;
            List<string> lines = new List<string>();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Open file";
            openFileDialog1.InitialDirectory = @"D:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader stream = new StreamReader(openFileDialog1.FileName))
                    {
                        while ((str = stream.ReadLine()) != null)
                        {
                            lines.Add(str);
                        }
                    }
                }
                else
                {
                    text = null; text2 = null;
                    return false;
                }
            }
            catch (Exception)
            {
                text.Text = null; text2.Text = null;
                return false;
            }
            text.Text = lines[0];
            text2.Text = lines[1];
            return true;
        }
        public int[,]AdjMatrix(ref Data data)
        {
            int N = data.arrP.Length;
            int[,] mtrx = new int[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    mtrx[i, j] = 0;
            List<List<int>> Llist = AdVertices(ref data);
            for (int i = 0; i < N; i++)
            {
                foreach (var a in Llist[i])
                {
                    mtrx[a - 1, i] = 1;
                    mtrx[i, a - 1] = 1;
                }
            }
            return mtrx;
        }   
    }
}

