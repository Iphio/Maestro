using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maestro
{
    class Song
    {

        public Song()
        {
            String Title;
            int Length;
            String Artist;
            String Filepath;
            List<Step> _listOfSteps;
        }
        public List<Step> _listOfSteps{get;set;}
    }
}
