using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisputeCommon.Feedback
{
	public class ArgumentFeedback
	{
        public string message="";
        public string argumentName="";
        public string playerName = "";
        public double rollResult;
        public List<string> affectedProperty=new List<string>();
        public List<double> affectedValues=new List<double>();
        public Result result = Result.None;
        public bool hidden = true;
	}
}
