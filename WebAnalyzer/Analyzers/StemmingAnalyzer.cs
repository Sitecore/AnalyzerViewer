using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAnalyzer.Analyzers
{
    using System.Collections;
    using System.IO;

    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Util;

    /// <summary>
    /// An analyzer that implements a number of filters. Including porter stemming,
    /// Diacritic stripping, and stop word filtering.
    /// </summary>
    public class StemmingAnalyzer : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            return new PorterStemFilter(new StandardTokenizer(Version.LUCENE_30,  reader));
        }
    }
}