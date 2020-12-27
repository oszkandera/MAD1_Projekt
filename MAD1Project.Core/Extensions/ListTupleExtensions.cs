using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;

namespace MAD1Project.Core.Extensions
{
    public static class ListTupleExtensions
    {
        public static List<DataPoint> ToDataPointList<T1, T2>(this List<Tuple<T1, T2>> data)
        {
            var result = new List<DataPoint>();

            foreach(var element in data)
            {
                result.Add(new DataPoint(Convert.ToDouble(element.Item1), Convert.ToDouble(element.Item2)));
            }

            return result;
        }

        public static List<ScatterPoint> ToScatterPoint<T1, T2>(this List<Tuple<T1, T2>> data)
        {
            var result = new List<ScatterPoint>();

            foreach (var element in data)
            {
                result.Add(new ScatterPoint(Convert.ToDouble(element.Item1), Convert.ToDouble(element.Item2)));
            }

            return result;
        }
    }
}
