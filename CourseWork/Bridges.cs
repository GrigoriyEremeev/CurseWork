using System;
using System.Collections.Generic;

namespace CourseWork
{
    public class Bridges
    {
        private int V = 0; 
        private List<int>[] adj;
        int time = 0;
        static readonly int NIL = -1;
        
        Bridges(int v)
        {
            V = v;
            adj = new List<int>[v];
            for (int i = 0; i < v; ++i)
                adj[i] = new List<int>();
        }
        void addEdge(int v, int w)
        {
            adj[v].Add(w); 
            adj[w].Add(v);
        }
        void bridgeUtil(int u, bool[] visited, int[] disc,int[] low, int[] parent,string[] bridges ,ref int couter)
        {
            visited[u] = true;          
            disc[u] = low[u] = ++time;           
            foreach (int i in adj[u])
            {
                int v = i; 
                if (!visited[v])
                {
                    parent[v] = u;
                    bridgeUtil(v, visited, disc, low, parent ,bridges,ref couter);
                    low[u] = Math.Min(low[u], low[v]);                   
                    if (low[v] > disc[u])
                    {
                        couter++;
                        bridges[couter] += $"{u + 1} {v + 1}";
                    }
                       
                }
                else if (v != parent[u])
                {
                    low[u] = Math.Min(low[u], disc[v]);
                }      
            }
        }
        void bridge(string []bridges, ref int couter)
        {
            bool[] visited = new bool[V];
            int[] disc = new int[V];
            int[] low = new int[V];
            int[] parent = new int[V];
            for (int i = 0; i < V; i++)
            {
                parent[i] = NIL;
                visited[i] = false;
            }
            for (int i = 0; i < V; i++)
                if (visited[i] == false)
                    bridgeUtil(i, visited, disc, low, parent, bridges, ref couter);
        }
        public static int FindBridges(ref int[,]mtrx,ref Data data,  out string []brid)
        {
            int couter = 0;
            string[] bridges = new string[50];
            int n = data.arrP.Length;
            Bridges g1 = new Bridges(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mtrx[i, j] == 1)
                    {
                        g1.addEdge(i, j);
                    }
                }
            } 
            g1.bridge(bridges,ref couter);
            brid = bridges;
            return couter;
        }
    }
}
