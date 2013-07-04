using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAnalyzer.Analyzers
{
    using System.IO;

    using Lucene.Net.Analysis;
    using Lucene.Net.Analysis.Standard;

    public class WordDelimiterAnalyzer : Analyzer
    {
        #region Overrides of Analyzer

        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            TokenStream result = new WhitespaceTokenizer(reader);

            result = new StandardFilter(result);
            result = new LowerCaseFilter(result);
            result = new StopFilter(result, StopAnalyzer.ENGLISH_STOP_WORDS);
            result = new WordDelimiterFilter(result, 1, 1, 1, 1, 0);

            return result;
        }

        #endregion
    }
}