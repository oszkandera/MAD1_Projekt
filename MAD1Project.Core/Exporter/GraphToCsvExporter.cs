using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mad1_Projekt.Exporter
{
    public class GraphToCsvExporter
    {
        public string CreateExportString(Dictionary<int, HashSet<int>> graph)
        {
            var exportStringBuilder = new StringBuilder();

            foreach(var node in graph)
            {
                foreach(var neighbor in node.Value)
                {
                    exportStringBuilder.Append($"{node.Key};{neighbor}{Environment.NewLine}");
                }
            }

            return exportStringBuilder.ToString();
        }

        public void Export(Stream stream, Dictionary<int, HashSet<int>> graph)
        {
            using(var streamWriter = new StreamWriter(stream))
            {
                var exportString = CreateExportString(graph);
                streamWriter.Write(exportString);
            }
        }
    }
}
