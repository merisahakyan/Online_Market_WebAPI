using FuncPlot.Models;
using System;
using System.Collections.Generic;


namespace FuncPlot.Controllers
{
    public class GraphCreator
    {
        public List<Point> GetCoordinates(string function)
        {
            List<Point> coordinates = new List<Point>();
            for(int i=0;i<1000;i++)
            {
                var x = i / 5.0;
                var y = Math.Sin(x);
                coordinates.Add(new Point() { X = x, Y = y });
            }
            return coordinates;
        }
    }
}