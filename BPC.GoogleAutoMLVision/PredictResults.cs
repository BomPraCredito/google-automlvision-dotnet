using System;
using System.Collections.Generic;
using System.Text;

namespace BPC.GoogleAutoMLVision
{
    public class Classification
    {
        public double score { get; set; }
    }

    public class PredictResult
    {
        public Classification classification { get; set; }
        public string displayName { get; set; }

    }
    public class PredictResults
    {
        public List<PredictResult> payload { get; set; }
    }
}
