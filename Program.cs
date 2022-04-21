using System;
using System.Collections.Generic;
using System.Linq;
using Console = System.Diagnostics.Debug;

namespace ConsoleApp1
{
    class CompareRoutes : IComparer<Route>
    {
        public int Compare(Route obj1, Route obj2)
        {
            string s1 = obj1.BeginNode + obj1.EndNode;
            string s2 = obj2.BeginNode + obj2.EndNode;
            return s1.CompareTo(s2);
        }
    }

    class Route
    {
        public string BeginNode { get; set; }
        public string EndNode { get; set; }
    }

    class Program
    {
        public static void FindAllRoutes(List<Route> routes, ref List<string> allRoutes, ref string partialRoute, string beginNode, string endNode)
        {   
            List<Route> l = routes.Where(r => r.BeginNode == beginNode).ToList();
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i].EndNode == endNode)
                {
                    allRoutes.Add(partialRoute + "-" + endNode);
                }
                else
                {
					partialRoute = partialRoute + "-" + l[i].EndNode;
                    FindAllRoutes(routes, ref allRoutes, ref partialRoute, l[i].EndNode, endNode);
                }
            }            
        }

        public static string GraphChallenge(string[] strArr)
        {
            int nodeNo;
            string beginNode;
            string endNode;
            List<string> nodes;
            List<Route> routes;
            Route route;
            int index;
            List<string> allRoutes;
    		string partialRoute;
			int shortestRouteIndex;
			string shortestRoute;

            nodeNo = Convert.ToInt32(strArr[0]);
            nodes = new List<string>();
            for (int i = 1; i <= nodeNo; i++)
            {
                nodes.Add(strArr[i]);
            }

            beginNode = nodes[0];
            endNode = nodes[nodes.Count - 1];

            routes = new List<Route>();
            for (int i = nodeNo + 1; i < strArr.Length; i++)
            {
                var a = strArr[i].Split("-");

                route = new Route() { BeginNode = a[0], EndNode = a[1] };
                index = routes.BinarySearch(route, new CompareRoutes());
                if (index < 0)
                {
                    routes.Insert(~index, route);
                }

				/* route = new Route() { BeginNode = a[1], EndNode = a[0] };
                index = routes.BinarySearch(route, new CompareRoutes());
                if (index < 0)
                {
                    routes.Insert(~index, route);
                } */
            };

            allRoutes = new List<string>();
            partialRoute = beginNode;

            FindAllRoutes(routes, ref allRoutes, ref partialRoute, beginNode, endNode);

			shortestRouteIndex = int.MaxValue;
            shortestRoute = "";
			for (int i = 0; i < allRoutes.Count; i++)
			{
				var t = allRoutes[i].Split("-");
				if (t.Length < shortestRouteIndex)
				{
					shortestRoute = allRoutes[i];
				}
			}
            
            return shortestRoute;
        }

        static void Main(string[] args)
        {            
            Console.WriteLine(GraphChallenge(new string[] {"3", "A", "B", "C", "A-B", "B-C"}));
        }
    }
}
